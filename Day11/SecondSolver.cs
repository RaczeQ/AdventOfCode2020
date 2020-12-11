using System.Collections.Generic;
using System.Linq;
using ElvenTools;

namespace Day11
{
    // Given the new visibility method and the rule change for occupied seats becoming empty, once equilibrium is reached, how many seats end up occupied?
    public class SecondSolver : ISolver
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
                        if (matrix[_x, _y] == Matrix.FREE && matrix.FirstNeighbors(_x, _y) == 0)
                        {
                            clone[_x, _y] = Matrix.OCCUPIED;
                        }
                        else if (matrix[_x, _y] == Matrix.OCCUPIED && matrix.FirstNeighbors(_x, _y) >= 5)
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
