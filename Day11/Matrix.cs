using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools.Utils;

namespace Day11
{
    public class Matrix
    {
        public static int FLOOR = 0;
        public static int FREE = 1;
        public static int OCCUPIED = 2;

        private static IList<(int x, int y)> _neighbors = new List<(int x, int y)>
        {
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1), (0, 1),
            (1, -1), (1, 0), (1, 1)
        };

        private readonly int[,] _matrix;

        private Matrix(int[,] matrix)
        {
            _matrix = matrix.Clone() as int[,];
        }

        public Matrix(List<string> input)
        {
            _matrix = new int[input[0].Length, input.Count];
            foreach (var (l, yIndex) in input.WithIndex())
            {
                foreach (var (ch, xIndex) in l.WithIndex())
                {
                    if (ch == 'L')
                    {
                        _matrix[xIndex, yIndex] = FREE;
                    }
                }
            }
        }

        public (int x, int y) Dimensions()
        {
            return (_matrix.GetLength(0), _matrix.GetLength(1));
        }

        public int this[int x, int y]
        {
            get => _matrix[x, y];
            set => _matrix[x, y] = value;
        }

        public long CountOccupied()
        {
            return _matrix.Cast<int>().Count(a => a == OCCUPIED);
        }

        private bool IsValidPos(int x, int y)
        {
            return x >= 0 && x < _matrix.GetLength(0)
                && y >= 0 && y < _matrix.GetLength(1);
        }

        public int CloseNeighbors(int x, int y)
        {
            return _neighbors
                .Where(t => IsValidPos(t.x + x, t.y + y))
                .Select(t => _matrix[t.x + x, t.y + y] == OCCUPIED ? 1 : 0)
                .Sum();
        }

        public int FirstNeighbors(int x, int y)
        {
            var neighbors = 0;
            foreach (var dim in _neighbors)
            {
                var multiplier = 1;
                var currentSeat = 0;
                do
                {
                    var _x = (dim.x * multiplier) + x;
                    var _y = (dim.y * multiplier) + y;
                    if (!IsValidPos(_x, _y))
                    {
                        break;
                    }
                    currentSeat = _matrix[_x, _y];
                    multiplier++;
                } while (currentSeat == FLOOR);

                neighbors += currentSeat == OCCUPIED ? 1 : 0;
            }

            return neighbors;
        }

        public Matrix Copy()
        {
            var m = new Matrix(_matrix);
            return m;
        }

        public bool Equals(Matrix other)
        {
            return _matrix.Rank == other._matrix.Rank &&
                   Enumerable.Range(0, _matrix.Rank).All(dimension =>
                       _matrix.GetLength(dimension) == other._matrix.GetLength(dimension)) &&
                   _matrix.Cast<int>().SequenceEqual(other._matrix.Cast<int>());
        }
    }
}