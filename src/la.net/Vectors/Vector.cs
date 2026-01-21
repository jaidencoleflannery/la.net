using System.Collections;
using System.Numerics;

namespace Vectors;

public sealed class Vector : IVector {
    // attributes

    public int Dimension { get; }

    private double[] _data; // a vector is a set of scalars (for each base vector that equates to the result)
    
    // accessors

    double Length => Math.Sqrt(_data.Sum(num => num * num)); // the "L2" length can be found by taking the square root of the sum of all squared scalars
 
    // constructors

    public Vector(int dimension, ReadOnlySpan<double> scalars) {
        if(scalars.Length == 0) throw new ArgumentException("Vector cannot be empty.");
        Dimension = dimension;
        _data = scalars.ToArray();  // this is copying, so if typeof(scalars) == double[] so our data cannot be tampered with
    }

    public Vector(int dimension, double[] scalars) : this(dimension, scalars.AsSpan()) { }

    // operators

    public double this[int index] =>
        _data[index];

    public static IVector operator +(Vector a, Vector b) =>
        Add(a, b);

    public static IVector operator -(Vector a, Vector b) =>
        Subtract(a, b);

    public static IVector operator *(Vector a, Vector b) =>
        Scale(a, b);

    public static IVector operator *(Vector a, double b) =>
        Scale(a, b);

    // methods

    public ReadOnlySpan<double> AsSpan() =>
        new(_data);

    public Span<double> AsMutableSpan() =>
        new(_data);

    public IVector Clone(IVector vector) =>
        new Vector(vector.Dimension, vector.AsSpan());

    public IEnumerator GetEnumerator() {
        for(int cursor = 0; cursor < _data.Length; cursor++) yield return _data[cursor];
    }

    public IVector Slice(int start, int numValues, IVector vector) =>
        new Vector(vector.Dimension, vector.AsSpan().Slice(start, numValues));

    public static IVector Add(this IVector a, IVector b) {
        double[] vector = new double[a.Dimension];
        for(int cursor = 0; cursor < a.Dimension; cursor++) vector[cursor] = a[cursor] + b[cursor];
        return new Vector(a.Dimension, vector);
    }

    public static IVector Subtract(this IVector a, IVector b) {
        double[] vector = new double[a.Dimension];
        for(int cursor = 0; cursor < a.Dimension; cursor++) vector[cursor] = a[cursor] - b[cursor];
        return new Vector(a.Dimension, vector);
    }

    public static IVector Scale(this IVector a, IVector b) {
        double[] vector = new double[a.Dimension];
        for(int cursor = 0; cursor < a.Dimension; cursor++) vector[cursor] = a[cursor] * b[cursor];
        return new Vector(a.Dimension, vector);
    }

    public static IVector Scale(this IVector a, double b){
        double[] vector = new double[a.Dimension];
        for(int cursor = 0; cursor < a.Dimension; cursor++) vector[cursor] = a[cursor] * b;
        return new Vector(a.Dimension, vector);
    }



}
