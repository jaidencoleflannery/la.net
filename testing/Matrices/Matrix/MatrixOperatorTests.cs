namespace Matrices.Tests;

public class MatrixOperatorTests
{
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
}
