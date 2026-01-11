    namespace Matrices;
    
    public enum RowOpKind {Swap, Scale, AddScaled};

    public struct RowOperation<T> {

        public RowOpKind Kind { get; }
        public int? R1 { get; }
        public int R2 { get; }
        public T? Scalar { get; }

        private RowOperation(RowOpKind rowOpKind, int? r1, int r2, T? scalar) {
            Kind = rowOpKind;
            R1 = r1; // == row being scaled for addition (nullable).
            R2 = r2; // == row being augmented (or directly scaled).
            Scalar = scalar;
        }
    }
