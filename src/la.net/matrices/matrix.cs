using System.Numerics;
using System.Text;

namespace Matrices;
public sealed class Matrix : IMatrix
{
    // properties.
    
    public int Cols { get; private set; }
    public int Rows { get; private set; }

    // fields.

    private double[,] _data;

    // constructors.

    public Matrix(int rows, int cols, double[,]? data = null)
    {
        if (rows <= 0) throw new ArgumentOutOfRangeException(nameof(rows), $"{nameof(rows)} must be greater than 0.");
        else if (cols <= 0) throw new ArgumentOutOfRangeException(nameof(cols), $"{nameof(cols)} must be greater than 0.");

        if (data != null && rows < data.GetLength(0)) throw new ArgumentOutOfRangeException(nameof(rows), $"{nameof(rows)} must be greater than or equal to provided data.");
        else if (data != null && cols < data.GetLength(1)) throw new ArgumentOutOfRangeException(nameof(cols), $"{nameof(cols)} must be greater than or equal to provided data.");
        
        this.Cols = cols;
        this.Rows = rows;

        _data = new double[rows, cols];

        // we are traversing the diagonal path of the matrix and need to know where the matrix ends, max is where the final pivot value will lay despite any free variables
        int max = (rows >= cols) ? cols : rows;

        if (data == null) {
            // this turns our remaining zero matrix entries into an identity matrix
            for (int cursor = 0; cursor < max; cursor++) _data[cursor, cursor] = 1.0;
        } else {
            // if matrix is smaller than identity matrix, push smaller dimensional matrix into larger dimensional identity matrix
            if (data.GetLength(0) < rows || data.GetLength(1) < cols) {
                for (int row = 0; row < data.GetLength(0); row++) {
                    for (int col = 0; col < data.GetLength(1); col++) {
                        _data[row, col] = data[row, col];
                    }
                }
                int index = (data.GetLength(0) > data.GetLength(1)) ? data.GetLength(1) : data.GetLength(0);
                // we are traversing the diagonal path of the matrix PAST the provided matrix and turning it into an identity matrix
                if (rows >= cols) max = rows;
                for (int cursor = index; cursor < max; cursor++) {
                    if (cursor < _data.GetLength(0) && cursor < _data.GetLength(1)) {
                        _data[cursor, cursor] = 1.0;
                    }
                    else break;
                }
            } else _data = (double[,])data.Clone(); // we clone so that we have our own matrix in memory
        }
    }

    // operator overloads.

    public double this[int row, int col] {
        get => _data[row, col];
        set => _data[row, col] = value;
    }

    public static Matrix operator +(Matrix a, Matrix b) {
        if (a is null || b is null) throw new ArgumentNullException();
        if(a.Cols != b.Cols || a.Rows != b.Rows) throw new ArgumentException("Dimensions must match.");

        double[,] c = new double[a.Rows, a.Cols];
        for(int row = 0; row < a.Rows; row++) {
            for(int col = 0; col < a.Cols; col++) {
                c[row, col] = a[row, col] + b[row, col];
            }
        }
        return new Matrix(a.Rows, a.Cols, c);
    }

    public static Matrix operator -(Matrix a, Matrix b) {
        if (a is null || b is null) throw new ArgumentNullException();
        if(a.Cols != b.Cols || a.Rows != b.Rows) throw new ArgumentException("Dimensions must match.");

        double[,] c = new double[a.Rows, a.Cols];
        for(int row = 0; row < a.Rows; row++) {
            for(int col = 0; col < a.Cols; col++) {
                c[row, col] = a[row, col] - b[row, col];
            }
        }
        return new Matrix(a.Rows, a.Cols, c);
    } 

    public static bool operator ==(Matrix a, Matrix b) {
        if (a is null || b is null) throw new ArgumentNullException();
        if(a.Cols != b.Cols || a.Rows != b.Rows) return false;

        for(int row = 0; row < a.Rows; row++) {
            for(int col = 0; col < a.Cols; col++) {
                if(a[row, col] != b[row, col]) return false;
            }
        }
        return true;
    }

    public static bool operator !=(Matrix a, Matrix b) {
        if (a is null || b is null) throw new ArgumentNullException();
        if(a.Cols != b.Cols || a.Rows != b.Rows) return true;

        for(int row = 0; row < a.Rows; row++) {
            for(int col = 0; col < a.Cols; col++) {
                if(a[row, col] != b[row, col]) return true;
            }
        }
        return false;
    }

    public override bool Equals(object? obj) => obj is Matrix other && this == other;

    public override int GetHashCode() {
        var hash = new HashCode();
        hash.Add(this.Rows);
        hash.Add(this.Cols);
        for(int row = 0; row < Rows; row++) {
            for(int col = 0; col < Cols; col++) {
                hash.Add(_data[row, col]);
            }
        }
        return hash.ToHashCode();
    }

    public static Matrix operator *(Matrix a, Matrix b) { 
        if(a is null || b is null) throw new ArgumentNullException("Arguments cannot be null.");
        if(a.Cols != b.Rows) throw new ArgumentException("First matrix's num of columns must match second matrix's num of rows.");

        double [,] c = new double[a.Rows, b.Cols];

        for(int leftRow = 0; leftRow < a.Rows; leftRow++) { 
            for(int rightCol = 0; rightCol < b.Cols; rightCol++) {
                double value = 0.0;
                for(int cursor = 0; cursor < a.Cols; cursor++) {
                    value += a[leftRow, cursor] * b[cursor, rightCol];
                }
                c[leftRow, rightCol] = value;
            } 
        }

        return new Matrix(a.Rows, a.Cols, c);
    }

    public double Get(int row, int col)
    {
        if(row < 0 || row >= this.Rows) throw new ArgumentOutOfRangeException(nameof(row), "Index out of range.");
        if(col < 0 || col >= this.Cols) throw new ArgumentOutOfRangeException(nameof(col), "Index out of range.");
        return _data[row, col];
    }

    public double[] GetRow(int row)
    {
        if(row < 0 || row >= Rows) throw new ArgumentOutOfRangeException(nameof(row), "Row index is out of range.");

        double[] output = new double[Cols];
        for (int col = 0; col < Cols; col++) output[col] = _data[row, col];
        return output;
    }

    public void Set(int row, int col, double value)
    {
        if(row < 0 || col < 0 || row >= this.Rows || col >= this.Cols) throw new ArgumentOutOfRangeException(nameof(row), "Index out of range.");
        _data[row, col] = value;
    }

    public void SetRow(int row, double[] values, bool conform = false)
    {
        if(Cols < values.Length) {
            if(conform) throw new ArgumentOutOfRangeException(nameof(row), "Added row must be less than or equal to column length of existing matrix.");
            else throw new ArgumentOutOfRangeException(nameof(row), "Added row must be equal to column length of existing matrix.");
        }
        for (int col = 0; col < Cols; col++) {
            if(col < values.Length) _data[row, col] = values[col]; 
            else _data[row, col] = 0.0;
        }
        if(conform) {
            // convert free n*n indices to identity form
            if(Cols > values.Length) {
                if(row > values.Length) _data[row, row] = 1.0;
            }
        }
    }

    public void PushRow(double[] values, bool conform = false)
    {
        if(Cols < values.Length) {
            if(conform) throw new ArgumentOutOfRangeException(nameof(values), "Added row must be less than or equal to column length of existing matrix.");
            else throw new ArgumentOutOfRangeException(nameof(values), "Added row must be equal to column length of existing matrix.");
        }
        double[,] matrix = new double[Rows + 1, Cols];
        for (int col = 0; col < values.Length; col++) matrix[0, col] = values[col];
        for (int row = 0; row < Rows; row++) {
            for (int col = 0; col < Cols; col++) {
                matrix[row + 1, col] = _data[row, col];
            }
        }
        if(conform) {
            // convert n*n index to identity form
            if(values.Length == 0) {
                matrix[0, 0] = 1.0;
            }
        }
        Rows += 1;
        _data = matrix;
    }

    public void AppendRow(double[] values, bool conform = false)
    {
        if(Cols < values.Length) {
            if(conform) throw new ArgumentOutOfRangeException(nameof(values), "Added row must be less than or equal to row length of existing matrix.");
            else throw new ArgumentOutOfRangeException(nameof(values), "Added row must be equal to row length of existing matrix.");
        }
        double[,] matrix = new double[Rows + 1, Cols];
        // Copy existing rows
        for(int row = 0; row < Rows; row++) {
            for(int col = 0; col < Cols; col++) {
                matrix[row, col] = this._data[row, col];
            }
        }  
        // Set new row values
        for(int col = 0; col < Cols; col++) {
            if(col < values.Length) matrix[Rows, col] = values[col];
            else matrix[Rows, col] = 0.0;
        }
        if(conform) {
            // convert n*n index to identity form
            if(values.Length < Cols) matrix[Rows, values.Length] = 1.0;
        }
        Rows += 1;
        _data = matrix;
    }

    public (int Rows, int Cols) GetSize()
    {
        return (Rows: this.Rows, Cols: this.Cols);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (int row = 0; row < Rows; row++)
        {
            sb.Append("| ");
            for (int col = 0; col < Cols; col++) sb.Append($"{_data[row, col]} ");
            sb.Append("|");
            sb.AppendLine("");
        }
        return sb.ToString();
    }
}
