namespace Matrices.Tests;

public class MatrixMethodsTests
{

    // GetRow(int row)

    [Fact]
    public void GetRowReturnsAccurateRow() {
        var data = new double[,] {{3, 4, 5, 8}};
        Matrix matrix = new Matrix(1, 4, data);
        Assert.Equal(data.Cast<double>().ToArray(), matrix.GetRow(0));
    }

    [Fact]
    public void GetRowThrowsWhenIndexedOutOfBounds() {
        var data = new double[,] {{3, 4, 5, 8}};
        var matrix = new Matrix(1, 4, data);
        Assert.Throws<IndexOutOfRangeException>(() => matrix.GetRow(1));
        Assert.Throws<IndexOutOfRangeException>(() => matrix.GetRow(-1));
    }

    // SetRow(int row, double[] values, bool conform = false)

    [Fact]
    public void SetRowAccuratelySetsRow() {
        var data = new double[,] {  
            {3, 4, 5, 8},
            {3, 4, 5, 4}
        };
        var matrix = new Matrix(2, 4, data);
        var insert = new double[] {0, 4, 9, 2};
        matrix.SetRow(0, insert);
        Assert.Equal(data.Cast<double>().ToArray(), matrix.GetRow(0));
    }

    [Fact]
    public void SetRowThrowsForLength() { 
        var matrix = new Matrix(2, 2);
        var insert = new double[] {0, 4, 9};
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix.SetRow(1, insert));
    }

    [Fact]
    public void ConformSetRowThrowsForLength() {
        var matrix = new Matrix(2, 2);
        var insert = new double[] {0, 4, 9};
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix.SetRow(1, insert, true));
    }

    [Fact]
    public void ConformSetSmallRowAccuratelySetsRow() {
        var data = new double[,] {  
            {3, 4, 5},
            {3, 4, 5}
        };
        var matrix = new Matrix(2, 3, data);
        var insert = new double[] {0, 4};
        matrix.SetRow(0, insert);
        Assert.Equal(new Double[] {0, 4, 1}, matrix.GetRow(0));
    }

    [Fact]
    public void SetRowBadIndiceThrows() {
        var data = new double[,] {  
            {3, 4, 5},
            {3, 4, 5}
        };
        var matrix = new Matrix(2, 3, data);
        var insert = new double[] {0, 4, 9};
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix.SetRow(9, insert));
    }

    [Fact]
    public void ConformSetRowToLargeMatrixAccuratelySetsRow() {
        var data = new double[,] {  
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},        
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
        };
        var matrix = new Matrix(8, 8, data);
        var insert = new double[] {1};
        matrix.SetRow(7, insert, true);
        Assert.Equal(new double[] {1, 0, 0, 0, 0, 0, 0, 1}, matrix.GetRow(7));
    }

    [Fact]
    public void NoConformSetSmallRowToLargeMatrixThrows() {
        var data = new double[,] {  
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},        
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
        };
        var matrix = new Matrix(8, 8, data);
        var insert = new double[] {1}; 
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix.SetRow(7, insert));
    }

    // PushRow(double[] values, bool conform = false)
     
    [Fact]
    public void PushRowAccuratelyPushesRow() {
        var data = new double[,] {  
            {3, 4, 5, 8},
            {3, 4, 5, 4}
        };
        var matrix = new Matrix(2, 4, data);
        var insert = new double[] {0, 4, 9, 2};
        matrix.PushRow(insert);
        Assert.Equal(data.Cast<double>().ToArray(), matrix.GetRow(0));
    }

    [Fact]
    public void PushRowThrowsForLength() { 
        var matrix = new Matrix(2, 2);
        var insert = new double[] {0, 4, 9};
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix.PushRow(insert));
    }

    [Fact]
    public void ConformPushRowThrowsForLength() {
        var matrix = new Matrix(2, 2);
        var insert = new double[] {0, 4, 9};
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix.PushRow(insert, true));
    }

    [Fact]
    public void ConformPushSmallRowAccuratelyPushesRow() {
        var data = new double[,] {  
            {3, 4, 5},
            {3, 4, 5}
        };
        var matrix = new Matrix(2, 3, data);
        var insert = new double[] {4};
        matrix.PushRow(insert, true);
        Assert.Equal(new Double[] {4, 0, 0}, matrix.GetRow(0));
    }
 

    [Fact]
    public void ConformPushRowToLargeMatrixAccuratelyPushesRow() {
        var data = new double[,] {  
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},        
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
        };
        var matrix = new Matrix(8, 8, data);
        var insert = new double[] {0, 1};
        matrix.PushRow(insert, true);
        Assert.Equal(new double[] {0, 1, 0, 0, 0, 0, 0, 0}, matrix.GetRow(0));
    }

    [Fact]
    public void NoConformPushSmallRowToLargeMatrixThrows() {
        var data = new double[,] {  
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},        
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
        };
        var matrix = new Matrix(8, 8, data);
        var insert = new double[] {1}; 
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix.PushRow(insert));
    }

    // AppendRow(double[] values, bool conform = false)
    
    [Fact]
    public void AppendRowAccuratelyAppendsRow() {
        var data = new double[,] {  
            {3, 4, 5, 8},
            {3, 4, 5, 4}
        };
        var matrix = new Matrix(2, 4, data);
        var insert = new double[] {0, 4, 9, 2};
        matrix.AppendRow(insert);
        Assert.Equal(data.Cast<double>().ToArray(), matrix.GetRow(matrix.Rows - 1));
    }

    [Fact]
    public void AppendRowThrowsForLength() { 
        var matrix = new Matrix(2, 2);
        var insert = new double[] {0, 4, 9};
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix.AppendRow(insert));
    }

    [Fact]
    public void ConformAppendRowThrowsForLength() {
        var matrix = new Matrix(2, 2);
        var insert = new double[] {0, 4, 9};
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix.AppendRow(insert, true));
    }

    [Fact]
    public void ConformAppendSmallRowAppends() {
        var data = new double[,] {  
            {3, 4, 5},
            {3, 4, 5}
        };
        var matrix = new Matrix(2, 3, data);
        var insert = new double[] {0, 4};
        matrix.SetRow(0, insert);
        Assert.Equal(new Double[] {0, 4, 1}, matrix.GetRow(matrix.Rows - 1));
    }

    [Fact]
    public void ConformAppendRowToLargeMatrixAppends() {
        var data = new double[,] {  
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},        
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
        };
        var matrix = new Matrix(8, 8, data);
        var insert = new double[] {1};
        matrix.AppendRow(insert, true);
        Assert.Equal(new double[] {1, 0, 0, 0, 0, 0, 0, 1}, matrix.GetRow(7));
    }

    [Fact]
    public void NoConformAppendSmallRowToLargeMatrixThrows() {
        var data = new double[,] {  
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},        
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},
        };
        var matrix = new Matrix(8, 8,data);
        var insert = new double[] {1}; 
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix.AppendRow(insert));
    }

    [Fact]
    public void ToStringOutputIsCorrect() {
        var data = new double[,] {  
            {3, 4, 5, 4, 5, 6, 8, 7},
            {3, 4, 5, 4, 5, 6, 8, 7},        
            {3, 4, 5, 4, 5, 6, 8, 7},
        };
        Matrix matrix = new Matrix(3, 8, data);
        string str = 
            "| 3 4 5 4 5 6 8 7 |\n| 3 4 5 4 5 6 8 7 |\n| 3 4 5 4 5 6 8 7 |";
        Assert.Equal(str, matrix.ToString());
    }

    [Fact]
    public void ToStringRoundedOutputIsCorrect() {
        var data = new double[,] {  
            {3.0000000001, 4.1, 5.4, 4.1, 5.88, 6.1, 8.45, 7.333},
            {3.220, 4.41, 5.9454, 4.4531, 5.188, 6.1, 8.4455, 7.983333},
            {3, 4, 5, 4, 5, 6, 8, 7},
        };
        Matrix matrix = new Matrix(3, 8, data);
        string str = 
            "| 3 4 5 4 5 6 8 7 |\n| 3 4 5 4 5 6 8 7 |\n| 3 4 5 4 5 6 8 7 |";
        Assert.Equal(str, matrix.ToString());
    }

}
    
