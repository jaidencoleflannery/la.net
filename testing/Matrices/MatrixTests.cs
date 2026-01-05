namespace Matrices.Tests;

public class MatrixTests
{
    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    [InlineData(4, 3)]
    [InlineData(14, 32)]
    public void Ctor_SetsRowsAndCols_Success(int rows, int cols) {
        Matrix<double> matrix = new Matrix<double>(rows, cols,
            new double[,] {
                {0, 4, 8}, 
                {0, 0, 8},
                {0, 1, 8},
            }
        );

        Assert.Equal(matrix.Rows, rows);
        Assert.Equal(matrix.Cols, cols);
    }

    [Theory]
    [InlineData(1, 3)]
    [InlineData(3, 1)]
    [InlineData(4, -3)]
    [InlineData(14, 0)]
    public void Ctor_DoesntSetBadRowsAndCols_Fail(int rows, int cols) {
        var data = new double[,] {
            {0, 4, 8}, 
            {0, 0, 8},
            {0, 1, 8},
        };

        Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix<double>(rows, cols, data));
    }

    [Fact]
    public void Ctor_WithData_StoresValuesProperly_Success() {
        Matrix<double> matrix = new Matrix<double>(3, 3,
            new double[,] {
                {0, 4, 8}, 
                {0, 0, 8},
                {0, 1, 8},
            }
        );

        Assert.Equal((matrix.Get(0, 1)), 4);
        Assert.Equal((matrix.Get(2, 2)), 8);
    }
}
