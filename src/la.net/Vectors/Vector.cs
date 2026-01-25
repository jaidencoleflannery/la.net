using System.Text;

namespace Vectors;

public sealed class Vector : IVector {

    // constructors

    public Vector(int dimension) {  
        ReadOnlySpan<double> scalars = new double[dimension];
        Dimension = dimension;
        _data = scalars.ToArray();  // this is copying, if typeof(scalars) == double[] (so our data cannot be tampered with)
        _norm = Math.Sqrt(_data.Sum(num => num * num)); // the "L2" length can be found by taking the square root of the sum of all squared scalars 
    }

    public Vector(ReadOnlySpan<double> scalars) {  
        Dimension = scalars.Length;
        _data = scalars.ToArray();
        _norm = Math.Sqrt(_data.Sum(num => num * num));
    }

    public Vector(double[] scalars) : this(scalars.AsSpan()) { }

    // attributes
    
    public int Dimension { get; }

    private double _norm { get; set; } 

    private double[] _data { get; set; } // a vector is a set of scalars (for each base vector that equates to the result)
    
    // accessors

    public double Norm => _norm;

    // operators

    public double this[int index] {
        get => _data[index];
        set => _data[index] = value;
    }

    public static IVector operator +(Vector a, IVector b) =>
        Add(a, b);

    public static IVector operator -(Vector a, IVector b) =>
        Subtract(a, b);

    public static IVector operator *(Vector a, IVector b) =>
        Scale(a, b);

    public static IVector operator *(Vector a, double scalar) =>
        Scale(a, scalar);

    // methods

    public ReadOnlySpan<double> AsSpan() =>
        new(_data);

    public Span<double> AsMutableSpan() =>
        new(_data);

    public IVector Clone() =>
        new Vector(this.AsSpan());

    public IEnumerator<double> GetEnumerator() {
        for(int cursor = 0; cursor < _data.Length; cursor++) yield return _data[cursor];
    }

    public IVector Slice(int start, int numValues) =>
        new Vector(this.AsSpan().Slice(start, numValues));

    public static IVector Add(IVector a, IVector b) {
        double[] vector = new double[a.Dimension];
        for(int cursor = 0; cursor < a.Dimension; cursor++) vector[cursor] = a[cursor] + b[cursor];
        return new Vector(vector);
    }

    public static IVector Subtract(IVector a, IVector b) {
        double[] vector = new double[a.Dimension];
        for(int cursor = 0; cursor < a.Dimension; cursor++) vector[cursor] = a[cursor] - b[cursor];
        return new Vector(vector);
    }

    public static IVector Scale(IVector a, IVector b) {
        double[] vector = new double[a.Dimension];
        for(int cursor = 0; cursor < a.Dimension; cursor++) vector[cursor] = a[cursor] * b[cursor];
        return new Vector(vector);
    }

    public static IVector Scale(IVector a, double scalar) {
        double[] vector = new double[a.Dimension];
        for(int cursor = 0; cursor < a.Dimension; cursor++) vector[cursor] = a[cursor] * scalar;
        return new Vector(vector);
    }

    public double Dot(IVector a) {
        if(this.Dimension != a.Dimension) throw new ArgumentException($"Cannot find the dot product of matrices with differing dimensions.");
        double dotProduct = 0.0;
        for(int cursor = 0; cursor < this.Dimension; cursor++) {
            dotProduct += (this[cursor] * a[cursor]);
        }
        return dotProduct;
    }

    public bool EqualsApprox(IVector a, double threshold) {
        if(this.Dimension != a.Dimension) throw new ArgumentException($"Cannot approximately compare matrices with differing dimensions.");
        for(int cursor = 0; cursor < this.Dimension; cursor++) {
            if(this[cursor] - a[cursor] >= threshold) return false;
        }
        return true;
    }

    public IVector GetUnit() {
        Vector v = new Vector(this.Dimension);
        for(int row = 0; row < this.Dimension; row++)
            v[row] = this[row] / this.Norm;
        return v;
    }

    public override string ToString() {
        var sb = new StringBuilder();
        for (int scalar = 0; scalar < Dimension; scalar++)
        {
            sb.Append("| ");
            sb.Append($"{_data[scalar]:F3} ");
            sb.Append("|");
            sb.AppendLine("");
        }
        return sb.ToString();
    }

    public string ToStringRounded() {
        var sb = new StringBuilder();
        for (int scalar = 0; scalar < Dimension; scalar++)
        {
            sb.Append("| ");
            sb.Append($"{_data[scalar]:F0} ");
            sb.Append("|");
            sb.AppendLine("");
        }
        return sb.ToString();
    }
}
