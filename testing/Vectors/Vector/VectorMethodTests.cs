using Vectors;

namespace Vectors.Tests;

public class VectorMethodTests
{
    [Fact]
    public void AsSpanSuccess() {
        Vector v1 = new Vector(new double[] { 0, 4, 8 });
        ReadOnlySpan<double> vs = v1.AsSpan();
        Assert.IsType<ReadOnlySpan<Double>(vs);
    }

    [Fact]
    public void AsMutableSpanSuccess() {
        Vector v1 = new Vector(new double[] { 0, 4, 8 });
        Span<double> mvs = v1.AsMutableSpan();
        Assert.IsType<Span<Double>>(mvs);
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
    public void EnumerateVectorSuccess() {
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
        Assert.Thows<ArgumentException>(() => v1.EqualsApprox(v2, 2));
    }

    [Fact]
    public void GetUnitSuccess() {
        Vector v1 = new Vector(new double[] { 0, 4, 8 });
        v1u = v1.GetUnit();
        Assert.Equal((v1[0] / v1.Norm), v1u[0]);
        Assert.Equal(1, Dot(v1u, v1u));
    }

    [Fact]
    public void GetDecimalUnitSuccess() {
        v1 = new Vector(new double[] { 0, 0.1, 0.4 });
        v1u = v1.GetUnit();
        Assert.Equal((v1[0] / v1.Norm), v1u[0]); 
        Assert.Equal(1, Dot(v1u, v1u));
    }

    [Fact]
    public void HashCodeMatchesSuccess() {
        v1 = new Vector(new double[] { 0, 0.1, 0.4 });
        v2 = new Vector(new double[] { 0, 0.1, 0.4 });
        Assert.Equal(v1.GetHashCode(), v2.GetHashCode());
    }

    [Fact]
    public void HashCodeDoesNotMatchSuccess() {
        v1 = new Vector(new double[] { 0, 0.1, 0.4 });
        v2 = new Vector(new double[] { 0, 9, 0.4 });
        Assert.NotEqual(v1.GetHashCode(), v2.GetHashCode());
    }

}

