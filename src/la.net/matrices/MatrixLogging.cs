using System.Numerics;

namespace Matrices.Logging;

public class MatrixLog<T> where T : INumber<T> {
    public List<RowOperation<T>> rowOps { get; private set; }

    public MatrixLog() {
        this.rowOps = new List<RowOperation<T>>();
    }

    public void LogStep(RowOperation<T> rowOp) {
        this.rowOps.Add(rowOp);
    }
}
