using Matrices;

namespace Matrices.Tests;

public class RowOperationTests
{
    [Fact]
    public void RowOperation_SwapConstructor_Success() {
        var rowOp = new RowOperation(RowOpKind.Swap, 0, 1);

        Assert.Equal(RowOpKind.Swap, rowOp.Kind);
        Assert.Equal(0, rowOp.R1);
        Assert.Equal(1, rowOp.R2);
    }

    [Fact]
    public void RowOperation_ScaleConstructor_Success() {
        var rowOp = new RowOperation(RowOpKind.Scale, 2, 3.5);

        Assert.Equal(RowOpKind.Scale, rowOp.Kind);
        Assert.Equal(2, rowOp.R2);
        Assert.Equal(3.5, rowOp.Scalar);
    }

    [Fact]
    public void RowOperation_AddScaledConstructor_Success() {
        var rowOp = new RowOperation(RowOpKind.AddScaled, 1, 2, 2.0, 0);

        Assert.Equal(RowOpKind.AddScaled, rowOp.Kind);
        Assert.Equal(1, rowOp.R1);
        Assert.Equal(2, rowOp.R2);
        Assert.Equal(2.0, rowOp.Scalar);
        Assert.Equal(0, rowOp.Pivot);
    }

    [Fact]
    public void RowOperation_SwapSameRows_Success() {
        var rowOp = new RowOperation(RowOpKind.Swap, 0, 0);

        Assert.Equal(0, rowOp.R1);
        Assert.Equal(0, rowOp.R2);
    }

    [Fact]
    public void RowOperation_SwapLargeIndices_Success() {
        var rowOp = new RowOperation(RowOpKind.Swap, 999, 1000);

        Assert.Equal(999, rowOp.R1);
        Assert.Equal(1000, rowOp.R2);
    }

    [Fact]
    public void RowOperation_SwapNonAdjacent_Success() {
        var rowOp = new RowOperation(RowOpKind.Swap, 0, 5);

        Assert.Equal(0, rowOp.R1);
        Assert.Equal(5, rowOp.R2);
    }

    [Fact]
    public void RowOperation_ScaleByZero_Success() {
        var rowOp = new RowOperation(RowOpKind.Scale, 0, 0.0);

        Assert.Equal(0.0, rowOp.Scalar);
    }

    [Fact]
    public void RowOperation_ScaleByOne_Success() {
        var rowOp = new RowOperation(RowOpKind.Scale, 1, 1.0);

        Assert.Equal(1.0, rowOp.Scalar);
    }

    [Fact]
    public void RowOperation_ScaleNegative_Success() {
        var rowOp = new RowOperation(RowOpKind.Scale, 0, -5.5);

        Assert.Equal(-5.5, rowOp.Scalar);
    }

    [Fact]
    public void RowOperation_ScaleLargeValue_Success() {
        var rowOp = new RowOperation(RowOpKind.Scale, 0, 1e100);

        Assert.Equal(1e100, rowOp.Scalar);
    }

    [Fact]
    public void RowOperation_ScaleSmallValue_Success() {
        var rowOp = new RowOperation(RowOpKind.Scale, 0, 1e-10);

        Assert.Equal(1e-10, rowOp.Scalar);
    }

    [Fact]
    public void RowOperation_AddScaledZeroScalar_Success() {
        var rowOp = new RowOperation(RowOpKind.AddScaled, 0, 1, 0.0, 0);

        Assert.Equal(0.0, rowOp.Scalar);
    }

    [Fact]
    public void RowOperation_AddScaledNegativeScalar_Success() {
        var rowOp = new RowOperation(RowOpKind.AddScaled, 0, 1, -2.5, 1);

        Assert.Equal(-2.5, rowOp.Scalar);
    }

    [Fact]
    public void RowOperation_AddScaledLargePivot_Success() {
        var rowOp = new RowOperation(RowOpKind.AddScaled, 0, 1, 1.0, 999);

        Assert.Equal(999, rowOp.Pivot);
    }

    [Fact]
    public void RowOperation_AddScaledZeroPivot_Success() {
        var rowOp = new RowOperation(RowOpKind.AddScaled, 1, 2, 1.5, 0);

        Assert.Equal(0, rowOp.Pivot);
    }

    [Fact]
    public void RowOperation_AllKindsDistinct_Success() {
        var swap = new RowOperation(RowOpKind.Swap, 0, 1);
        var scale = new RowOperation(RowOpKind.Scale, 0, 1.0);
        var addScaled = new RowOperation(RowOpKind.AddScaled, 0, 1, 1.0, 0);

        Assert.NotEqual(swap.Kind, scale.Kind);
        Assert.NotEqual(scale.Kind, addScaled.Kind);
        Assert.NotEqual(swap.Kind, addScaled.Kind);
    }

    [Fact]
    public void RowOperation_GetHashCode_Consistent_Success() {
        var rowOp1 = new RowOperation(RowOpKind.Swap, 0, 1);
        var rowOp2 = new RowOperation(RowOpKind.Swap, 0, 1);

        Assert.Equal(rowOp1.GetHashCode(), rowOp2.GetHashCode());
    }

    [Fact]
    public void RowOperation_Equality_Swap_Success() {
        var rowOp1 = new RowOperation(RowOpKind.Swap, 0, 1);
        var rowOp2 = new RowOperation(RowOpKind.Swap, 0, 1);

        Assert.Equal(rowOp1, rowOp2);
    }

    [Fact]
    public void RowOperation_Equality_Scale_Success() {
        var rowOp1 = new RowOperation(RowOpKind.Scale, 2, 2.0);
        var rowOp2 = new RowOperation(RowOpKind.Scale, 2, 2.0);

        Assert.Equal(rowOp1, rowOp2);
    }

    [Fact]
    public void RowOperation_Equality_AddScaled_Success() {
        var rowOp1 = new RowOperation(RowOpKind.AddScaled, 1, 2, 3.0, 1);
        var rowOp2 = new RowOperation(RowOpKind.AddScaled, 1, 2, 3.0, 1);

        Assert.Equal(rowOp1, rowOp2);
    }

    [Fact]
    public void RowOperation_Inequality_DifferentKind_Success() {
        var rowOp1 = new RowOperation(RowOpKind.Swap, 0, 1);
        var rowOp2 = new RowOperation(RowOpKind.Scale, 0, 1.0);

        Assert.NotEqual(rowOp1, rowOp2);
    }

    [Fact]
    public void RowOperation_Inequality_DifferentR2_Success() {
        var rowOp1 = new RowOperation(RowOpKind.Swap, 0, 1);
        var rowOp2 = new RowOperation(RowOpKind.Swap, 0, 2);

        Assert.NotEqual(rowOp1, rowOp2);
    }

    [Fact]
    public void RowOperation_Inequality_DifferentScalar_Success() {
        var rowOp1 = new RowOperation(RowOpKind.Scale, 0, 1.0);
        var rowOp2 = new RowOperation(RowOpKind.Scale, 0, 2.0);

        Assert.NotEqual(rowOp1, rowOp2);
    }

    [Fact]
    public void RowOperation_Inequality_DifferentPivot_Success() {
        var rowOp1 = new RowOperation(RowOpKind.AddScaled, 1, 2, 1.0, 0);
        var rowOp2 = new RowOperation(RowOpKind.AddScaled, 1, 2, 1.0, 1);

        Assert.NotEqual(rowOp1, rowOp2);
    }

    [Fact]
    public void RowOperation_SwapReverse_Success() {
        var rowOp = new RowOperation(RowOpKind.Swap, 5, 2);

        Assert.Equal(5, rowOp.R1);
        Assert.Equal(2, rowOp.R2);
    }

    [Fact]
    public void RowOperation_ScaleLargeRow_Success() {
        var rowOp = new RowOperation(RowOpKind.Scale, 1000, 0.5);

        Assert.Equal(1000, rowOp.R2);
        Assert.Equal(0.5, rowOp.Scalar);
    }

    [Fact]
    public void RowOperation_AddScaledSameRows_Success() {
        var rowOp = new RowOperation(RowOpKind.AddScaled, 0, 0, 1.0, 0);

        Assert.Equal(0, rowOp.R1);
        Assert.Equal(0, rowOp.R2);
    }
}
