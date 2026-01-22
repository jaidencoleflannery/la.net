using System.Collections;

namespace Vectors;

public sealed class Vector : IVector {
    // attributes

    public int Dimension { get; }

    private double[] _data; // a vector is a set of scalars (for each base vector that equates to the result)
    
    // accessors

    public double Norm => Math.Sqrt(_data.Sum(num => num * num)); // the "L2" length can be found by taking the square root of the sum of all squared scalars
 
    // constructors

    public Vector(int dimension, ReadOnlySpan<double> scalars) {
        if(scalars.Length == 0) throw new ArgumentException("Vector cannot be empty.");
        if(scalars.Length != dimension) throw new ArgumentException($"Vector must have less {nameof(scalars)} than {nameof(dimension)}.");
        Dimension = dimension;
        _data = scalars.ToArray();  // this is copying, so if typeof(scalars) == double[] so our data cannot be tampered with
    }

    public Vector(int dimension, double[] scalars) : this(dimension, scalars.AsSpan()) { }

    // operators

    public double this[int index] =>
        _data[index];

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
        new Vector(this.Dimension, this.AsSpan());

    public IEnumerator<double> GetEnumerator() {
        for(int cursor = 0; cursor < _data.Length; cursor++) yield return _data[cursor];
    }

    public IVector Slice(int start, int numValues) =>
        new Vector(this.Dimension, this.AsSpan().Slice(start, numValues));

    public IVector Add(this Vector a, IVector b) {
        double[] vector = new double[a.Dimension];
        for(int cursor = 0; cursor < a.Dimension; cursor++) vector[cursor] = a[cursor] + b[cursor];
        return new Vector(this.Dimension, vector);
    }

    public IVector Subtract(IVector a) {
        double[] vector = new double[this.Dimension];
        for(int cursor = 0; cursor < this.Dimension; cursor++) vector[cursor] = this[cursor] - a[cursor];
        return new Vector(this.Dimension, vector);
    }

    public IVector Scale(IVector a) {
        double[] vector = new double[this.Dimension];
        for(int cursor = 0; cursor < this.Dimension; cursor++) vector[cursor] = this[cursor] * a[cursor];
        return new Vector(this.Dimension, vector);
    }

    public IVector Scale(double scalar) {
        double[] vector = new double[this.Dimension];
        for(int cursor = 0; cursor < this.Dimension; cursor++) vector[cursor] = this[cursor] * scalar;
        return new Vector(this.Dimension, vector);
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
}
