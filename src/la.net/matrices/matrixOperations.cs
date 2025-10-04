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
        reduceRow(instance, swap, pivot);
        return instance;
    }

    private static void SwapRows<T>(this Matrix<T> instance, int row1, int row2) where T : INumber<T> {
        var rowLength = instance.GetSize().Cols;
        for(int col = 0; col < rowLength; col++) {
            T temp = instance.Get(row1, col);
            instance.Set(row1, col, instance.Get(row2, col));
            instance.Set(row2, col, temp);
        }
    }

    private static (int row, int col) FindPivot<T>(this Matrix<T> instance, int initRow) where T : INumber<T> {
        if(initRow == instance.Rows) {
            throw new ArgumentOutOfRangeException(nameof(initRow), $"{nameof(initRow)} must be less than the total number of rows.");
        }
        int[] numZeroes = [];
        int pivotRow = 0;
        for(int row = initRow; row < instance.Rows; row++) {
            for(int col = 0; col < instance.Cols; col++) {
                if(instance.Get(row, col) == T.Zero) {
                    numZeroes[row]++;
                } else {
                    break;
                }
            }
        }
        for(int cursor = 1; cursor < (numZeroes.Length - 1); cursor++) {
            if(numZeroes[cursor] < numZeroes[pivotRow]) {
                pivotRow = cursor;
            }
        }
        // numZeroes.Length will be the first index after leading zeroes (pivot)
        return (pivotRow, numZeroes.Length);
    }

    private static void reduceRow<T>(this Matrix<T> instance, (int row, int col) swap, (int row, int col) pivot) where T : INumber<T>{
        T scalar = -instance.Get(pivot.row, pivot.col) / instance.Get(swap.row, swap.col);
        for(int cursor = 0; cursor < instance.Rows; cursor++) {
            T value = instance.Get(swap.row, swap.col) - scalar * instance.Get(pivot.row, cursor);
            instance.Set(swap.row, swap.col, value);
        }
    }
}