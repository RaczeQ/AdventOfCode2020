using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools;
using ElvenTools.IO;

namespace Day15
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(15, "Rambunctious Recitation", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}