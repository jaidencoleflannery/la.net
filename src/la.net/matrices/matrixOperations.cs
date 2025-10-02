using System.Numerics;

namespace Matrices;
static class MatrixOperations {
    public static Matrix<T> RowReduce<T>(this Matrix<T> instance) where T : INumber<T> {

        var (rows, cols) = instance.GetSize();
        //(int row, int col) pivot = (-1, -1);
        //(int row, int col) target = (-1, -1);

        instance.SortRows();
/*
        for(int row = 0; row < rows; row++) { 
            for(int col = 0; col < cols; col++) {
                if(instance.Get(row, col) != T.Zero) {
                    if(pivot == (-1, -1)) {
                        pivot = (row, col);
                        Console.WriteLine($"pivot at: {row}, {col} = {instance.Get(pivot.row, pivot.col)}");
                        break;
                    } else {
                        if(pivot.col < col) {
                            instance.SwapRows(pivot.row, row);
                            row--;
                            col = 0;
                        }
                        target = (row, col);
                        Console.WriteLine($"next val at: {row}, {col} = {instance.Get(target.row, target.col)}");
                        pivot = (-1, -1);
                        //instance.Set(row, col, instance.Get(pivot.row, pivot.col));
                    }
                }
            }
        }
        */
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

    private static void SortRows<T>(this Matrix<T> instance) where T : INumber<T> {
        for(int row = 0; row < instance.Rows; row++) {
            for(int col = 0; col < instance.Cols; col++) {
                if(row < instance.Rows - 1) {
                    if(!(instance.Get(row, col) == T.Zero) && instance.Get(row + 1, col) == T.Zero && (row < instance.Rows)) {
                        Console.WriteLine($"NOT swapping rows {row}{row + 1}");
                        break;
                    } else if (instance.Get(row, col) == T.Zero && !(instance.Get(row + 1, col) == T.Zero)){
                        Console.WriteLine($"Swapping rows {row}{row + 1}");
                        SwapRows(instance, row, row + 1); 
                        row--;
                        col = 0;
                        break;
                    }
                }
            }
        }
    }
}