namespace Matrices.Tests;

public class MatrixOperatorTests
{
    // comparisons

    // ==
    
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

        Assert.True((m == m));
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

    // !=
    
    [Fact]
    public void InequalityOperator_DataMatches_Fail() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(10, 10, data);
        Matrix<double> m2 = new Matrix<double>(10, 10, data);

        Assert.False(m1 != m2);
    }

    [Fact]
    public void InequalityOperator_Reflection_Fail() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m = new Matrix<double>(10, 10, data);

        Assert.False(m != m);
    }

    [Fact]
    public void InequalityOperator_BiDirectional_Fail() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(10, 10, data);
        Matrix<double> m2 = new Matrix<double>(10, 10, data);

        Assert.False((m2 == m1) != (m1 == m2));
    }

    [Fact]
    public void InequalityOperator_InverseCheck_Success() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(10, 10, data);
        Matrix<double> m2 = new Matrix<double>(10, 10, data);

        Assert.True((m2 != m1) != (m2 == m1));
    }

    [Fact]
    public void InequalityOperator_EqualsBehaviorComparison_Fail() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(10, 10, data);
        Matrix<double> m2 = new Matrix<double>(10, 10, data);

        Assert.False(m1.Equals(m2) != (m1 == m2));
    }

    [Fact]
    public void InequalityOperator_EqualsBehavior_Fail() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(6, 6, data);
        data =  new double[,] {
            {0, 4, 8}, 
            {0, 2, 8},
            {0, 1, 8},
        };
        Matrix<double> m2 = new Matrix<double>(6, 6, data);

        Assert.False(m1.Equals(m2));
    }

    [Fact]
    public void InequalityOperator_EqualsBehaviorOnDifferingSize_Fail() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(3, 3, data);
        data =  new double[,] {
            {0, 4, 8}, 
            {0, 2, 8},
            {0, 1, 8},
        };
        Matrix<double> m2 = new Matrix<double>(6, 6, data);

        Assert.False(m1.Equals(m2));
    }

    [Fact]
    public void InequalityOperator_DataNotMatching_Success() {
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

        Assert.False(m1 == m2);
    }

    [Fact]
    public void InequalityOperator_MatchingDataGivesMatchingHash_Fail() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(10, 10, data);
        Matrix<double> m2 = new Matrix<double>(10, 10, data);

        Assert.False(m1.GetHashCode() != m2.GetHashCode());
    }

    [Fact]
    public void InequalityOperator_NonMatchingDataGivesMatchingHash_Success() {
        var data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(4, 4, data);

        data =  new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 2, 8},
            };

        Matrix<double> m2 = new Matrix<double>(4, 4, data);

        Assert.True(m1.GetHashCode() != m2.GetHashCode());
    }

    // arithmetic
    
    // +
    
    [Fact]
    public void AdditionOperator_AddingTwoMatrices_Success() {
        var data = new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(3, 3, data);

        data = new double[,] {
            {0, 4, 8}, 
            {0, 3, 8},
            {0, 1, 8},
        };

        Matrix<double> m2 = new Matrix<double>(3, 3, data);

        data = new double[,] {
            {0, 8, 16},
            {0, 6, 16},
            {0, 2, 16},
        };

        Matrix<double> result = new Matrix<double>(3, 3, data);

        Assert.True((m1 + m2) == result);
    }

    [Fact]
    public void AdditionOperator_AddingTwoMatricesOfDifferingSize_Fail() {
        var data = new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(3, 3, data);

        data = new double[,] {
            {0, 4, 8}, 
            {0, 3, 8},
            {0, 1, 8},
            {0, 3, 8},
        };

        Matrix<double> m2 = new Matrix<double>(4, 3, data);

        Assert.Throws<ArgumentException>(() => m1 + m2);
    }

    [Fact]
    public void AdditionOperator_AddingNull_Fail() {
        var data = new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(3, 3, data);

        Matrix<double> m2 = null;

        Assert.Throws<ArgumentNullException>(() => m1 + m2);
    }

    // -

    [Fact]
    public void SubtractionOperator_SubtractingTwoMatrices_Success() {
        var data = new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(3, 3, data);

        data = new double[,] {
            {0, 4, 9}, 
            {0, 2, 8},
            {1, 1, 8},
        };

        Matrix<double> m2 = new Matrix<double>(3, 3, data);

        data = new double[,] {
            {0, 0, -1},
            {0, 1, 0},
            {-1, 0, 0},
        };

        Matrix<double> result = new Matrix<double>(3, 3, data);

        Assert.True((m1 - m2) == result);
    }

    [Fact]
    public void SubtractionOperator_SubtractingTwoMatricesOfDifferingSize_Fail() {
        var data = new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(3, 3, data);

        data = new double[,] {
            {0, 4, 8}, 
            {0, 3, 8},
            {0, 1, 8},
            {0, 3, 8},
        };

        Matrix<double> m2 = new Matrix<double>(4, 3, data);

        Assert.Throws<ArgumentException>(() => m1 - m2);
    }

    [Fact]
    public void SubtractionOperator_AddingNull_Fail() {
        var data = new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(3, 3, data);

        Matrix<double> m2 = null;

        Assert.Throws<ArgumentNullException>(() => m1 - m2);
    }

    // *
    
    [Fact]
    public void MultiplicationOperator_MultiplyingTwoMatrices_Success() {
        var data = new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(3, 3, data);

        data = new double[,] {
            {2, 1, 1}, 
            {0, 2, 1},
            {1, 0, 2},
        };

        Matrix<double> m2 = new Matrix<double>(3, 3, data);

        data = new double[,] {
            {8, 8, 20},
            {8, 6, 19},
            {8, 2, 17},
        };

        Matrix<double> result = new Matrix<double>(3, 3, data);

        Assert.True((m1 * m2) == result);
    }

    [Fact]
    public void MultiplicationOperator_MultiplyingTwoMatricesOfDifferingSize_Fail() {
        var data = new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(3, 3, data);

        data = new double[,] {
            {0, 4, 8}, 
            {0, 3, 8},
            {0, 1, 8},
            {0, 3, 8},
        };

        Matrix<double> m2 = new Matrix<double>(4, 3, data);

        Assert.Throws<ArgumentException>(() => m1 * m2);
    }

    [Fact]
    public void MultiplicationOperator_MultiplyingNull_Fail() {
        var data = new double[,] {
                {0, 4, 8}, 
                {0, 3, 8},
                {0, 1, 8},
            };

        Matrix<double> m1 = new Matrix<double>(3, 3, data);

        Matrix<double> m2 = null;

        Assert.Throws<ArgumentNullException>(() => m1 * m2);
    }

}
