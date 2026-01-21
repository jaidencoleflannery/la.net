namespace Vectors;
public interface IVector {
    
    /// <summary>
    /// returns the length of the vector.
    /// </summary>
    int Length { get; }

	/// <summary>
	/// gets the x and y value of the vector.
    /// </summary>
	double this[int index] { get; set; }

	/// <summary>
	/// get a readonly span of data for the vector instance.
    /// </summary>
    ReadOnlySpan<double> AsSpan();

    /// <summary>
	/// return a clone of the vector instance.
    /// </summary>
    IVector Clone(IVector vector);

    /// <summary>
	/// get a mutable span of data for the vector instance.
    /// </summary>
    Span<double> AsMutableSpan();

	/// <summary>
	/// get an enumerable of the vector instance.
    /// </summary>
    IEnumerator<double> GetEnumerator();

	/// <summary>
	/// decrease the dimension of the vector instance. 
	/// </summary>
    IVector Slice(int start, int numValues, IVector vector);

    /// <summary>
	/// add vector {a} to the vector instance. 
	/// </summary>
    IVector Add(IVector a);

    /// <summary>
	/// subtract from the vector instance by vector {a}. 
	/// </summary>
    IVector Subtract(IVector a);

    /// <summary>
	/// scale the vector instance by vector {a}. 
	/// </summary>
    IVector Scale(IVector a);

    /// <summary>
	/// find the dot product of the vector instance and vector {a}. 
	/// </summary>
    Double Dot(IVector a);

    /// <summary>
	/// find if two vectors are approximately comparable by the specified threshold.
    /// </summary>
    bool EqualsApprox(IVector a, double threshold);
}
