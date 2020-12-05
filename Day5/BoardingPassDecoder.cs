using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    public static class BoardingPassDecoder
    {
        public static string Transform(string pass)
        {
            return pass
                .Replace('F', '0')
                .Replace('B', '1')
                .Replace('L', '0')
                .Replace('R', '1');
        }
        public static (int row, int column) DecodePass(string pass)
        {
            // Console.WriteLine(pass);
            int lowerRow = 0;
            int upperRow = 127;
            int lowerColumn = 0;
            int upperColumn = 7;

            for (int r = 0; r < 7; r++)
            {
                int mid = (lowerRow + upperRow) / 2;
                if (pass[r] == '0')
                {
                    upperRow = mid;
                }
                else if (pass[r] == '1')
                {
                    lowerRow = mid + 1;
                }
                // Console.WriteLine($"{lowerRow}, {mid}, {upperRow}");
            }
            int row = upperRow;
            // Console.WriteLine($"{row}");

            for (int c = 7; c < 10; c++)
            {
                int mid = (lowerColumn + upperColumn) / 2;
                if (pass[c] == '0')
                {
                    upperColumn = mid;
                }
                else if (pass[c] == '1')
                {
                    lowerColumn = mid + 1;
                }
                // Console.WriteLine($"{lowerColumn}, {mid}, {upperColumn}");
            }
            int column = upperColumn;
            // Console.WriteLine($"{column}");

            return (row, column);
        }
    }
}
