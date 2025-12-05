using System.Drawing;
using System.Numerics;
using System.Reflection.Metadata;

namespace Matrices;
public sealed class Matrix<T> where T : INumber<T>
{
    public int Cols { get; set; }
    public int Rows { get; set; }
    private T[,] _data;
    public Matrix(int rows, int cols, T[,]? data = null)
    {
        if (rows <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rows), $"{nameof(rows)} must be greater than 0.");
        }
        else if (cols <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cols), $"{nameof(cols)} must be greater than 0.");
        }

        if (data != null && rows < data.GetLength(0))
        {
            throw new ArgumentOutOfRangeException(nameof(rows), $"{nameof(rows)} must be greater than or equal to provided data.");
        }
        else if (data != null && cols < data.GetLength(1))
        {
            throw new ArgumentOutOfRangeException(nameof(cols), $"{nameof(cols)} must be greater than or equal to provided data.");
        }
        
        this.Cols = cols;
        this.Rows = rows;

        _data = new T[rows, cols];

        if (data == null)
        {
            int max;
            // we are traversing the diagonal path of the matrix and need to know where the matrix ends, max is where the final pivot value will lay despite any free variables
            if (rows >= cols)
            {
                max = cols;
            }
            else
            {
                max = rows;
            }
            // this turns our remaining zero matrix entries into an identity matrix
            for (int cursor = 0; cursor < max; cursor++)
            {
                _data[cursor, cursor] = T.One;
            }
        }
        else
        {
            // if matrix is smaller than identity matrix, push smaller dimensional matrix into larger dimensional identity matrix
            if (data.GetLength(0) < rows || data.GetLength(1) < cols)
            {
                for (int row = 0; row < data.GetLength(0); row++)
                {
                    for (int col = 0; col < data.GetLength(1); col++)
                    {
                        _data[row, col] = data[row, col];
                    }
                }
                int index = (data.GetLength(0) > data.GetLength(1)) ? data.GetLength(1) : data.GetLength(0);
                int max;
                // we are traversing the diagonal path of the matrix PAST the provided matrix and turning it into an identity matrix
                if (rows >= cols)
                {
                    max = rows;
                }
                else
                {
                    max = cols;
                }
                for (int cursor = index; cursor < max; cursor++)
                {
                    if (cursor < _data.GetLength(0) && cursor < _data.GetLength(1))
                    {
                        _data[cursor, cursor] = T.One;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                // we clone so that we have our own matrix in memory
                _data = (T[,])data.Clone();
            }
        }
    }

    public T Get(int row, int col)
    {
        return _data[row, col];
    }

    public T[] GetRow(int row)
    {
        if (row < 0 || row >= Rows)
        {
            throw new ArgumentOutOfRangeException(nameof(row), "Row index is out of range.");
        }
        T[] output = new T[Cols];
        for (int col = 0; col < Cols; col++)
        {
            output[col] = _data[row, col];
        }
        return output;
    }

    public void Set(int row, int col, T value)
    {
        _data[row, col] = value;
    }

    public void SetRow(int row, T[] value, Boolean conform = false)
    {
        if(Cols < value.Length) {
            if(conform) {
                throw new ArgumentOutOfRangeException(nameof(row), "Added row must be less than or equal to column length of existing matrix.");
            } else {
                throw new ArgumentOutOfRangeException(nameof(row), "Added row must be equal to column length of existing matrix.");
            }
        }
        for (int col = 0; col < Cols; col++) {
            if(col < value.Length) {
                _data[row, col] = value[col];
            } else {
                _data[row, col] = T.Zero;
            }
        }
        if(conform) {
            // convert free n*n indices to identity form
            if(Cols > value.Length) {
                if(row > value.Length) {
                    _data[row, row] = T.One;
                }
            }
        }
    }

    public void PushRow(T[] value, Boolean conform = false)
    {
        if(Cols < value.Length) {
            if(conform) {
                throw new ArgumentOutOfRangeException(nameof(value), "Added row must be less than or equal to column length of existing matrix.");
            } else {
                throw new ArgumentOutOfRangeException(nameof(value), "Added row must be equal to column length of existing matrix.");
            }
        }
        T[,] matrix = new T[Rows + 1, Cols];
        for (int col = 0; col < value.Length; col++)
        {
            matrix[0, col] = value[col];
        }
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Cols; col++)
            {
                matrix[row + 1, col] = _data[row, col];
            }
        }
        if(conform) {
            // convert n*n index to identity form
            if(value.Length == 0) {
                    matrix[0, 0] = T.One;
            }
        }
        Rows += 1;
        _data = matrix;
    }

    // this is not working currently
    public void AppendRow(T[] values, bool conform = false)
    {
        if(Cols < values.Length) {
            if(conform) {
                throw new ArgumentOutOfRangeException(nameof(values), "Added row must be less than or equal to row length of existing matrix.");
            } else {
                throw new ArgumentOutOfRangeException(nameof(values), "Added row must be equal to row length of existing matrix.");
            }
        }
        T[,] matrix = new T[Rows + 1, Cols];
        for(int row = 0; row < Rows; row++) {
            Array.Copy(_data, 0, matrix, 0, _data.Length);
        }  
        for(int col = 0; col < values.Length; col++) {
            matrix[Rows, col] = values[col];
        }
        if(conform) {
            // convert n*n index to identity form
            if(values.Length < Rows && Rows < Cols) {
                matrix[Rows, Rows] = T.One;
            }
        }
        _data = matrix;
    }

    public (int Rows, int Cols) GetSize()
    {
        return (Rows: this.Rows, Cols: this.Cols);
    }

    public string Print()
    {
        string output = "";
        for (int row = 0; row < Rows; row++)
        {
            output += "| ";
            for (int col = 0; col < Cols; col++) { output += $"{_data[row, col]} "; }
            output += "|\n";
        }
        return output;
    }
}