using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools;
using ElvenTools.IO;

namespace Day16
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(16, "Ticket Translation", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}