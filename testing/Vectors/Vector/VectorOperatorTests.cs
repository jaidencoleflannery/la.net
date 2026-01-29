using Vectors;

namespace Vectors.Tests;

public class VectorOperatorTests
{
    // comparisons

    // ==
    
    [Fact]
    public void ComparisonOperator_DataMatches_Success() {
        var data = new double[] { 0, 4, 8 };

        Vector v1 = new Vector(data);
        Vector v2 = new Vector(data);

        Assert.True(v1 == v2);
    }

    [Fact]
    public void ComparisonOperator_Reflection_Success() {
        var data = new double[] { 0, 4, 8 };

        Vector v1 = new Vector(data);
        Vector v2 = new Vector(data);

        Assert.True(v2 == v1);
    }

    [Fact]
    public void ComparisonOperator_BiDirectional_Success() {
        var data = new double[] { 0, 4, 8 };

        Vector v1 = new Vector(data);
        Vector v2 = new Vector(data);

        Assert.True((v2 == v1) == (v1 == v2));
    }

    [Fact]
    public void ComparisonOperator_InverseCheck_Success() {
        var data = new double[] { 0, 4, 8 };

        Vector v1 = new Vector(data);
        Vector v2 = new Vector(data); 

        Assert.True((v2 == v1) != (v2 != v1));
    }

    [Fact]
    public void ComparisonOperator_EqualsBehavior_Success() { 
        var data = new double[] { 0, 4, 8 };

        Vector v1 = new Vector(data);
        Vector v2 = new Vector(data);

        Assert.True(v1.Equals(v2) == (v1 == v2));
    }

    [Fact]
    public void ComparisonOperator_EqualsBehavior_Fail() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);

        data[2] = 4;
        Vector v2 = new Vector(data);

        Assert.False(v1.Equals(v2));
    }

    [Fact]
    public void ComparisonOperator_DataNotMatching_Failure() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);

        data[2] = 4;
        Vector v2 = new Vector(data);

        Assert.False(v1 == v2);
    }

    [Fact]
    public void ComparisonOperator_MatchingDataGivesMatchingHash_Success() {
        var data = new double[] { 0, 4, 8 };

        Vector v1 = new Vector(data);
        Vector v2 = new Vector(data);

        Assert.True(v1.GetHashCode() == v2.GetHashCode());
    }

    // !=
    
    [Fact]
    public void InequalityOperator_DataMatches_Fail() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);

        data[2] = 4;
        Vector v2 = new Vector(data);

        Assert.False(v1 != v2);
    }

    [Fact]
    public void InequalityOperator_Reflection_Fail() {
        var data = new double[] { 0, 4, 8 };
        Vector v = new Vector(data);

        Assert.False(v != v);
    }

    [Fact]
    public void InequalityOperator_BiDirectional_Fail() {
        var data = new double[] { 0, 4, 8 };

        Vector v1 = new Vector(data);
        Vector v2 = new Vector(data);

        Assert.False((v2 == v1) != (v1 == v2));
    }

    [Fact]
    public void InequalityOperator_InverseCheck_Success() {
        var data = new double[] { 0, 4, 8 };

        Vector v1 = new Vector(data);
        Vector v2 = new Vector(data);

        Assert.True((v2 != v1) != (v2 == v1));
    }

    [Fact]
    public void InequalityOperator_EqualsBehaviorComparison_Fail() {
        var data = new double[] { 0, 4, 8 };

        Vector v1 = new Vector(data);
        Vector v2 = new Vector(data);

        Assert.False(v1.Equals(v2) != (v1 == v2));
    }

    [Fact]
    public void InequalityOperator_EqualsBehavior_Fail() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);

        data[2] = 4;
        Vector v2 = new Vector(data);

        Assert.False(v1.Equals(v2));
    }

    [Fact]
    public void InequalityOperator_EqualsBehaviorOnDifferingSize_Fail() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);

        data[2] = 4;
        Vector v2 = new Vector(data);

        Assert.False(v1.Equals(v2));
    }

    [Fact]
    public void InequalityOperator_DataNotMatching_Success() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);

        data[2] = 4;
        Vector v2 = new Vector(data);

        Assert.False(v1 == v2);
    }

    [Fact]
    public void InequalityOperator_MatchingDataGivesMatchingHash_Fail() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);        
        data[2] = 8;
        Vector v2 = new Vector(data); 

        Assert.False(v1.GetHashCode() != v2.GetHashCode());
    }

    [Fact]
    public void InequalityOperator_NonMatchingDataGivesMatchingHash_Success() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);        

        data[2] = 7;
        Vector v2 = new Vector(data);

        Assert.True(v1.GetHashCode() != v2.GetHashCode());
    }

    // arithmetic
    
    // +
    
    [Fact]
    public void AdditionOperator_AddingTwoVectors_Success() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);        
        Vector v2 = new Vector(data);

        data = new double[] { 0, 8, 16 };
        Vector result = new Vector(data);

        Assert.True((v1 + v2) == result);
    }

    [Fact]
    public void AdditionOperator_AddingTwoVectorsOfDifferingSize_Fail() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);        
        data = new double[] { 0, 4, 8, 9 };
        Vector v2 = new Vector(data);

        Assert.Throws<ArgumentException>(() => v1 + v2);
    }

    [Fact]
    public void AdditionOperator_AddingNull_Fail() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);        

        Vector v2 = null;

        Assert.Throws<ArgumentNullException>(() => v1 + v2);
    }

    // -

    [Fact]
    public void SubtractionOperator_SubtractingTwoVectors_Success() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);        

        data = new double[] { 0, 4, 2 };
        Vector v2 = new Vector(data);

        data = new double[] { 0, 0, 6 };

        Vector result = new Vector(data);

        Assert.True((v1 - v2) == result);
    }

    [Fact]
    public void SubtractionOperator_SubtractingTwoVectorsOfDifferingSize_Fail() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);        

        data = new double[] { 0, 4, 2, 8 };
        Vector v2 = new Vector(data);

        Assert.Throws<ArgumentException>(() => v1 - v2);
    }

    [Fact]
    public void SubtractionOperator_AddingNull_Fail() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);        

        Vector v2 = null;

        Assert.Throws<ArgumentNullException>(() => v1 - v2);
    }

    // *
    
    [Fact]
    public void ScaleOperator_ScalingVector_Success() {
        var data = new double[] { 0, 4, 8 };
        Vector v1 = new Vector(data);         

        var result = new Vector(new double[] { 0, 8, 16 });

        Assert.Equal(result, (v1 * 2));
    } 
    
    // array indexing
    
    [Fact]
    public void IndexOperator_GetValue_Success() {
        var data = new double[] { 0, 4, 8 };
        Vector v = new Vector(data);

        Assert.Equal(4, v[1]);
    }

    [Fact]
    public void IndexOperator_SetValue_Success() {
        var data = new double[] { 0, 4, 8 };
        Vector v = new Vector(data);

        v[1] = 9;

        Assert.Equal(9, v[1]);
    }
}
