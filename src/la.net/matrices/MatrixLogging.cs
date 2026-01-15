using System.Numerics;

namespace Matrices.Logging;

public class MatrixLog {
    public List<RowOperation<double>> rowOps { get; private set; }

    public MatrixLog() {
        this.rowOps = new List<RowOperation<double>>();
    }

    public void LogStep(RowOperation<double> rowOp) {
        this.rowOps.Add(rowOp);
    }
}
