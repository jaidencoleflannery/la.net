namespace Vectors.Tests;

public class VectorAccessorTests
{
    [Fact]
    public void DimensionIsAccurateSuccess() {
        Vector v = new Vector(new double[] { 0, 4, 8 });
        Assert.Equal(3, v.Dimension); 
    }

    [Fact]
    public void NormIsAccurateSuccess() {
        Vector v = new Vector(new double[] { 2, 4, 2 });
        Assert.Equal(Math.Sqrt(24), v.Norm);  
    }

}
