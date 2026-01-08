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
    [InlineData(4, 0)]
    [InlineData(0, 4)]
    [InlineData(0, 0)]
    public void Ctor_DoesntSetTooSmallRowsAndCols_Fail(int rows, int cols) {
        var data = new double[,] {
            {0, 4, 8}, 
            {0, 0, 8},
            {0, 1, 8},
        };

        Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix<double>(rows, cols, data));
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

        Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix<double>(rows, cols, data));
    }

    [Fact]
    public void Ctor_WithData_StoresValuesProperly_Success() {
        var data = new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> matrix = new Matrix<double>(3, 3, data);

        for(int row = 0; row < matrix.Rows; row++) {
            for(int col = 0; col < matrix.Cols; col++) {
                Assert.Equal((matrix.Get(row, col)), data[row, col]);
            }
        }
    }

    [Fact]
    public void Ctor_NoData_CreatesIdentityMatrixProperly_Success() {
        Matrix<double> matrix = new Matrix<double>(3, 3);

        for(int row = 0; row < matrix.Rows; row++) {
            for(int col = 0; col < matrix.Cols; col++) {
                if(row == col) Assert.Equal((matrix.Get(row, col)), 1);
                if(row != col) Assert.Equal((matrix.Get(row, col)), 0);
            }
        }
    }

    [Fact]
    public void Ctor_WithData_MatrixConforms_Success() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> matrix = new Matrix<double>(5, 5, data);

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

    [Fact]
    public void ComparisonOperator_DataMatches_Success() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(10, 10, data);
        Matrix<double> m2 = new Matrix<double>(10, 10, data);

        bool response = (m1 == m2);
        Assert.True(response);
    }

    [Fact]
    public void ComparisonOperator_Reflection_Success() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m = new Matrix<double>(10, 10, data);

        bool response = (m == m);
        Assert.True(response);
    }

    [Fact]
    public void ComparisonOperator_BiDirectional_Success() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(10, 10, data);
        Matrix<double> m2 = new Matrix<double>(10, 10, data);

        Assert.True((m2 == m1) == (m1 == m2));
    }

    [Fact]
    public void ComparisonOperator_InverseCheck_Success() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(10, 10, data);
        Matrix<double> m2 = new Matrix<double>(10, 10, data);

        Assert.True((m2 == m1) != (m2 != m1));
    }

    [Fact]
    public void ComparisonOperator_EqualsBehavior_Success() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(10, 10, data);
        Matrix<double> m2 = new Matrix<double>(10, 10, data);

        Assert.True(m1.Equals(m2) == (m1 == m2));
    }

    [Fact]
    public void ComparisonOperator_EqualsBehavior_Fail() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(10, 10, data);
        data =  new double[,] {
            {0, 4, 8}, 
            {0, 2, 8},
            {0, 1, 8},
        };
        Matrix<double> m2 = new Matrix<double>(10, 10, data);

        Assert.False(m1.Equals(m2));
    }

    [Fact]
    public void ComparisonOperator_DataNotMatching_Failure() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(10, 10, data);

        data = new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {1, 1, 8},
            };

        Matrix<double> m2 = new Matrix<double>(10, 10, data);

        bool response = (m1 == m2);
        Assert.False(response);
    }

    [Fact]
    public void ComparisonOperator_MatchingDataGivesMatchingHash_Success() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(10, 10, data);
        Matrix<double> m2 = new Matrix<double>(10, 10, data);

        Assert.True(m1.GetHashCode() == m2.GetHashCode());
    }

    [Fact]
    public void Get_ReturnsAccurateData_Success() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> matrix = new Matrix<double>(10, 10, data);

        for(int row = 0; row < 3; row++) {
            for(int col = 0; col < 3; col++) {
                Assert.Equal((matrix.Get(row, col)), data[row, col]);
            }
        }

        for(int row = matrix.Rows; row < 10; row++) {
            for(int col = matrix.Cols; col < 10; col++) {
                if(row == col) Assert.Equal((matrix.Get(row, col)), 1);
                if(row != col) Assert.Equal((matrix.Get(row, col)), 0);
            }
        }
    }
}
