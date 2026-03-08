using Vectors;
using Matrices;
using static Vectors.VectorOperations;

namespace Vectors.Tests;

public class VectorOperationsTests
{
    // Dot Product Tests

    [Fact]
    public void Dot_BasicCalculation_Success() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        var v2 = new Vector(new double[] { 4, 5, 6 });

        double result = Dot(v1, v2);

        Assert.Equal(32, result);
    }

    [Fact]
    public void Dot_CommutativeProperty_Success() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        var v2 = new Vector(new double[] { 4, 5, 6 });

        Assert.Equal(Dot(v1, v2), Dot(v2, v1));
    }

    [Fact]
    public void Dot_OrthogonalVectors_Success() {
        var v1 = new Vector(new double[] { 1, 0, 0 });
        var v2 = new Vector(new double[] { 0, 1, 0 });

        double result = Dot(v1, v2);

        Assert.Equal(0, result);
    }

    [Fact]
    public void Dot_IdenticalVectors_Success() {
        var v1 = new Vector(new double[] { 3, 4 });

        double result = Dot(v1, v1);

        Assert.Equal(25, result);
    }

    [Fact]
    public void Dot_ZeroVector_Success() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        var v2 = new Vector(3);

        double result = Dot(v1, v2);

        Assert.Equal(0, result);
    }

    [Fact]
    public void Dot_NegativeValues_Success() {
        var v1 = new Vector(new double[] { -1, -2, -3 });
        var v2 = new Vector(new double[] { 4, 5, 6 });

        double result = Dot(v1, v2);

        Assert.Equal(-32, result);
    }

    [Fact]
    public void Dot_SingleDimension_Success() {
        var v1 = new Vector(new double[] { 5 });
        var v2 = new Vector(new double[] { 7 });

        double result = Dot(v1, v2);

        Assert.Equal(35, result);
    }

    [Fact]
    public void Dot_LargeDimension_Success() {
        var v1 = new Vector(1000);
        var v2 = new Vector(1000);
        for (int i = 0; i < 1000; i++) {
            v1[i] = 1;
            v2[i] = 1;
        }

        double result = Dot(v1, v2);

        Assert.Equal(1000, result);
    }

    [Fact]
    public void Dot_MismatchedDimensions_Throws() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        var v2 = new Vector(new double[] { 4, 5 });

        Assert.Throws<ArgumentException>(() => Dot(v1, v2));
    }

    [Fact]
    public void Dot_NullFirstVector_Throws() {
        Vector v1 = null;
        var v2 = new Vector(new double[] { 1, 2, 3 });

        Assert.Throws<ArgumentNullException>(() => Dot(v1, v2));
    }

    [Fact]
    public void Dot_NullSecondVector_Throws() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        Vector v2 = null;

        Assert.Throws<ArgumentNullException>(() => Dot(v1, v2));
    }

    // Cosine Similarity Tests

    [Fact]
    public void Cos_IdenticalVectors_Success() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        var v2 = new Vector(new double[] { 1, 2, 3 });

        double result = Cos(v1, v2);

        Assert.Equal(1, result, precision: 10);
    }

    [Fact]
    public void Cos_PerpendicularVectors_Success() {
        var v1 = new Vector(new double[] { 1, 0, 0 });
        var v2 = new Vector(new double[] { 0, 1, 0 });

        double result = Cos(v1, v2);

        Assert.Equal(0, result, precision: 10);
    }

    [Fact]
    public void Cos_OppositeVectors_Success() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        var v2 = new Vector(new double[] { -1, -2, -3 });

        double result = Cos(v1, v2);

        Assert.Equal(-1, result, precision: 10);
    }

    [Fact]
    public void Cos_CommutativeProperty_Success() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        var v2 = new Vector(new double[] { 4, 5, 6 });

        Assert.Equal(Cos(v1, v2), Cos(v2, v1), precision: 10);
    }

    [Fact]
    public void Cos_ValueInRange_Success() {
        var v1 = new Vector(new double[] { 3, 4, 5 });
        var v2 = new Vector(new double[] { 1, 7, 2 });

        double result = Cos(v1, v2);

        Assert.True(result >= -1 && result <= 1);
    }

    [Fact]
    public void Cos_WithUnitVectors_Success() {
        var v1 = new Vector(new double[] { 1, 0, 0 });
        var v2 = new Vector(new double[] { 0.6, 0.8, 0 });

        double result = Cos(v1, v2);

        Assert.Equal(0.6, result, precision: 10);
    }

    [Fact]
    public void Cos_MismatchedDimensions_Throws() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        var v2 = new Vector(new double[] { 4, 5 });

        Assert.Throws<ArgumentException>(() => Cos(v1, v2));
    }

    [Fact]
    public void Cos_NullFirstVector_Throws() {
        Vector v1 = null;
        var v2 = new Vector(new double[] { 1, 2, 3 });

        Assert.Throws<ArgumentNullException>(() => Cos(v1, v2));
    }

    [Fact]
    public void Cos_NullSecondVector_Throws() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        Vector v2 = null;

        Assert.Throws<ArgumentNullException>(() => Cos(v1, v2));
    }

    // Angle Tests

    [Fact]
    public void Angle_IdenticalVectors_Success() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        var v2 = new Vector(new double[] { 1, 2, 3 });

        double result = Angle(v1, v2);

        Assert.Equal(0, result, precision: 10);
    }

    [Fact]
    public void Angle_PerpendicularVectors_Success() {
        var v1 = new Vector(new double[] { 1, 0, 0 });
        var v2 = new Vector(new double[] { 0, 1, 0 });

        double result = Angle(v1, v2);

        Assert.Equal(90, result, precision: 10);
    }

    [Fact]
    public void Angle_OppositeVectors_Success() {
        var v1 = new Vector(new double[] { 1, 0, 0 });
        var v2 = new Vector(new double[] { -1, 0, 0 });

        double result = Angle(v1, v2);

        Assert.Equal(180, result, precision: 10);
    }

    [Fact]
    public void Angle_CommutativeProperty_Success() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        var v2 = new Vector(new double[] { 4, 5, 6 });

        Assert.Equal(Angle(v1, v2), Angle(v2, v1), precision: 10);
    }

    [Fact]
    public void Angle_FortyFiveDegrees_Success() {
        var v1 = new Vector(new double[] { 1, 0 });
        var v2 = new Vector(new double[] { 1, 1 });

        double result = Angle(v1, v2);

        Assert.Equal(45, result, precision: 10);
    }

    [Fact]
    public void Angle_ValueInRange_Success() {
        var v1 = new Vector(new double[] { 3, 4, 5 });
        var v2 = new Vector(new double[] { 1, 7, 2 });

        double result = Angle(v1, v2);

        Assert.True(result >= 0 && result <= 180);
    }

    [Fact]
    public void Angle_WithDifferentMagnitudes_Success() {
        var v1 = new Vector(new double[] { 1, 0 });
        var v2 = new Vector(new double[] { 0, 100 });

        double result = Angle(v1, v2);

        Assert.Equal(90, result, precision: 10);
    }

    [Fact]
    public void Angle_MismatchedDimensions_Throws() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        var v2 = new Vector(new double[] { 4, 5 });

        Assert.Throws<ArgumentException>(() => Angle(v1, v2));
    }

    [Fact]
    public void Angle_NullFirstVector_Throws() {
        Vector v1 = null;
        var v2 = new Vector(new double[] { 1, 2, 3 });

        Assert.Throws<ArgumentNullException>(() => Angle(v1, v2));
    }

    [Fact]
    public void Angle_NullSecondVector_Throws() {
        var v1 = new Vector(new double[] { 1, 2, 3 });
        Vector v2 = null;

        Assert.Throws<ArgumentNullException>(() => Angle(v1, v2));
    }

    // Scale(Vector, Matrix) Tests

    [Fact]
    public void Scale_VectorByMatrix_Success() {
        var v = new Vector(new double[] { 1, 2 });
        var m = new Matrix(2, 2, new double[,] { { 3, 0 }, { 0, 4 } });

        Vector result = Scale(v, m);

        Assert.Equal(3, result[0]);
        Assert.Equal(8, result[1]);
    }

    [Fact]
    public void Scale_VectorByIdentityMatrix_Success() {
        var v = new Vector(new double[] { 5, 7 });
        var m = new Matrix(2, 2);

        Vector result = Scale(v, m);

        Assert.Equal(v[0], result[0]);
        Assert.Equal(v[1], result[1]);
    }

    [Fact]
    public void Scale_VectorByZeroMatrix_Success() {
        var v = new Vector(new double[] { 5, 7 });
        var m = new Matrix(2, 2, new double[,] { { 0, 0 }, { 0, 0 } });

        Vector result = Scale(v, m);

        Assert.Equal(0, result[0]);
        Assert.Equal(0, result[1]);
    }

    [Fact]
    public void Scale_ResultDimensionCorrect_Success() {
        var v = new Vector(new double[] { 1, 2, 3 });
        var m = new Matrix(4, 3, new double[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 },
            { 0, 0, 0 }
        });

        Vector result = Scale(v, m);

        Assert.Equal(4, result.Dimension);
    }

    [Fact]
    public void Scale_DimensionMismatch_Throws() {
        var v = new Vector(new double[] { 1, 2 });
        var m = new Matrix(3, 3);

        Assert.Throws<ArgumentException>(() => Scale(v, m));
    }

    [Fact]
    public void Scale_NullVector_Throws() {
        Vector v = null;
        var m = new Matrix(2, 2);

        Assert.Throws<ArgumentNullException>(() => Scale(v, m));
    }

    [Fact]
    public void Scale_NullMatrix_Throws() {
        var v = new Vector(new double[] { 1, 2 });
        Matrix m = null;

        Assert.Throws<ArgumentNullException>(() => Scale(v, m));
    }
}
