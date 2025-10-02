using System.Numerics;

namespace Matrices;
public sealed class Matrix<T> where T : INumber<T> {
    public int Cols { get; }
    public int Rows { get; }
    private readonly T[,] _data;
    public Matrix(int rows, int cols, T[,]? data = null) {
        if(rows <= 0) { 
            throw new ArgumentOutOfRangeException(nameof(rows), $"{nameof(rows)} must be greater than 0.");
        } else if(cols <= 0) { 
            throw new ArgumentOutOfRangeException(nameof(cols), $"{nameof(cols)} must be greater than 0."); 
        }
        if(data == null) {
            _data = new T[rows, cols]; 
        } else {
            // if data provided: check if rows and cols match actual data
            if(data.GetLength(0) != rows) {
                throw new ArgumentException(nameof(rows), $"{nameof(rows)} must be equal to the number of rows in the array.");
            } else if (data.GetLength(1) != cols) {
                throw new ArgumentException(nameof(cols), $"{nameof(cols)} must be equal to the number of columns in the array.");
            } else {
                this.Cols = cols;
                this.Rows = rows;
                // create a clone of the param so if the ref is changed our obj has a private instance
                _data = (T[,])data.Clone();
            }
        }
    }

    public T Get(int row, int col) {
        return _data[row, col];
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
        // NEED TO ACTUALLY IMPLEMENT THIS - MIGHT NEED TO SWITCH TO JAGGED ARRAYS TO PRESERVE MEMORY SPACE [][]
        int row = 0;
        for(int col = 0; col < value.Length; col++) {
            _data[row, col] = value[col];
        }
    }

    public void AppendRow(T[] value) {
        int row = Rows + 1;
        for(int col = 0; col < value.Length; col++) {
            _data[row, col] = value[col];
        }
    }

    public (int Rows, int Cols) GetSize() {
        return ( Rows: _data.GetLength(0), Cols: _data.GetLength(1) ) ;
    }

    public string Print() {
        string output = "";
        for(int row = 0; row < Rows; row++) {
            output += "| ";
            for(int col = 0; col < Cols; col++) { output += $"{_data[row, col]} "; }
            output += "|\n";
        }
        return output;
    }
}