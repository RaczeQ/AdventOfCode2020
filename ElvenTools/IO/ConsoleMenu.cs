using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ElvenTools.IO
{
    public class ConsoleMenu
    {
        public delegate int Calculate(List<string> input);

        private readonly int _day;
        private readonly string _name;
        private readonly IDictionary<string, Calculate> _actions;
        private int? _selectedActionIndex;
        private List<string> _selectedActionInput;

        private string SelectedActionName => _selectedActionIndex.HasValue
            ? _actions.ElementAt(_selectedActionIndex.Value).Key
            : null;

        private Calculate SelectedActionDelegate => _selectedActionIndex.HasValue
            ? _actions.ElementAt(_selectedActionIndex.Value).Value
            : null;

        public ConsoleMenu(int day, string name)
        {
            _day = day;
            _name = name;
            _actions = new Dictionary<string, Calculate>();
        }

        public void RegisterAction(string name, Calculate delegateMethod)
        {
            _actions.Add(name, delegateMethod);
        }

        public void ShowMenu()
        {
            select_action:
            _selectedActionIndex = ShowActions();
            if (_selectedActionIndex == null)
            {
                goto close_menu;
            }

            read_input:
            _selectedActionInput = GetActionInput();
            if (_selectedActionInput == null)
            {
                goto select_action;
            }

            execute_action:
            var executionMenuResult = ExecuteAction();
            switch (executionMenuResult)
            {
                case 0:
                    goto execute_action;
                case 2:
                    goto select_action;
                case 3:
                    goto close_menu;
                case 1:
                default:
                    goto read_input;
            }

            close_menu:
            Console.Clear();
            Console.WriteLine("Press ENTER to close window...");
            Console.ReadLine();
        }

        private int? ShowMenuList(string title, List<string> options)
        {
            int? result = 0;
            ConsoleKeyInfo pressedKey;
            do
            {
                Console.Clear();
                Console.Write($"Day {_day} - {_name} ");
                if (_selectedActionIndex.HasValue)
                {
                    Console.Write($"({SelectedActionName})");
                }

                Console.WriteLine($"\n{title}\n");

                foreach (var (actionName, index) in options.WithIndex())
                {
                    if (result == index)
                    {
                        Console.Write("- ");
                    }

                    Console.WriteLine($"{actionName}");
                }

                Console.WriteLine($"\n-------------------------------------\n");
                Console.WriteLine("Use ↑↓ to navigate actions.");
                Console.WriteLine("Press ENTER to select action.");
                Console.WriteLine("Press ESCAPE to go to previous menu.");

                pressedKey = Console.ReadKey(false);
                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    result = --result > -1 ? result : options.Count - 1;
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    result = (result + 1) % options.Count;
                }
            } while (pressedKey.Key != ConsoleKey.Enter && pressedKey.Key != ConsoleKey.Escape);

            if (pressedKey.Key == ConsoleKey.Escape)
            {
                result = null;
            }

            return result;
        }

        private int? ShowActions()
        {
            int? selectedAction = ShowMenuList("Select action", _actions.Keys.ToList());
            return selectedAction;
        }

        private List<string> ReadInputFromConsole()
        {
            List<string> lines = new List<string>();
            Console.Clear();
            Console.WriteLine("Provide input:");
            string line;
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                lines.Add(line);
            }

            return lines;
        }

        private List<string> ReadInputFromFile()
        {
            List<string> lines = new List<string>();

            Console.Clear();
            Console.Write("Provide file path: ");
            string filePath = Console.ReadLine();
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    using (FileStream fs = File.Open(filePath, FileMode.Open))
                    {
                        byte[] b = new byte[1024];
                        UTF8Encoding temp = new UTF8Encoding(true);
                        while (fs.Read(b, 0, b.Length) > 0)
                        {
                            lines.Add(temp.GetString(b));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid file");
                }
            }

            return lines;
        }

        private List<string> GetActionInput()
        {
            List<string> input = null;
            var actions = new List<string> {"Paste input into console", "Load input from txt file", "Change action"};
            int? selectedAction = ShowMenuList($"Select data input method", actions);
            switch (selectedAction)
            {
                case 0:
                    input = ReadInputFromConsole();
                    break;
                case 1:
                    input = ReadInputFromFile();
                    break;
            }

            return input;
        }

        private int? ExecuteAction()
        {
            string resultDesc;
            Console.Clear();
            try
            {
                var result = SelectedActionDelegate(_selectedActionInput);
                resultDesc = $"Result: {result}";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while executing method");
                Console.WriteLine(ex);
                Console.WriteLine("\nPress ENTER to go further...");
                resultDesc = "Error while executing method";
                Console.ReadLine();
            }

            var actions = new List<string>
                {"Execute action again", "Change action input", "Go to action select menu", "Close menu"};
            int? selectedAction = ShowMenuList(resultDesc, actions);

            return selectedAction;
        }
    }
}