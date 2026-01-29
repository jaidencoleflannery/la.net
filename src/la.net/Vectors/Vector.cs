using System.Text;
using Matrices;
using static Vectors.VectorOperations;

namespace Vectors;

public sealed class Vector {

    // attributes
    
    private int _dimension { get; }

    private double _norm { get; set; } 

    private double[] _data { get; set; } // a vector is a set of scalars (for each base vector that sums to the result)

    // constructors

    public Vector(int dimension) {  
        if(dimension <= 0) throw new ArgumentException("A vector must have at least 1 dimension.");
        ReadOnlySpan<double> scalars = new double[dimension];
        _dimension = dimension;
        _data = scalars.ToArray();  // this is copying, if typeof(scalars) == double[] (so our data cannot be tampered with)
        _norm = Math.Sqrt(_data.Sum(num => num * num)); // the "L2" length can be found by taking the square root of the sum of all squared scalars 
    }

    public Vector(ReadOnlySpan<double> scalars) {  
        _dimension = scalars.Length;
        _data = scalars.ToArray();
        _norm = Math.Sqrt(_data.Sum(num => num * num));
    }

    public Vector(double[] scalars) : this(scalars.AsSpan()) { } 
    
    // accessors

    public int Dimension => _dimension;

    public double Norm => _norm;

    // operators

    public double this[int index] {
        get => _data[index];
        set => _data[index] = value;
    }

    public static Vector operator +(Vector a, Vector b) =>
        Add(a, b);

    public static Vector operator -(Vector a, Vector b) =>
        Subtract(a, b);

    public static Vector operator *(Vector a, double scalar) =>
        Scale(a, scalar);

    public static Vector operator *(Vector a, Matrix m) =>
        Scale(a, m); 

    public static bool operator ==(Vector a, Vector b) =>
        IsEqual(a, b);
 
    public static bool operator !=(Vector a, Vector b) =>
        IsNotEqual(a, b);
 
    public override bool Equals(object? obj) => obj is Vector other && this == other;


    // methods

    public ReadOnlySpan<double> AsSpan() =>
        new(_data);

    public Span<double> AsMutableSpan() =>
        new(_data);

    public Vector Clone() =>
        new Vector(this.AsSpan());

    public IEnumerator<double> GetEnumerator() {
        for(int cursor = 0; cursor < _data.Length; cursor++) yield return _data[cursor];
    }

    public Vector Slice(int start, int numValues) =>
        new Vector(this.AsSpan().Slice(start, numValues));

    public bool EqualsApprox(Vector a, double threshold) {
        if(this.Dimension != a.Dimension) throw new ArgumentException($"Cannot approximately compare matrices with differing dimensions.");
        for(int cursor = 0; cursor < this.Dimension; cursor++) {
            if(this[cursor] - a[cursor] >= threshold) return false;
        }
        return true;
    }

    public Vector GetUnit() {
        Vector v = new Vector(this.Dimension);
        for(int row = 0; row < this.Dimension; row++)
            v[row] = this[row] / this.Norm;
        return v;
    }

    public override int GetHashCode() {
        var hash = new HashCode();
        hash.Add(this.Norm);
        hash.Add(this.Dimension);
        for(int cursor = 0; cursor < this.Dimension; cursor++)
            hash.Add(_data[cursor]);
        return hash.ToHashCode();
    }

    public override string ToString() {
        var sb = new StringBuilder();
        for (int scalar = 0; scalar < Dimension; scalar++) {
            sb.Append("| ");
            sb.Append($"{_data[scalar]:F3} ");
            sb.Append("|");
            sb.AppendLine("");
        }
        return sb.ToString();
    }

    public string ToStringRounded() {
        var sb = new StringBuilder();
        for (int scalar = 0; scalar < Dimension; scalar++) {
            sb.Append("| ");
            sb.Append($"{_data[scalar]:F0} ");
            sb.Append("|");
            sb.AppendLine("");
        }
        return sb.ToString();
    }
}
