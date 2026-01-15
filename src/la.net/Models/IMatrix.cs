
namespace Matrices;
public interface IMatrix {
    
    /// <summary>
    /// returns the number of rows in the matrix.
	/// </summary>
    int Rows { get; }

    /// <summary>
    /// returns the number of columns in the matrix.
	/// </summary>
    int Cols { get; }

	/// <summary>
	/// gets a value from the specified row and column of matrix.
	/// </summary>
	double Get(int row, int col);
	
	/// <summary>
	/// gets the specified row (array) of values from matrix.
	/// </summary>
	double[] GetRow(int row);

	/// <summary>
	/// augments existing matrix's value stored in specified row and column.
	/// </summary>
    void Set(int row, int col, double value);

	/// <summary>
	/// replaces the specified row (array) of values from matrix with provided array.
	/// </summary>
    void SetRow (int row, double[] value, Boolean conform);

	/// <summary>
	/// pushes a new row to the beginning (top) of the matrix. 
	/// </summary>
    void PushRow (double[] value, Boolean conform);

	/// <summary>
	/// appends a row to the end (bottom) of the matrix.
	/// </summary>
    void AppendRow (double[] values, Boolean conform);

	/// <summary>
	/// returns a tuple (rows, columns) of the current size of the matrix.
	/// </summary>
    (int Rows, int Cols) GetSize();

}
