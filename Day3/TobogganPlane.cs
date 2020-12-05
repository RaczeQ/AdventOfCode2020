using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    public class TobogganPlane
    {
        private const char TreeIdentifier = '#';

        private readonly List<string> _rows;
        private readonly int _width;

        public TobogganPlane(List<string> rows)
        {
            _rows = rows;
            _width = rows[0].Length;
        }

        public bool IsTree(int x, int y)
        {
            return _rows[y][x % _width] == TreeIdentifier;
        }
    }
}
