using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools;
using ElvenTools.IO;

namespace Day11
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(11, "Seating System", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}