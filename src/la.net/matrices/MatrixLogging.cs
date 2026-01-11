using System.Numerics;

namespace Matrices.Logging;

public class MatrixLog<T> where T : INumber<T> {
    List<RowOperation<T>> rowOps;

    public MatrixLog() {
        List<RowOperation<T>> rowOps = new List<RowOperation<T>>();
    }

    public void LogStep(RowOperation<T> rowOp) {
        this.rowOps.Add(rowOp);
    }
    }
