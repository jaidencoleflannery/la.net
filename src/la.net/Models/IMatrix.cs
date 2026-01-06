using System.Numerics;

namespace Matrices;
public interface IMatrix<T> where T : INumber<T> {
    
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
	T Get(int row, int col);
	
	/// <summary>
	/// gets the specified row (array) of values from matrix.
	/// </summary>
	T[] GetRow(int row);

	/// <summary>
	/// augments existing matrix's value stored in specified row and column.
	/// </summary>
    void Set(int row, int col, T value);

	/// <summary>
	/// replaces the specified row (array) of values from matrix with provided array.
	/// </summary>
    void SetRow (int row, T[] value, Boolean conform);

	/// <summary>
	/// pushes a new row to the beginning (top) of the matrix. 
	/// </summary>
    void PushRow (T[] value, Boolean conform);

	/// <summary>
	/// appends a row to the end (bottom) of the matrix.
	/// </summary>
    void AppendRow (T[] values, Boolean conform);

	/// <summary>
	/// returns a tuple (rows, columns) of the current size of the matrix.
	/// </summary>
    (int Rows, int Cols) GetSize();

}
