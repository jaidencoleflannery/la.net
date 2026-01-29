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
}
