    namespace Matrices;
    
    public enum RowOpKind {Swap, Scale, AddScaled};
    public readonly struct RowOperation<T> (
        RowOpKind RowOpKind,
        int R1,
        int R2,
        T Scalar
    );