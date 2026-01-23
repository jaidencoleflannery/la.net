namespace Vectors;
public interface IVector {

    /// <summary>
    /// returns the dimension of the vector.
    /// </summary>
    int Dimension { get; }
    
    /// <summary>
    /// returns the length of the vector.
    /// </summary>
    double Norm { get; }

	/// <summary>
	/// gets the specified base scalar from the vector instance.
    /// </summary>
	double this[int index] { get; }
	
    /// <summary>
	/// return a clone of the vector instance.
    /// </summary>
    IVector Clone();
 
	/// <summary>
	/// get an enumerable of the vector instance.
    /// </summary>
    IEnumerator<double> GetEnumerator();

	/// <summary>
	/// decrease the dimension of the vector instance. 
	/// </summary>
    IVector Slice(int start, int length);
    
    /// <summary>
	/// find the dot product of the vector instance and vector {a}. 
	/// </summary>
    double Dot(IVector a);

    /// <summary>
	/// find if two vectors are approximately comparable by the specified threshold 
    /// (difference between vectors is greater than or equal to threshold).
    /// </summary>
    bool EqualsApprox(IVector b, double threshold);
}
