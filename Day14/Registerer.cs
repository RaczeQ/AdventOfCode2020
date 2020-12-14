using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools;
using ElvenTools.IO;

namespace Day14
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(14, "Docking Data", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}