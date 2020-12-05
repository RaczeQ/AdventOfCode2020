using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElvenTools.IO;

namespace ElvenTools
{
    public abstract class ActionRegisterer
    {
        private readonly int _dayNumber;
        private readonly string _dayName;
        private readonly ConsoleMenu.Calculate _firstSolver;
        private readonly ConsoleMenu.Calculate _secondSolver;

        protected ActionRegisterer(int dayNumber, string dayName, ConsoleMenu.Calculate firstSolver, ConsoleMenu.Calculate secondSolver)
        {
            _dayNumber = dayNumber;
            _dayName = dayName;
            _firstSolver = firstSolver;
            _secondSolver = secondSolver;
        }

        public void RegisterActions(ref ConsoleMenu menu)
        {
            menu.RegisterDay(_dayNumber, _dayName);
            menu.RegisterAction(_dayNumber, "First star", _firstSolver);
            menu.RegisterAction(_dayNumber, "Second star", _secondSolver);
        }
    }
}
