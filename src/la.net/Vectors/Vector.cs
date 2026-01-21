namespace Vectors;

public sealed class Vector {
    public int Dimension { get; private set; }
    // a vector is a set of scalars (for each base vector that equates to the result)
    private double[] _data; 
 
    public Vector(int dimension, ReadOnlySpan<double> scalars) {
        if(scalars.Length == 0) throw new ArgumentException("Vector cannot be empty.");
        Dimension = dimension;
        _data = scalars.ToArray();  // this is copying our array if typeof(scalars) == double[]
                                    // so our data cannot be tampered with
    }

    public Vector(int dimension, double[] scalars) : this(dimension, scalars.AsSpan()) { }
}
