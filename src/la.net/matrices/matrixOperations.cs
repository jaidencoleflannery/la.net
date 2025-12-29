using System.Numerics;

namespace Matrices;
static class MatrixOperations {
    public static Matrix<T> ToRowEchelon<T>(this Matrix<T> instance) where T : INumber<T> {
	var (rows, cols) = instance.GetSize();

	// find leading pivot, if it isnt row 0, swap row 0 and leading pivot's row.
        (int Row, int Col) pivot1 = instance.FindPivot(0);
        if(pivot1.Row != 0) SwapRows(instance, 0, pivot1.Row);
	int index = 1; // 0 was our first pivot.

	// look for pivots in the same index on each row and row reduce, then go to next pivot and repeat until you reach row echelon form.
	while(true) {
		// next leading pivot.
		(int row, int col) pivot2 = instance.FindPivot(index);
		if(pivot2 == null) break;

		// if both pivots have the same column, row reduce.
		if(pivot1.Col == pivot2.Col) ReduceRow(instance, pivot2, pivot1);
		// if pivot1 comes before pivot2, recursively check if the next found pivot has a second pivot in the same index.
		else {
			pivot1 = pivot2;
			index = pivot.Row;
		}

		if(index == rows) break;
	}
	Console.WriteLine(instance);
	return instance;
    }

    public static void SwapRows<T>(this Matrix<T> instance, int row1, int row2) where T : INumber<T> {
        var rowLength = instance.GetSize().Cols;
        for(int col = 0; col < rowLength; col++) {
            T temp = instance.Get(row1, col);
            instance.Set(row1, col, instance.Get(row2, col));
            instance.Set(row2, col, temp);
        }
    }

    public static (int row, int col) FindPivot<T>(this Matrix<T> instance, int initRow) where T : INumber<T> {
        if(initRow >= instance.Rows) {
            throw new ArgumentOutOfRangeException(nameof(initRow), $"{nameof(initRow)} must be less than the total number of rows.");
        }
        int[] numZeroes = new int[instance.Rows];
        int pivotRow = 0;
        // if the first row has a leading value, then we can call it the pivot
        if(instance.Get(initRow, 0) != T.Zero) {
            return (initRow, 0);
        }
        // else let's find the row with the least leading zeroes
        for(int row = initRow; row < instance.Rows; row++) {
            for(int col = 0; col < instance.Cols; col++) {
                if(instance.Get(row, col) == T.Zero) {
                    numZeroes[row]++;
                } else {
                    break;
                }
            }
        }
        for(int cursor = 0; cursor < numZeroes.Length; cursor++) {
            if(numZeroes[cursor] < numZeroes[pivotRow]) {
                pivotRow = cursor;
            }
        }
        // numZeroes.Length will be the first index after leading zeroes (pivot).
        return (pivotRow, numZeroes[pivotRow]);
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
