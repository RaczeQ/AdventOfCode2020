using System;
using ElvenTools.IO;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstSolver = new FirstSolver();
            var secondSolver = new SecondSolver();
            var menu = new ConsoleMenu(2, "Password Philosophy");
            menu.RegisterAction("First star", firstSolver.Calculate);
            menu.RegisterAction("Second star", secondSolver.Calculate);
            menu.ShowMenu();
        }
    }
}
