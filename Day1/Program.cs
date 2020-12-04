using System;
using ElvenTools.IO;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstSolver = new FirstSolver();
            var secondSolver = new SecondSolver();
            var menu = new ConsoleMenu(1, "Report Repair");
            menu.RegisterAction("First star", firstSolver.Calculate);
            menu.RegisterAction("Second star", secondSolver.Calculate);
            menu.ShowMenu();
        }
    }
}
