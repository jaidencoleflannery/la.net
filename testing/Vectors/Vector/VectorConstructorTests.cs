using Vectors;

namespace Vectors.Tests;

public class VectorConstructorTests {
    
    [Fact]
    public void VectorValidConstructionFromArraySuccess() {
        var scalars = new double[] {0, 2, 9, 4};
        Vector vector = new Vector(scalars);
        Assert.NotNull(vector);
        Assert.IsType<Vector>(vector);
    }

    [Fact]
    public void ZeroVectorValidConstructionSuccess() {
        Vector vector = new Vector(3);
        foreach(Double scalar in vector)
            Assert.Equal(0, scalar);
        Assert.NotNull(vector);
        Assert.IsType<Vector>(vector);
    }

    [Fact]
    public void VectorValidConstructionFromSpanSuccess() {
        Span<Double> scalars = new double[] {0, 2, 9, 4};
        var vector = new Vector(scalars);
        Assert.NotNull(vector);
        Assert.IsType<Vector>(vector);
    }

    [Fact]
    public void VectorConstructionNormSuccess() {
        var scalars = new double[] {0, 2, 9, 4, 5};
        var v = new Vector(scalars);
        Assert.Equal(Math.Sqrt(scalars.Sum(num => num * num)), v.Norm);
    } 

    [Fact]
    public void SingleDimensionVectorFromArrayConstructionSuccess() {
        var scalars = new double[] {0};
        var v = new Vector(scalars);
        Assert.NotNull(v);
        Assert.IsType<Vector>(v);
    } 

    [Fact]
    public void SingleDimensionZeroVectorFromDimensionConstructionSuccess() {
        var v = new Vector(0);
        Assert.NotNull(v);
        Assert.IsType<Vector>(v);
        Assert.Equal(0, v[0]);
        Assert.Throws<IndexOutOfRangeException>(() => v[1]);
    }

    [Fact]
    public void LargeDimensionVectorConstructionSuccess() {
        var v = new Vector(999999);
        Assert.NotNull(v);
        Assert.IsType<Vector>(v);
        Assert.Equal(0, v[0]);
        Assert.Equal(0, v[999999]);
        Assert.Throws<IndexOutOfRangeException>(() => v[1000000]);
    }

    [Fact]
    public void ZeroDimensionVectorInvalidConstructionFail() => 
        Assert.Throws<ArgumentException>(() => new Vector(0)); 

}
