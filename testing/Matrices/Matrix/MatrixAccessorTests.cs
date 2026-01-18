namespace Matrices.Tests;

public class MatrixAccessorTests
{
    [Fact]
    public void Get_ReturnsAccurateData_Success() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix matrix = new Matrix(10, 10, data);

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
