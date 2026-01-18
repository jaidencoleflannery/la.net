namespace Matrices.Logging;

public class MatrixLog {
    public List<RowOperation> rowOps { get; private set; }

    public MatrixLog() {
        this.rowOps = new List<RowOperation>();
    }

    public void LogStep(RowOperation rowOp) {
        this.rowOps.Add(rowOp);
    }
}
