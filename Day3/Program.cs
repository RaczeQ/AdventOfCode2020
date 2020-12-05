using System;
using ElvenTools.IO;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstSolver = new FirstSolver();
            var secondSolver = new SecondSolver();
            var menu = new ConsoleMenu(3, "Toboggan Trajectory");
            menu.RegisterAction("First star", firstSolver.Calculate);
            menu.RegisterAction("Second star", secondSolver.Calculate);
            menu.ShowMenu();
        }
    }
}
