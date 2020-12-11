using System;
using System.Collections.Generic;
using System.Linq;
using ElvenTools;
using ElvenTools.Utils;

namespace Day11
{
    // Simulate your seating area by applying the seating rules repeatedly until no seats change state. How many seats end up occupied?
    public class FirstSolver : ISolver
    {
        public long Calculate(List<string> input)
        {
            var matrix = new Matrix(input);
            Matrix clone = matrix.Copy();
            var (x, y) = matrix.Dimensions();
            do
            {
                matrix = clone.Copy();
                for (int _x = 0; _x < x; _x++)
                {
                    for (int _y = 0; _y < y; _y++)
                    {
                        if (matrix[_x, _y] == Matrix.FREE && matrix.CloseNeighbors(_x, _y) == 0)
                        {
                            clone[_x, _y] = Matrix.OCCUPIED;
                        }
                        else if (matrix[_x, _y] == Matrix.OCCUPIED && matrix.CloseNeighbors(_x, _y) >= 4)
                        {
                            clone[_x, _y] = Matrix.FREE;
                        }
                    }
                }
            } while (!matrix.Equals(clone));
            return clone.CountOccupied();
        }
    }
}