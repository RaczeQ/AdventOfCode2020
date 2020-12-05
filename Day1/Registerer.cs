using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools;
using ElvenTools.IO;

namespace Day1
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(1, "Report Repair", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}