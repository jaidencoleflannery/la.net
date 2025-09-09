using System.Numerics;

namespace Matrices;
static class MatrixOperations {
    public static Matrix<T> RowReduce<T>(this Matrix<T> instance) where T : INumber<T> {
        var (rows, cols) = instance.GetSize();
        (int, int) pivot;
        for(int col = 0; col < cols; col++) {
            for(int row = 0; row < rows; row++) { 
                if(instance.Get(row, col) != T.Zero) {
                  pivot = (row, col);
                  Console.WriteLine(pivot);
                }
            }
        }
        return instance;
    }
}