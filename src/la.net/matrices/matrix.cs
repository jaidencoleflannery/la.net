namespace Matrices;
public sealed class Matrix {
    public int Cols { get; }
    public int Rows { get; }
    private readonly double[,] _data;
    public Matrix (int rows, int cols, double[,] data) {
        // check if rows and cols are valid
        if(rows <= 0) { 
            throw new ArgumentOutOfRangeException(nameof(rows), $"{nameof(rows)} must be greater than 0.") 
        } else if(cols <= 0) { 
            throw new ArgumentOutOfRangeException(nameof(cols), $"{nameof(cols)} must be greater than 0."); 
        }
        if(data == null) {
            // create empty matrix on init if no data is provided
            _data = new double[rows, cols]; 
        } else {
            // if data provided: check if rows and cols match actual data
            if(data.GetLength(0) != rows) {
                throw new ArgumentException(nameof(rows), $"{nameof(rows)} must be equal to the number of columns in the array.");
            } else if (data.GetLength(1) != cols) {
                throw new ArgumentException(nameof(cols), $"{nameof(cols)} must be equal to the number of columns in the array.");
            } else {
                this.Cols = cols;
                this.Rows = rows;
                // create a clone of the param so if the ref is changed our obj has a private instance (typecasted because .Clone() returns an object)
                _data = (double[,])data.Clone();
            }
        }
    }

    public double Get(int row, int col) {
        return _data[row, col];
    }
}