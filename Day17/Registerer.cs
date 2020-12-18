using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools;
using ElvenTools.IO;

namespace Day17
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(17, "Conway Cubes", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}