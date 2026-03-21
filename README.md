# la.net
a simple linear algebra library for accurate vector and matrix calculations.

this library focuses on accuracy in minimal time complexity.

## features
* vectors
* matrices

## installation
run: ```dotnet add package jaidencoleflannery.la.net```

## quick start
barebones example of usage:
```
using Matrices;
using Vectors;

class Program{
    static void Main() {
        var v = new double[] {3.0, 4.8, 9.2};
        Vector vector = new Vector(v);
        Console.WriteLine(vector.ToString());

        var m = new double[,] {
            {3.0, 4.8, 9.2},
            {3.0, 4.1, 5.2}, 
            {9.0, 1.23, 2}
        };
        Matrix matrix = new Matrix(3, 3, m);
        Console.WriteLine(matrix.ToString());
    }
}
```

## contributing
to contribute, ensure your request passes all unit tests before opening a pr.
if you feel as though any of the tests are innacurate or need adjustment, please submit a seperate pr for unit test changes.

run tests: ```cd ./testing/dotnet test```
