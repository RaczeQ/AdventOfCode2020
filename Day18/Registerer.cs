using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools;
using ElvenTools.IO;

namespace Day18
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(18, "Operation Order", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}