namespace Vector.Tests;

public class VectorConstructorTests {
    
    [Fact]
    public void VectorValidConstructionFromArraySuccess() {
        double scalars = new double[] {0, 2, 9, 4};
        Vector vector = new Vector(4, scalars);
        Assert.NotNull(vector);
        Assert.IsType<Vector>(vector);
    }

    [Fact]
    public void VectorInvalidConstructionFromArrayFail() {
        double scalars = new double[] {0, 2, 9, 4, 5}; 
        Assert.Throws<ArgumentException>(() => (new Vector(4, scalars)));
        Assert.Throws<ArgumentException>(() => (new Vector(5, new double() {})));
    }

    [Fact]
    public void VectorValidConstructionFromSpanSuccess() {
        span<Double> scalars = new double[] {0, 2, 9, 4};
        Vector vector = new Vector(4, scalars);
        Assert.NotNull(vector);
        Assert.IsType<Vector>(vector);
    }

    [Fact]
    public void VectorInvalidConstructionFromSpanFail() {
        Span<double> scalars = new double[] {0, 2, 9, 4, 5}; 
        Assert.Throws<ArgumentException>(() => (new Vector(4, scalars)));
        Assert.Throws<ArgumentException>(() => (new Vector(5, new double() {})));
    }

    [Fact]
    public void VectorConstructionNormSuccess() {
        double[] scalars = new double[] {0, 2, 9, 4, 5};
        Assert.Equals(Math.Sqrt(_data.Sum(num => num * num)), v.Norm);
    } 
}
