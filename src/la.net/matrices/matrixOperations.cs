using System.Numerics;

namespace Matrices;
static class MatrixOperations {
    public static Matrix<T> RowReduce<T>(this Matrix<T> instance) where T : INumber<T> {

        var (rows, cols) = instance.GetSize();
        (int row, int col) pivot = (-1, -1);
        (int row, int col) target = (-1, -1);

        for(int row = 0; row < rows; row++) { 
            for(int col = 0; col < cols; col++) {
                if(instance.Get(row, col) != T.Zero) {
                    if(pivot == (-1, -1)) {
                        pivot = (row, col);
                        Console.WriteLine($"pivot at: {row}, {col} = {instance.Get(pivot.row, pivot.col)}");
                        break;
                    } else {
                        if(pivot.row < row) {
                            instance.SwapRows(pivot.row, row);
                        }
                        target = (row, col);
                        Console.WriteLine($"next val at: {row}, {col} = {instance.Get(target.row, target.col)}");
                        pivot = (-1, -1);
                        //instance.Set(row, col, instance.Get(pivot.row, pivot.col));
                        break;
                    }
                }
            }
        }
        return instance;
    }

    private static void SwapRows<T>(this Matrix<T> instance, int row1, int row2) where T : INumber<T> {
        var rowLength = instance.GetSize().Rows;
            for(int col = 0; col < rowLength; col++) {
                T temp = instance.Get(row1, col);
                instance.Set(row1, col, instance.Get(row2, col));
                instance.Set(row2, col, temp);
            }
        }
}