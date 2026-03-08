namespace Matrices.Tests;

public class MatrixConstructorTests
{
    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    [InlineData(4, 3)]
    [InlineData(14, 32)]
    public void Ctor_SetsRowsAndCols_Success(int rows, int cols) {
        Matrix matrix = new Matrix(rows, cols,
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
    [InlineData(4, 0)]
    [InlineData(0, 4)]
    [InlineData(0, 0)]
    public void Ctor_DoesntSetTooSmallRowsAndCols_Fail(int rows, int cols) {
        var data = new double[,] {
            {0, 4, 8}, 
            {0, 0, 8},
            {0, 1, 8},
        };

        Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix(rows, cols, data));
    }

    [Theory]
    [InlineData(4, -3)]
    [InlineData(-4, 3)]
    [InlineData(-4, -3)]
    public void Ctor_DoesntSetNegativeRowsAndCols_Fail(int rows, int cols) {
        var data = new double[,] {
            {0, 4, 8}, 
            {0, 0, 8},
            {0, 1, 8},
        };

        Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix(rows, cols, data));
    }

    [Fact]
    public void Ctor_WithData_StoresValuesProperly_Success() {
        var data = new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix matrix = new Matrix(3, 3, data);

        for(int row = 0; row < matrix.Rows; row++) {
            for(int col = 0; col < matrix.Cols; col++) {
                Assert.Equal((matrix.Get(row, col)), data[row, col]);
            }
        }
    }

    [Fact]
    public void Ctor_NoData_CreatesIdentityMatrixProperly_Success() {
        Matrix matrix = new Matrix(3, 3);

        for(int row = 0; row < matrix.Rows; row++) {
            for(int col = 0; col < matrix.Cols; col++) {
                if(row == col) Assert.Equal((matrix.Get(row, col)), 1);
                if(row != col) Assert.Equal((matrix.Get(row, col)), 0);
            }
        }
    }

    [Fact]
    public void Ctor_ClonedCopyNotAugmentable_Success() {
        var data = new double[,] {
            {0, 9, 6},
            {8, 2, 1},
        };
        Matrix matrix = new Matrix(2, 3, data);
        data[0, 0] = 3;
        Assert.Equal(matrix[0, 0], 0);
    }

    [Fact]
    public void Ctor_WithData_MatrixConforms_Success() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix matrix = new Matrix(5, 5, data);

        for(int row = 0; row < 3; row++) {
            for(int col = 0; col < 3; col++) {
                Assert.Equal((matrix.Get(row, col)), data[row, col]);
            }
        }

        for(int row = 3; row < matrix.Rows; row++) {
            for(int col = 3; col < matrix.Cols; col++) {
                if(row == col) Assert.Equal((matrix.Get(row, col)), 1);
                if(row != col) Assert.Equal((matrix.Get(row, col)), 0);
            }
        }    
    } 
}
