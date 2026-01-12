using System.Numerics;
using Matrices.Logging;

namespace Matrices;
public static class MatrixOperations {

    public static void GetInverse<T>(this Matrix<T> instance) where T : INumber<T> {
        var logger = new MatrixLog<T>();
        instance.GetReducedRowEchelon<T>(logger);
        Matrix<T> inverse = new Matrix<T>(instance.Rows, instance.Cols);
    }

    public static Matrix<T> GetReducedRowEchelon<T>(this Matrix<T> instance, MatrixLog<T>? logger = null) where T : INumber<T> {
        // 1. reduce to row echelon,
        // 2. rid of upper triangular values and reduce so instance becomes an identity matrix.
        Matrix<T> matrix = instance.GetRowEchelon(logger);
        for(int row = 0; row < (instance.Rows - 1); row++) {
            (int row, int col) pivot = instance.FindPivot(row + 1);
            if(pivot.col < 0) continue;
            // scalar needs to be a value such that (second row's pivot * scalar) + first row's pivot = 0.
            var (rowValue1, rowValue2) = (instance.Get(row, pivot.col), instance.Get((row + 1), pivot.col));
            T scalar = -(rowValue1 / rowValue2);
            // iterate through each value in second row and multiply by {scalar}, 
            // then add that value (which should be the negation of the first row's {col}) - to the first row's {col}.
            for(int col = pivot.col; col < instance.Cols; col++) {
                matrix[row, col] = ((scalar * rowValue2) + rowValue1);
                if(logger is not null) logger.LogStep(new RowOperation<T>(RowOpKind.AddScaled, row, row + 1, scalar));
            }
        }
        Console.WriteLine($"in func before sort: \n{matrix.ToString()}");
        matrix.Sort();
        return matrix;
    }

    public static void ToRowEchelon<T>(this Matrix<T> instance, MatrixLog<T>? logger = null) where T : INumber<T> {
	    // 1. sort the matrix by leading pivot index,
	    // 2. look for pivots in the same index as {pivot} and row reduce,
	    // 3. go to next pivot and repeat until you reach row echelon form.
	
	    var (rows, cols) = instance.GetSize(); // bounds.

	    List<int> indices =  instance.Sort(); // sort the matrix - gives us our pivot buckets in indices.

        // row reduce
        for(int row = 0; row < (rows - 1); row++) { 
            for(int comp = (row + 1); comp < rows; comp++) {
                if(indices[row] == indices[comp]) {
                    instance.ReduceRow((comp, indices[comp]), (row, indices[row]), logger); 
                    indices[comp] = instance.FindPivot(comp).col; // keep indices updated - this returns -1 if you have a free variable.
                    // we log this in ReduceRow().
                }
            }
        }

        for(int row = 0; row < rows; row++) {
            if(indices[row] > -1) instance.ScaleRow((row, indices[row])); // this is technically the row and column of row's pivot.
            // we log this in ScaleRow().
        }
	}

    public static Matrix<T> GetRowEchelon<T>(this Matrix<T> instance, MatrixLog<T>? logger = null) where T : INumber<T> {
	    // 1. sort the matrix by leading pivot index,
	    // 2. look for pivots in the same index as {pivot} and row reduce,
	    // 3. go to next pivot and repeat until you reach row echelon form.
	
        var matrix = instance.Clone();
	    var (rows, cols) = instance.GetSize(); // bounds.
	    List<int> indices = new(); // for "bucket" sorting.

	    // sort the matrix
	    for(int row = 0; row < rows; row++) {
            // indices is the column of each row's index (this is a key-value map so we can iterate backwards without recalling FindPivot()).
             indices.Add((instance.FindPivot(row).col));
            // get the pivot index for the current row
            var pivot = indices[row];
		    // insertion sort - check each index of {indices} until we find a value greater than pivot => place pivot after
			for(int cursor = row; cursor > 0; cursor--) {
				if(pivot < indices[cursor - 1]) {
                    // keep our bucket accurate for later use
                    (indices[cursor], indices[cursor - 1]) = (indices[cursor - 1], indices[cursor]);
				    matrix.SwapRows(cursor, (cursor - 1));
                    // we log these steps inside of SwapRows().
                } else {
                    break;
                } 
		    }
		}

        // row reduce
        for(int row = 0; row < (rows - 1); row++) { 
            for(int comp = (row + 1); comp < rows; comp++) {
                if(indices[row] == indices[comp]) {
                    matrix.ReduceRow((comp, indices[comp]), (row, indices[row]), logger); // indices is our bucket, so indices[row] gives us the column.
                    indices[comp] = instance.FindPivot(comp).col; // this returns -1 if you have a free variable.
                    // we log these steps inside of ReduceRow().
                }
            }
        }

        for(int row = 0; row < rows; row++) {
            if(indices[row] > -1) matrix.ScaleRow((row, indices[row])); // this is technically the row and column of row's pivot.
            // we log these steps inside of ScaleRow(). 
        }

        return matrix;
	}

    public static Matrix<T> Clone<T>(this Matrix<T> instance) where T : INumber<T> {
        Matrix<T> matrix = new Matrix<T>(instance.Rows, instance.Cols);

        for(int row = 0; row < instance.Rows; row++) {
            for(int col = 0; col < instance.Cols; col++) {
                matrix[row, col] = instance[row, col];
            }
        }

        return matrix;
    }

    public static void SwapRows<T>(this Matrix<T> instance, int row1, int row2, MatrixLog<T>? logger = null) where T : INumber<T> {
        var rowLength = instance.GetSize().Cols;
        for(int col = 0; col < rowLength; col++) {
            T temp = instance.Get(row1, col);
            instance.Set(row1, col, instance.Get(row2, col));
            instance.Set(row2, col, temp);
        }
        if(logger is not null) logger.LogStep(new RowOperation<T>(RowOpKind.Swap, row1, row2));
    }

    public static (int row, int col) FindPivot<T>(this Matrix<T> instance, int row) where T : INumber<T> { 
        int col = 0;
	    while(col < instance.Cols) {
		    if(instance.Get(row, col) != T.Zero) {
			    return(row, col);
		    } else col++;
	    }
	    return (row, -1);
    }

    public static void ScaleRow<T>(this Matrix<T> instance, (int row, int pivot) target, MatrixLog<T>? logger = null) where T : INumber<T> {
        // scalar needs to be a value such that pivot * scalar = 1.
        T scalar = T.One / T.CreateChecked(instance.Get(target.row, target.pivot));
        for(int cursor = target.pivot; cursor < instance.Cols; cursor++) {
            T value = instance.Get(target.row, cursor) * scalar;
            instance.Set(target.row, cursor, value);
        }
        if(logger is not null) logger.LogStep(new RowOperation<T>(RowOpKind.Scale, r2: target.row, scalar));
    }

    // target is the row being augmented, pivot is the row we're basing off of for the elementary operation.
    public static void ReduceRow<T>(this Matrix<T> instance, (int row, int col) target, (int row, int col) pivot, MatrixLog<T>? logger = null) where T : INumber<T>{
	    // scalar needs to be a value such that pivot * scalar + target = 0, 
	    // thus the negation of the target divided by the pivot value, multiplied by the pivot value provides the negation of the target's value. 
        T scalar = -(instance.Get(target.row, target.col) / instance.Get(pivot.row, pivot.col));
        for(int cursor = 0; cursor < instance.Cols; cursor++) {
            T value = instance.Get(target.row, cursor) + (scalar * instance.Get(pivot.row, cursor));
            instance.Set(target.row, cursor, value);
            if(logger is not null) logger.LogStep(new RowOperation<T>(RowOpKind.AddScaled, target.row, pivot.row, scalar));
        }
    }

    public static List<int> Sort<T>(this Matrix<T> instance) where T : INumber<T> {
        // sort the matrix in place and return a list where index = row, value = pivot.
        List<int> indices = new();
	    for(int row = 0; row < instance.Rows; row++) {
            // indices is the column of each row's index (this is a key-value map so we can iterate backwards without recalling FindPivot()).
            int pivot = instance.FindPivot(row).col;
            if(pivot == -1) pivot = instance.Cols;
            indices.Add(pivot);
		    // online insertion - check each index of {indices} until we find a value greater than pivot => place pivot after.
            // if pivot == -1 then we want it to sit at the bottom.
            for(int cursor = row; cursor > 0; cursor--) {
		    	if(pivot < indices[cursor - 1]) {
                    // keep our bucket accurate for later use.
                    (indices[cursor], indices[cursor - 1]) = (indices[cursor - 1], indices[cursor]);
				    instance.SwapRows(cursor, (cursor - 1));
                    // we log this in SwapRows.
                } else {
                    break;
                } 
            }
        }
        return indices;
    }
}
