using System;
using System.Reflection;
using ElvenTools;
using ElvenTools.IO;

namespace SolverExecutor
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new ConsoleMenu();
            for (int i = 1; i < 25; i++)
            {
                var name = $"Day{i}";
                try
                {
                    var dayAssembly = Assembly.Load(name);
                    Type t = dayAssembly.GetType($"{name}.Registerer");
                    var registerer = (ActionRegisterer)Activator.CreateInstance(t);
                    registerer.RegisterActions(ref menu);
                }
                catch
                {
                    // pass
                }
            }
            menu.ShowMenu();
        }
    }
}