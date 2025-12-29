using System.Numerics;

namespace Matrices;
static class MatrixOperations {
    public static void ToRowEchelon<T>(this Matrix<T> instance) where T : INumber<T> {
	var (rows, cols) = instance.GetSize();

	// find leading pivot, if it isnt row 0, swap row 0 and leading pivot's row.
        (int row, int col) pivot1 = instance.FindPivot(0);
	if(pivot1 == (-1, -1)) return; // no pivot found
        if(pivot1.row != 0) SwapRows(instance, 0, pivot1.row);
	int index = 1; // 0 was our first pivot.

	// look for pivots in the same index on each row and row reduce, then go to next pivot and repeat until you reach row echelon form.
	while(index < rows) {
		// next leading pivot.
		(int row, int col) pivot2 = instance.FindPivot(index);
		if(pivot2 == (-1, -1)) return;

		// if both pivots have the same column, row reduce.
		if(pivot1.col == pivot2.col) ReduceRow(instance, pivot2, pivot1);
		// if pivot1 comes before pivot2, recursively check if the next found pivot has a second pivot in the same index.
		else {
			pivot1 = pivot2;
			index = pivot1.row;
		}
	}
	return;
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
        uint cursor = 0;
	int pivot = -1;
	while(pivot == -1) {
		int entry = instance.Get(row, cursor);
		if(entry > 0) pivot = entry;
		else cursor++;
	}
	return (row, cursor);
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
