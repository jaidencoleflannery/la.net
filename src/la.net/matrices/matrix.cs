using System.Numerics;

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

    public T[] GetRow(int row) {
        T[] output = new T[Cols]; 
        for(int col = 0; col < Cols; col++) {
            output[col] = _data[row, col];
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