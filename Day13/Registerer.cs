using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools;
using ElvenTools.IO;

namespace Day13
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(13, "Shuttle Search", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}