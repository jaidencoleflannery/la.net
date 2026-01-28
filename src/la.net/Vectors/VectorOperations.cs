using Matrices;
namespace Vectors;

public static class VectorOperations {

    public static Vector Add(Vector a, Vector b) {
        if(a.Dimension != b.Dimension) throw new ArgumentException("Vector dimensions must match for vector addition.");
        double[] vector = new double[a.Dimension];
        for(int cursor = 0; cursor < a.Dimension; cursor++) vector[cursor] = a[cursor] + b[cursor];
        return new Vector(vector);
    }

    public static Vector Subtract(Vector a, Vector b) {
        if(a.Dimension != b.Dimension) throw new ArgumentException("Vector dimensions must match for vector subtraction.");
        double[] vector = new double[a.Dimension];
        for(int cursor = 0; cursor < a.Dimension; cursor++) vector[cursor] = a[cursor] - b[cursor];
        return new Vector(vector);
    } 

    public static Vector Scale(Vector a, double scalar) {
        double[] vector = new double[a.Dimension];
        for(int cursor = 0; cursor < a.Dimension; cursor++) vector[cursor] = a[cursor] * scalar;
        return new Vector(vector);
    } 

    public static Vector Scale(Vector a, Matrix matrix) {
        if(matrix.Cols != a.Dimension) throw new ArgumentException($"Cannot multiply a vector by a matrix unless the vectors number of rows matches the matrix's number of columns.");
        double[] vector = new double[matrix.Rows]; // after multiplication, the resulting vector of a mxn matrix will be m dimensions.
        for(int row = 0; row < matrix.Rows; row++) {
            for(int col = 0; col < matrix.Cols; col++) {
                vector[row] += a[col] * matrix[row, col];
            }
        }
        return new Vector(vector);
    }

    public static double Dot(Vector a, Vector b) {
        if(a.Dimension != b.Dimension) throw new ArgumentException($"Cannot find the dot product of matrices with differing dimensions.");
        double dotProduct = 0.0;
        for(int cursor = 0; cursor < a.Dimension; cursor++) {
            dotProduct += (a[cursor] * b[cursor]);
        }
        return dotProduct;
    }

}
