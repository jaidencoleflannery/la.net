namespace Matrices.Tests;

public class MatrixOperationsReducedRowEchelon {

    // reduced row echelon
    
    [Fact]
    public void ToRRE_AugmentsRREFormOfMatrix_Success() {
        var data =  new double[,] {
                {0, 4, 8, 2, 7}, 
                {9, 3, 8, 4, 6},
                {0, 1, 8, 3, 4},
                {1, 3, 8, 6, 6},
                {9, 8, 8, 1, 5},
            };

        Matrix<double> matrix = new Matrix<double>(5, 5, data);
        matrix.ToReducedRowEchelon();

        for(int row = matrix.Rows; row < 5; row++) {
            for(int col = matrix.Cols; col < 5; col++) {
                if(row == col) Assert.Equal((matrix[row, col]), 1);
                if(row != col) Assert.Equal((matrix[row, col]), 0);
            }
        }
    }

    [Fact]
    public void RRE_ReturnsRREFormOfMatrix_Success() {
        var data =  new double[,] {
                {0, 4, 8, 2, 7}, 
                {9, 3, 8, 4, 6},
                {0, 1, 8, 3, 4},
                {1, 3, 8, 6, 6},
                {9, 8, 8, 1, 5},
            };

        Matrix<double> matrix = new Matrix<double>(5, 5, data);
        var m = matrix.ReducedRowEchelon();

        for(int row = matrix.Rows; row < 5; row++) {
            for(int col = matrix.Cols; col < 5; col++) {
                if(row == col) Assert.Equal((m[row, col]), 1);
                if(row != col) Assert.Equal((m[row, col]), 0);
            }
        }
    }

    // row echelon
    
    [Fact]
    public void RE_ReturnsREFormOfMatrix_Success() {
        var data =  new double[,] {
                {0, 4}, 
                {9, 3},
            };

        Matrix<double> matrix = new Matrix<double>(5, 5, data);
        var m = matrix.GetRowEchelon();

        Assert.Equal(1, m[0, 0]);
        Assert.Equal(1.0/3.0, m[0, 1]);
    } 
}
