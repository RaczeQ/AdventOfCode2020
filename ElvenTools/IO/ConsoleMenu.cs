using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ElvenTools.Utils;

namespace ElvenTools.IO
{
    public class ConsoleMenu
    {
        private static List<ConsoleColor> _borderColors = new List<ConsoleColor>
        {
            ConsoleColor.Red,
            // ConsoleColor.White,
            ConsoleColor.Green,
            // ConsoleColor.White
        };
        private static List<string> _footerMenuValues = new List<string>
        {
            "Use ↑↓ to navigate actions.",
            "Press ENTER to select action.",
            "Press ESCAPE to go to previous menu."
        };
        public delegate long Calculate(List<string> input);

        private readonly IDictionary<int, string> _days;
        private readonly IDictionary<int, IDictionary<string, Calculate>> _actions;
        private int? _selectedDayIndex;
        private int? _selectedActionIndex;
        private List<string> _selectedActionInput;

        private int? SelectedDayNumber =>
            _selectedDayIndex.HasValue ? _days.ElementAt(_selectedDayIndex.Value).Key : null;

        private string SelectedDayName =>
            _selectedDayIndex.HasValue ? _days.ElementAt(_selectedDayIndex.Value).Value : null;

        private IDictionary<string, Calculate> SelectedDayActions =>
            _selectedDayIndex.HasValue ? _actions.ElementAt(_selectedDayIndex.Value).Value : null;

        private string SelectedActionName => _selectedDayIndex.HasValue && _selectedActionIndex.HasValue
            ? SelectedDayActions.ElementAt(_selectedActionIndex.Value).Key
            : null;

        private Calculate SelectedActionDelegate => _selectedDayIndex.HasValue && _selectedActionIndex.HasValue
            ? SelectedDayActions.ElementAt(_selectedActionIndex.Value).Value
            : null;

        public ConsoleMenu()
        {
            _actions = new Dictionary<int, IDictionary<string, Calculate>>();
            _days = new Dictionary<int, string>();
        }

        public void RegisterDay(int day, string name)
        {
            _days.Add(day, name);
            _actions.Add(day, new Dictionary<string, Calculate>());
        }

        public void RegisterAction(int day, string name, Calculate delegateMethod)
        {
            _actions[day].Add(name, delegateMethod);
        }

        public void ShowMenu()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            select_day:
            _selectedDayIndex = null;
            _selectedDayIndex = ShowDays();
            if (_selectedDayIndex == null)
            {
                goto close_menu;
            }

            select_action:
            _selectedActionIndex = null;
            _selectedActionIndex = ShowActions();
            if (_selectedActionIndex == null)
            {
                goto select_day;
            }

            read_input:
            _selectedActionInput = null;
            var actionMenuResult = GetActionInput();
            switch (actionMenuResult)
            {
                case null:
                case 2:
                    goto select_action;
                case 3:
                    goto select_day;
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
                    goto select_day;
                case 4:
                    goto close_menu;
                default:
                    goto read_input;
            }

            close_menu:
            Console.Clear();
            Console.WriteLine("Press ENTER to close window...");
            Console.ReadLine();
        }

        private void PrintBorder(int totalRows)
        {
            int width = Console.WindowWidth;

            var topRow = Enumerable.Range(0, width).Select(x => (x: x, y: 0));
            var rightColumn = Enumerable.Range(1, totalRows - 2).Select(y => (x: width - 1, y: y));
            var bottomRow = Enumerable.Range(0, width).Select(x => (x: width - x - 1, y: totalRows - 1));
            var leftColumn = Enumerable.Range(1, totalRows - 2).Select(y => (x: 0, y: totalRows - y - 1));
            var allPositions = topRow.Concat(rightColumn).Concat(bottomRow).Concat(leftColumn).ToList();
            foreach (var (position, n) in allPositions.WithIndex())
            {
                Console.BackgroundColor = _borderColors[n % _borderColors.Count];
                Console.SetCursorPosition(position.x, position.y);
                Console.Write(" ");
            }

            Console.BackgroundColor = ConsoleColor.White;
        }

        private void WriteMenuToConsole(List<string> headerLines, string title, List<string> optionsLines, List<string> footerLines, int? index)
        {
            Console.Clear();
            Console.CursorVisible = false;
            var allLines = headerLines.Concat(optionsLines).Concat(footerLines).ToList();
            int maxWidth = allLines.Select(l => l.Length).Max();
            int height = allLines.Count();

            int rowPosition = 2;
            
            foreach (var (headerLine, n) in headerLines.WithIndex())
            {
                Console.SetCursorPosition((Console.WindowWidth - headerLine.Length) / 2, ++rowPosition);
                Console.WriteLine(headerLine);
            }

            rowPosition += 2;
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, rowPosition);
            Console.WriteLine(title);
            Console.WriteLine();

            rowPosition += 2;
            foreach (var (actionName, n) in optionsLines.WithIndex())
            {
                if (n == index)
                {
                    Console.BackgroundColor = n % 2 == 0 ? ConsoleColor.Red : ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(2, rowPosition);
                    Console.Write(" ");
                    // Console.BackgroundColor = n % 2 == 0 ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen;
                }

                Console.SetCursorPosition(4, rowPosition++);
                Console.Write(actionName);

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            
            Console.SetCursorPosition(2, ++rowPosition);
            Console.Write(string.Empty.PadRight(Console.WindowWidth - 4, '-'));
            rowPosition++;
            foreach (var footer in footerLines)
            {
                Console.SetCursorPosition(4, ++rowPosition);
                Console.WriteLine(footer);
            }

            rowPosition += 3;

            PrintBorder(rowPosition);
        }

        private int? ShowMenuList(string title, List<string> options)
        {
            int? result = 0;
            ConsoleKeyInfo pressedKey;
            do
            {
                var headerLines = new List<string>();
                headerLines.Add("AoC 2020");
                if (_selectedDayIndex.HasValue)
                {
                    var tempStr = $"Day {SelectedDayNumber} - {SelectedDayName}";
                    if (_selectedActionIndex.HasValue)
                    {
                        tempStr += $" ({SelectedActionName})";
                    }

                    headerLines.Add(tempStr);
                }

                WriteMenuToConsole(headerLines, title, options, _footerMenuValues, result);

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

        private int? ShowDays()
        {
            int? selectedDay = ShowMenuList("Select day", _days.Select(kv => $"Day {kv.Key} - {kv.Value}").ToList());
            return selectedDay;
        }

        private int? ShowActions()
        {
            int? selectedAction = null;
            if (_selectedDayIndex != null)
            {
                selectedAction = ShowMenuList("Select action",
                    _actions.ElementAt(_selectedDayIndex.Value).Value.Keys.ToList());
            }

            return selectedAction;
        }

        private List<string> ReadInputFromConsole()
        {
            List<string> lines = new List<string>();
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Provide input (Submit with END key):");
            StringBuilder sb = new StringBuilder();
            ConsoleKeyInfo pressedKey;
            while ((pressedKey = Console.ReadKey(true)).Key != ConsoleKey.End)
            {
                if (pressedKey.Key == ConsoleKey.Enter)
                {
                    lines.Add(sb.ToString());
                    sb.Clear();
                    Console.WriteLine();
                }
                else
                {
                    sb.Append(pressedKey.KeyChar);
                    Console.Write(pressedKey.KeyChar);
                }
            }

            if (sb.Length > 0)
            {
                lines.Add(sb.ToString());
                sb.Clear();
            }

            return lines;
        }

        private List<string> ReadInputFromFile()
        {
            List<string> lines = new List<string>();

            Console.Clear();
            Console.CursorVisible = true;
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

        private int? GetActionInput()
        {
            List<string> input = null;
            var actions = new List<string>
                {"Paste input into console", "Load input from txt file", "Change action", "Change day"};
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

            _selectedActionInput = input;
            return selectedAction;
        }

        private int? ExecuteAction()
        {
            string resultDesc;
            Console.Clear();
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                var result = SelectedActionDelegate(_selectedActionInput);
                watch.Stop();
                resultDesc = $"Result: {result} ({watch.ElapsedMilliseconds} ms)";
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
            {
                "Execute action again", "Change action input", "Go to action select menu", "Go to day select menu",
                "Close menu"
            };
            int? selectedAction = ShowMenuList(resultDesc, actions);

            return selectedAction;
        }
    }
}