using System.Numerics;

namespace Matrices;
public interface IMatrix<T> where T : INumber<T> {
	/// <summary>
	/// gets a value from the specified row and column of matrix.
	/// </summary>
	public T Get(int row, int col);
	
	/// <summary>
	/// gets the specified row (array) of values from matrix.
	/// </summary>
	public T[] GetRow(int row);

	/// <summary>
	/// augments existing matrix's value stored in specified row and column.
	/// </summary>
	public void Set(int row, int col, T value);

	/// <summary>
	/// replaces the specified row (array) of values from matrix with provided array.
	/// </summary>
	public void SetRow (int row, T[] value, Boolean conform);

	/// <summary>
	/// pushes a new row to the beginning (top) of the matrix. 
	/// </summary>
	public void PushRow (T[] value, Boolean conform);

	/// <summary>
	/// appends a row to the end (bottom) of the matrix.
	/// </summary>
	public void AppendRow (T[] values, Boolean conform);

	/// <summary>
	/// returns a tuple (rows, columns) of the current size of the matrix.
	/// </summary>
	public (int Rows, int Cols) GetSize();

	/// <summary>
	/// creates a formatted string of the matrix.	
	/// </summary>
	public string Print();
	

}
