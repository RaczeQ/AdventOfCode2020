using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools;
using ElvenTools.IO;

namespace Day10
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(10, "Adapter Array", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}