namespace Matrices.Tests;
using Matrices.Logging;

public class MatrixLoggingTests
{
    // Write to Log 

    [Fact]
    public void WriteSwapToLogValid() {
        var rowOp = new RowOperation(RowOpKind.Swap, 1, 2);
        var logger = new MatrixLog();
        logger.LogStep(rowOp);
        Assert.Equal(rowOp, logger.rowOps[0]);
    }

    [Fact]
    public void WriteScaleToLogValid() {
        var rowOp = new RowOperation(RowOpKind.Scale, 2, 14);
        var logger = new MatrixLog();
        logger.LogStep(rowOp);
        Assert.Equal(rowOp, logger.rowOps[0]);
    }

    [Fact]
    public void WriteAddScaledToLogValid() {
        var rowOp = new RowOperation(RowOpKind.AddScaled, 1, 2, 2, 2);
        var logger = new MatrixLog();
        logger.LogStep(rowOp);
        Assert.Equal(rowOp, logger.rowOps[0]);
    }
}
