using System.Numerics;

namespace Matrices;
public static class MatrixOperations {

    public static void ToReducedRowEchelon<T>(this Matrix<T> instance) where T : INumber<T> {
        // 1. reduce to row echelon,
        // 2. rid of upper triangular values so instance becomes an identity matrix.
        instance.ToRowEchelon();

        for(int row = 0; row < (instance.Rows - 1); row++) {
            (int row, int col) pivot = instance.FindPivot(row + 1);
            if(pivot.col < 0) continue;
            // scalar needs to be a value such that (second row's pivot * scalar) + first row's pivot = 0.
            var (rowValue1, rowValue2) = (instance.Get(row, pivot.col), instance.Get((row + 1), pivot.col));
            T scalar = -(rowValue1 / rowValue2);
            // iterate through each value in second row and multiply by {scalar}, then add that value (which should be the negation of the first row's {col}) -
            // to the first row's {col}.
            for(int col = pivot.col; col < instance.Cols; col++) {
                instance.Set(row, col, ((scalar * rowValue2) + rowValue1));
            }
        }
    }

    public static void ToRowEchelon<T>(this Matrix<T> instance) where T : INumber<T> {
	    // 1. sort the matrix by leading pivot index,
	    // 2. look for pivots in the same index as {pivot} and row reduce,
	    // 3. go to next pivot and repeat until you reach row echelon form.
	
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
				    instance.SwapRows(cursor, (cursor - 1));
                } else {
                    break;
                } 
		    }
		}

        // row reduce
        for(int row = 0; row < (rows - 1); row++) { 
            for(int comp = (row + 1); comp < rows; comp++) {
                if(indices[row] == indices[comp]) {
                    instance.ReduceRow((comp, indices[comp]), (row, indices[row]));
                    indices[comp] = instance.FindPivot(comp).col; // this returns -1 if you have a free variable.
                }
            }
        }

        for(int row = 0; row < rows; row++) {
            if(indices[row] > -1) instance.ScaleRow((row, indices[row])); // this is technically the row and column of row's pivot.
        }
	}

    public static void SwapRows<T>(this Matrix<T> instance, int row1, int row2) where T : INumber<T> {
        var rowLength = instance.GetSize().Cols;
        for(int col = 0; col < rowLength; col++) {
            T temp = instance.Get(row1, col);
            instance.Set(row1, col, instance.Get(row2, col));
            instance.Set(row2, col, temp);
        }
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

    public static void ScaleRow<T>(this Matrix<T> instance, (int row, int pivot) target) where T : INumber<T> {
        // scalar needs to be a value such that pivot * scalar = 1.
        T scalar = T.One / T.CreateChecked(instance.Get(target.row, target.pivot));
        for(int cursor = target.pivot; cursor < instance.Cols; cursor++) {
            T value = instance.Get(target.row, cursor) * scalar;
            instance.Set(target.row, cursor, value);
        }
    }

    // target is the row being augmented, pivot is the row we're basing off of for the elementary operation.
    public static void ReduceRow<T>(this Matrix<T> instance, (int row, int col) target, (int row, int col) pivot) where T : INumber<T>{
	    // scalar needs to be a value such that pivot * scalar + target = 0, 
	    // thus the negation of the target divided by the pivot value, multiplied by the pivot value provides the negation of the target's value. 
        T scalar = -(instance.Get(target.row, target.col) / instance.Get(pivot.row, pivot.col));
        for(int cursor = 0; cursor < instance.Cols; cursor++) {
            T value = instance.Get(target.row, cursor) + (scalar * instance.Get(pivot.row, cursor));
            instance.Set(target.row, cursor, value);
        }
    }
}
