using System.Numerics;

namespace Matrices;
static class MatrixOperations {
    public static Matrix<T> RowReduce<T>(this Matrix<T> instance) where T : INumber<T> {
        var (rows, cols) = instance.GetSize();
        (int row, int col) pivot = instance.FindPivot(0);
        if(pivot.row != 0) {
            SwapRows(instance, 0, pivot.row);
        }
        (int row, int col) swap = instance.FindPivot(1);
        ReduceRow(instance, swap, pivot);
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
