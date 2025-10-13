using System.Drawing;
using System.Numerics;
using System.Reflection.Metadata;

namespace Matrices;
public sealed class Matrix<T> where T : INumber<T> {
    public int Cols { get; set; }
    public int Rows { get; set; }
    private T[,] _data;
    public Matrix(int rows, int cols, T[,]? data = null) {
        if(rows <= 0) { 
            throw new ArgumentOutOfRangeException(nameof(rows), $"{nameof(rows)} must be greater than 0.");
        } else if(cols <= 0) { 
            throw new ArgumentOutOfRangeException(nameof(cols), $"{nameof(cols)} must be greater than 0."); 
        }

        if(data != null && rows < data.GetLength(0)) {
            throw new ArgumentOutOfRangeException(nameof(rows), $"{nameof(rows)} must be greater than or equal to provided data.");
        } else if(data != null && cols < data.GetLength(1)) {
            throw new ArgumentOutOfRangeException(nameof(cols), $"{nameof(cols)} must be greater than or equal to provided data.");
        }

        this.Cols = cols;
        this.Rows = rows;

        _data = new T[rows, cols];

        if(data == null) {
            int max;
            // we are traversing the diagonal path of the matrix and need to know where the matrix ends, max is where the final pivot value will lay despite any free variables
            if(rows >= cols) {
                max = cols;
            } else {
                max = rows;
            }
            // this turns our zero matrix into an identity matrix
            for(int cursor = 0; cursor < max; cursor++) { 
                _data[cursor, cursor] = T.One;
            }
        } else {
            // push smaller dimensional matrix into larger dimensional identity matrix
            if(data.GetLength(0) < rows || data.GetLength(1) < cols) {
                for(int row = 0; row < data.GetLength(0); row++) {
                    for(int col = 0; col < data.GetLength(1); col++) {
                            _data[row, col] = data[row, col];
                    }
                }
                int index = (data.GetLength(0) > data.GetLength(1)) ? data.GetLength(1) : data.GetLength(0);
                int max;
                // we are traversing the diagonal path of the matrix PAST the provided matrix and turning it into an identity matrix
                if(rows >= cols) {
                    max = rows;
                } else {
                    max = cols;
                }
                for(int cursor = index; cursor < max; cursor++) {
                    if(cursor < _data.GetLength(0) && cursor < _data.GetLength(1)) {
                        _data[cursor, cursor] = T.One;
                    } else {
                        break;
                    }
                }
            } else {
                _data = (T[,])data.Clone();
            }
        }
    }

    public T Get(int row, int col) {
        return _data[row, col];
    }

    public T[,] GetRow(int row) {
        T[,] output = new T[1, Cols]; 
        for(int col = 0; col < Cols; col++) {
            output[0, col] = _data[row, col];
        }
        return output;
    }

    public void Set(int row, int col, T value) {
        _data[row, col] = value;
    }

    public void SetRow(int row, T[] value) {
        for(int col = 0; col < value.Length; col++) {
            _data[row, col] = value[col];
        }
    }

    public void PushRow(T[] value) {
        if(value.Length != Cols) {
            throw new ArgumentOutOfRangeException(nameof(Rows), $"{nameof(Rows)} must match existing matrix's length."); 
        }
        T[,] matrix = new T[Rows + 1, Cols];
        for(int col = 0; col < Cols; col++) {
            matrix[0, col] = value[col];
        }
        for(int row = 0; row < Rows; row++) {
            for(int col = 0; col < Cols; col++) {
                matrix[row + 1, col] = _data[row, col];
            }
        }
        Rows += 1;
        _data = matrix;
    }

    public void AppendRow(T[] value) {
        this.Cols++;
        Matrix<T> tempMatrix = new (Rows, Cols, _data);
        tempMatrix.SetRow(Cols, value);
    }

    public (int Rows, int Cols) GetSize() {
        return ( Rows: _data.GetLength(0), Cols: _data.GetLength(1) ) ;
    }

    public string Print() {
        string output = "";
        for(int row = 0; row < Rows; row++) {
            output += "| ";
            for(int col = 0; col < Cols - 1; col++) { output += $"{_data[row, col]} "; }
            output += "|\n";
        }
        return output;
    }
}