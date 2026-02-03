using Vectors;
using static Vectors.VectorOperations;

namespace Vectors.Tests;

public class VectorMethodTests
{
    [Fact]
    public void AsSpanSuccess() {
        Vector v1 = new Vector(new double[] { 0, 4, 8 });
        ReadOnlySpan<double> vs = v1.AsSpan();
        Assert.Equal(v1[0], vs[0]);
        Assert.Equal(v1[1], vs[1]);
        Assert.Equal(v1[2], vs[2]);
    }

    [Fact]
    public void AsMutableSpanSuccess() {
        Vector v1 = new Vector(new double[] { 0, 4, 8 });
        Span<double> mvs = v1.AsMutableSpan();
        Assert.Equal(v1[0], mvs[0]);
        Assert.Equal(v1[1], mvs[1]);
        Assert.Equal(v1[2], mvs[2]);
    }

    [Fact]
    public void CloneVectorSuccess() {
        Vector v1 = new Vector(new double[] { 0, 4, 8 });
        Vector v2 = v1.Clone();
        Assert.IsType<Vector>(v2);
        Assert.True(v1 == v2);
        v2[0] = 2;
        Assert.False(v1 == v2);
    }

    [Fact]
    public void EnumerateVectorSuccess() {
        Vector v1 = new Vector(new double[] { 0, 4, 8 });
        int cursor = 0;
        foreach(Double scalar in v1) {
            Assert.Equal(v1[cursor], scalar);
            cursor++;
        }
    }

    [Fact]
    public void SliceIndexOutOfBoundsThrows() {
        Vector v1 = new Vector(new double[] { 0, 4, 8 });
        Vector v2 = v1.Slice(1, 1);
        Assert.IsType<Vector>(v2);
        Assert.Equal(4, v2[0]);
        Assert.Throws<IndexOutOfRangeException>(() => v2[1]);
    }

    [Fact]
    public void EqualsApproxSuccess() {
        Vector v1 = new Vector(new double[] { 0, 4, 8 });
        Vector v2 = new Vector(new double[] { 0.9, 4, 8 });
        Assert.True(v1.EqualsApprox(v2, 1));
    }

    [Fact]
    public void EqualsApproxFail() {
        Vector v1 = new Vector(new double[] { 0, 4, 8 });
        Vector v2 = new Vector(new double[] { 1, 4, 8 });
        Assert.False(v1.EqualsApprox(v2, 1));
    }

    [Fact]
    public void EqualsApproxThrowsFail() {
        Vector v1 = new Vector(new double[] { 0, 4, 8 });
        Vector v2 = new Vector(new double[] { 1, 4, 8, 9 });
        Assert.Throws<ArgumentException>(() => v1.EqualsApprox(v2, 2));
    }

    [Fact]
    public void GetUnitSuccess() {
        Vector v1 = new Vector(new double[] { 0, 4, 8 });
        Vector v1u = v1.GetUnit();
        Assert.Equal((v1[0] / v1.Norm), v1u[0]);
        Assert.Equal(1, Dot(v1u, v1u));
    }

    [Fact]
    public void GetDecimalUnitSuccess() {
        Vector v1 = new Vector(new double[] { 0, 0.1, 0.4 });
        Vector v1u = v1.GetUnit();
        Assert.Equal((v1[0] / v1.Norm), v1u[0]); 
        Assert.Equal(1, Dot(v1u, v1u));
    }

    [Fact]
    public void HashCodeMatchesSuccess() {
        Vector v1 = new Vector(new double[] { 0, 0.1, 0.4 });
        Vector v2 = new Vector(new double[] { 0, 0.1, 0.4 });
        Assert.Equal(v1.GetHashCode(), v2.GetHashCode());
    }

    [Fact]
    public void HashCodeDoesNotMatchSuccess() {
        Vector v1 = new Vector(new double[] { 0, 0.1, 0.4 });
        Vector v2 = new Vector(new double[] { 0, 9, 0.4 });
        Assert.NotEqual(v1.GetHashCode(), v2.GetHashCode());
    }

}

