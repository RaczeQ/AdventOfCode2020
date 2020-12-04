using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElvenTools
{
    public interface IBaseSolver
    {
        int Calculate(List<string> input);
    }
}