using ElvenTools.IO;

namespace SolverExecutor
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new ConsoleMenu();
            new Day1.Registerer().RegisterActions(ref menu);
            new Day2.Registerer().RegisterActions(ref menu);
            new Day3.Registerer().RegisterActions(ref menu);
            new Day4.Registerer().RegisterActions(ref menu);
            
            menu.ShowMenu();
        }
    }
}
