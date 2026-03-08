namespace Matrices.Tests;

public class MatrixOperationsGetInverseTests {

    // get inverse
    
    [Fact]
    public void GetInverse_ReturnsInverseOfMatrix_Success() {
        var identityData = new double[,] {
                {1, 0, 0}, 
                {0, 1, 0},
                {0, 0, 1},
            };

        Matrix identity = new Matrix(3, 3, identityData);

        var matrixData = new double[,] {
                {1, 4, 8}, 
                {0, 0, 8},
                {0, 1, 7},
            };

        Matrix matrix = new Matrix(3, 3, matrixData);
        Matrix inverse = matrix.GetInverse();

        Assert.Equal(identity, (matrix * inverse)); 
        Assert.Equal(identity, (inverse * matrix));
    }


    [Fact]
    public void GetInverse_ThrowsOnNonInvertibleMatrix_Failure() {
        var data = new double[,] {
                {1, 0, 0}, 
                {0, 8, 0},
                {0, 0, 0},
            };

        Matrix noInverseMatrix = new Matrix(3, 3, data);
        Assert.Throws<ArgumentException>(() => noInverseMatrix.GetInverse());

        data = new double[,] {
                {1, 0, 0, 0}, 
                {0, 8, 0, 0},
                {0, 0, 1, 0},
            };

        noInverseMatrix = new Matrix(3, 4, data);
        Assert.Throws<ArgumentException>(() => noInverseMatrix.GetInverse());
    }
}
