﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools;
using ElvenTools.IO;

namespace Day12
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(12, "Rain Risk", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}