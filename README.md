# la.net
A library for accurate Linear Algebra calculations.

## Overview
A simple Linear Algebra library for accurate Vector and Matrix calculations.
This library is not memory efficient, instead it attempts to be extremely accurate in minimal time complexity.

## Features
* Vectors
* Matrices

## Installation
Run: ```dotnet add package jaidencoleflannery.la.net```

## Quick Start
Barebones example of usage:
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

## Contributing
To contribute, ensure your request passes all unit tests before opening a PR.
Run tests: ```cd ./testing/dotnet test```
