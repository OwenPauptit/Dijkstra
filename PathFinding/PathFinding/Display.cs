using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PathFinding
{
    static class Display
    {
        public enum Points
        {
            start = ConsoleColor.Green,
            end = ConsoleColor.Red,
            wall = ConsoleColor.White
        }

        private const int SMALLESTWIDTH = 20;
        public static int WINDOWWIDTH { get; private set; } = 40;
        public static int WINDOWHEIGHT { get; private set; } = 20;
        public static ConsoleColor DEFAULTCOLOUR { get; private set; } = ConsoleColor.Black;
        public static int DELAY { get; private set; } = 10;

        public static int REFRESHRATE { get; private set; } = 30;

        public static class Grid
        {

            public enum Distances
            {
                diagonal = 3,
                straight = 2
            }

            private static ConsoleColor[,] _staticGrid;
            private static ConsoleColor[,] _dynamicGrid;

            private static int[] _endPoint;
            private static int[] _startPoint;

            public static int[] EndPoint
            {
                get { return (_endPoint != null) ? _endPoint : new int[] { -1, -1 }; }
                set
                {
                    if (value[0] <= _staticGrid.GetLength(1) && value[1] <= _staticGrid.GetLength(0) && value[0] > 0 && value[1] > 0)
                    {
                        try
                        {
                            if (_endPoint != null)
                            {
                                _staticGrid[_endPoint[1], _endPoint[0]] = DEFAULTCOLOUR;
                            }

                            _staticGrid[value[1], value[0]] = (ConsoleColor)Points.end;

                            _endPoint = new int[] { value[0], value[1] };

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: ", e);
                            Environment.Exit(-1);
                        }
                    }
                }
            }
            public static int[] StartPoint
            {
                get { return (_startPoint != null) ? _startPoint : new int[] { -1, -1 }; }
                set
                {

                    if (value[0] <= _staticGrid.GetLength(1) && value[1] <= _staticGrid.GetLength(0) && value[0] > 0 && value[1] > 0)
                    {
                        try
                        {

                            if (_startPoint != null)
                            {
                                _staticGrid[_startPoint[1], _startPoint[0]] = DEFAULTCOLOUR;
                            }

                            _staticGrid[value[1], value[0]] = (ConsoleColor)Points.start;

                            _startPoint = new int[] { value[0], value[1] };

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: ", e);
                            Environment.Exit(-1);
                        }
                    }
                }
            }

            public static int Width { get { return _staticGrid.GetLength(1); } }
            public static int Height { get { return _staticGrid.GetLength(0); } }
            public static void SetUp()
            {
                _staticGrid = new ConsoleColor[WINDOWHEIGHT - 1, WINDOWWIDTH/2];
                _dynamicGrid = new ConsoleColor[WINDOWHEIGHT - 1, WINDOWWIDTH/2];

                Fill(DEFAULTCOLOUR);

                _dynamicGrid = _staticGrid.Clone() as ConsoleColor[,];

            }
            public static void Display()
            {
                Console.Clear();
                for (int i = 0; i < _dynamicGrid.GetLength(0); ++i)
                {
                    for (int k = 0; k < _dynamicGrid.GetLength(1); ++k)
                    {

                        Console.ForegroundColor = _dynamicGrid[i, k];
                        Console.Write("██");
                    }
                }
            }

            private static void Fill(ConsoleColor c)
            {
                for (int i = 0; i < _staticGrid.GetLength(0); ++i)
                {
                    for (int k = 0; k < _staticGrid.GetLength(1); ++k)
                    {
                        _staticGrid[i, k] = c;
                    }
                }
            }

            public static void ClearAll()
            {
                for (int i = 0; i < _staticGrid.GetLength(0); ++i)
                {
                    for (int k = 0; k < _staticGrid.GetLength(1); ++k)
                    {
                        _staticGrid[i, k] = DEFAULTCOLOUR;
                    }
                }
            }
            public static void ClearDynamicGrid()
            {
                _dynamicGrid = _staticGrid.Clone() as ConsoleColor[,];
            }

            private static void SetRandomPoints()
            {
                int x, y;
                foreach (Points p in Enum.GetValues(typeof(Points)))
                {
                    if (p != Points.wall)
                    {
                        do
                        {
                            x = Program.rnd.Next(0, WINDOWWIDTH);
                            y = Program.rnd.Next(0, _staticGrid.GetLength(0));

                        } while (_staticGrid[y, x] != DEFAULTCOLOUR);

                        _staticGrid[y, x] = (ConsoleColor)p;
                    }
                }
            }

            public static void SetStaticPoint(int x, int y, Points p)
            {
                if (x <= _staticGrid.GetLength(1) && y <= _staticGrid.GetLength(0) && x > 0 && y > 0)
                {
                    try
                    {
                        _staticGrid[y, x] = (ConsoleColor)p;
                        switch (p)
                        {
                            case Points.start:
                                _startPoint = new int[] { x, y };
                                break;
                            case Points.end:
                                _endPoint = new int[] { x, y };
                                break;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: ", e);
                        Environment.Exit(-1);
                    }
                }
            }

            public static void AddWalls(int[][] walls)
            {
                foreach (var w in walls)
                {
                    if (w[0] < _staticGrid.GetLength(1) && w[1] <= _staticGrid.GetLength(0) && w[0] >= 0 && w[1] >= 0)
                    {
                        try
                        {
                            _staticGrid[w[1], w[0]] = (ConsoleColor)Points.wall;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: ", e);
                            Environment.Exit(-1);
                        }
                    }
                }
            }

            public static void AddDynamicPoint(int x, int y, ConsoleColor c)
            {
                if (!(x >= _dynamicGrid.GetLength(1) || y >= _dynamicGrid.GetLength(0) || x < 0 || y < 0))
                {
                    if (_dynamicGrid[y, x] != (ConsoleColor)Points.end && _dynamicGrid[y, x] != (ConsoleColor)Points.start && _dynamicGrid[y, x] != (ConsoleColor)Points.wall)
                    {

                        try
                        {


                            _dynamicGrid[y, x] = c;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: ", e);
                            Environment.Exit(-1);
                        }
                    }
                }
            }

            public static void RemovePoint(int x, int y)
            {
                if (!(x >= _dynamicGrid.GetLength(1) || y >= _dynamicGrid.GetLength(0) || x < 0 || y < 0))
                {
                    try
                    {

                        _dynamicGrid[y, x] = _staticGrid[y, x];

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: ", e);
                        Environment.Exit(-1);
                    }
                }
            }

            public static bool IsDynamicPoint(int x, int y, Points p)
            {
                if (!(x >= _dynamicGrid.GetLength(1) || y >= _dynamicGrid.GetLength(0) || x < 0 || y < 0))
                {
                    try
                    {

                        return _dynamicGrid[y, x] == (ConsoleColor)p;

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: ", e);
                        Environment.Exit(-1);
                    }
                }
                return false;
            }
            public static bool IsStaticPoint(int x, int y, Points p)
            {
                if (!(x >= _staticGrid.GetLength(1) || y >= _staticGrid.GetLength(0) || x < 0 || y < 0))
                {
                    try
                    {

                        return _staticGrid[y, x] == (ConsoleColor)p;

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: ", e);
                        Environment.Exit(-1);
                    }
                }
                return false;
            }

            public static List<int[]> GetAvailableCells(int x, int y)
            {

                var available = new List<int[]>();

                if (x <= _staticGrid.GetLength(1) && y <= _staticGrid.GetLength(0) && x > 0 && y > 0)
                {
                    // left, up, right, down
                    if (x > 0 && _staticGrid[y, x - 1] != (ConsoleColor)Points.wall)
                    {
                        available.Add(new int[] { x - 1 ,y,(int)Distances.straight});
                    }

                    if (y > 0 && _staticGrid[y - 1, x] != (ConsoleColor)Points.wall)
                    {
                        available.Add(new int[] { x,y - 1, (int)Distances.straight });
                    }

                    if (x < _staticGrid.GetLength(1) - 2 && _staticGrid[y, x + 1] != (ConsoleColor)Points.wall)
                    {
                        available.Add(new int[] { x + 1 ,y, (int)Distances.straight });
                    }

                    if (y < _staticGrid.GetLength(0) - 1 && _staticGrid[y + 1, x] != (ConsoleColor)Points.wall)
                    {
                        available.Add(new int[] { x, y + 1, (int)Distances.straight });
                    }

                    // diagonals
                    if (x > 0 && y > 0 && _staticGrid[y - 1, x - 1] != (ConsoleColor)Points.wall)
                    {
                        available.Add(new int[] { x - 1, y - 1, (int)Distances.diagonal });
                    }

                    if (y > 0 && x < _staticGrid.GetLength(1) - 2 && _staticGrid[y - 1, x + 1] != (ConsoleColor)Points.wall)
                    {
                        available.Add(new int[] { x + 1, y - 1, (int)Distances.diagonal });
                    }

                    if (y < _staticGrid.GetLength(0) - 1 && x < _staticGrid.GetLength(1) - 2 && _staticGrid[y + 1, x + 1] != (ConsoleColor)Points.wall)
                    {
                        available.Add(new int[] { x + 1, y + 1, (int)Distances.diagonal });
                    }

                    if (y < _staticGrid.GetLength(0) - 1 && x > 0 && _staticGrid[y + 1, x - 1] != (ConsoleColor)Points.wall)
                    {
                        available.Add(new int[] { x - 1, y + 1, (int)Distances.diagonal });
                    }
                }

                return available;

            }
        }

        public static class MainMenu
        {

            private static int _choice;
            enum MenuOptions
            {
                Random = 1,
                LoadFile,
                EnterCoords,
                Help,
                Settings,
                Quit
            }

            enum SettingsOptions
            {
                ChangeGridSize = 1,
                ChangeRefresh,
                ChangeDelay,
                Quit
            }

            public static void Run()
            {

                while (true)
                {

                    Display();
                    GetInput();
                }

            }

            private static void Display()
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("    DIJKSTRA'S SHORTEST PATH ALGORITHM");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Main Menu: ");
                Console.WriteLine();
                Console.WriteLine(" {0}. Generate Random Grid", ((int)MenuOptions.Random).ToString());
                Console.WriteLine(" {0}. Load Grid from textfile", ((int)MenuOptions.LoadFile).ToString());
                Console.WriteLine(" {0}. Manually Enter Grid Coords", ((int)MenuOptions.EnterCoords).ToString());
                Console.WriteLine(" {0}. Help", ((int)MenuOptions.Help).ToString());
                Console.WriteLine(" {0}. Settings", ((int)MenuOptions.Settings).ToString());
                Console.WriteLine(" {0}. Quit", ((int)MenuOptions.Quit).ToString());
                Console.WriteLine();
            }

            private static void GetInput()
            {
                _choice = 0;
                Int32.TryParse(Console.ReadLine(), out _choice);

                Console.Clear();
                switch (_choice)
                {
                    case (int)MenuOptions.Random:
                        Console.WriteLine("Coming Soon!!!");
                        break;
                    case (int)MenuOptions.LoadFile:
                        LoadFromFile();
                        Program.Run();
                        break;
                    case (int)MenuOptions.EnterCoords:
                        Reader.ReadInCoords();
                        Program.Run();
                        break;
                    case (int)MenuOptions.Help:
                        DisplayHelpMenu();
                        break;
                    case (int)MenuOptions.Settings:
                        Settings();
                        break;
                    case (int)MenuOptions.Quit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }

            }

            private static void LoadFromFile()
            {
                string fileName;
                Console.WriteLine();
                Console.WriteLine("Enter the filename: ");
                fileName = Console.ReadLine();

                while (!System.IO.File.Exists(fileName) || fileName.Substring(fileName.Length - 3) != "txt")
                {
                    Console.WriteLine("Invalid input, try again");
                    fileName = Console.ReadLine();
                }

                Reader.ReadFromTextFile(fileName);
            }

            private static void DisplayHelpMenu()
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("    DIJKSTRA'S SHORTEST PATH ALGORITHM");
                Console.WriteLine();
                Console.WriteLine("Help Menu:");
                Console.WriteLine();
                Console.WriteLine(" - Read about this algorithm at:");
                Console.WriteLine("https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm");
                Console.WriteLine();
                Console.WriteLine(" - When running the program, press esc  to return to the main menu");
                Console.WriteLine();
                Console.WriteLine(" - The grid is set at default to 20 x 20");
                Console.WriteLine();
                Console.WriteLine(" - For help with files, see 'help.txt'");
                Console.WriteLine();
                Console.WriteLine("Press any key to return to the main menu");
                Console.ReadKey();
                Console.Clear();
            }

            private static void DisplaySettingsMenu()
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("    DIJKSTRA'S SHORTEST PATH ALGORITHM");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Settings Menu:");
                Console.WriteLine();
                Console.WriteLine(" {0}. Change Grid Size", ((int)SettingsOptions.ChangeGridSize).ToString());
                Console.WriteLine(" {0}. Change Refresh Rate", ((int)SettingsOptions.ChangeRefresh).ToString());
                Console.WriteLine(" {0}. Change Delay", ((int)SettingsOptions.ChangeDelay).ToString());
                Console.WriteLine(" {0}. Return to Main Menu", ((int)SettingsOptions.Quit).ToString());
                Console.WriteLine();

            }

            private static void Settings()
            {
                int choice;

                while (true)
                {

                    DisplaySettingsMenu();

                    choice = 0;
                    Int32.TryParse(Console.ReadLine(), out choice);


                    Console.Clear();
                    switch (choice)
                    {
                        case (int)SettingsOptions.ChangeGridSize:
                            ChangeSize();
                            break;
                        case (int)SettingsOptions.ChangeRefresh:
                            ChangeRefreshRate();
                            break;
                        case (int)SettingsOptions.ChangeDelay:
                            ChangeDelay();
                            break;
                        case (int)SettingsOptions.Quit:
                            return;
                        default:
                            Console.WriteLine("Invalid Input");
                            break;
                    }
                }
            }
        }

        public static void Create()
        {
            Console.SetWindowSize(WINDOWWIDTH, WINDOWHEIGHT);
            Console.SetBufferSize(WINDOWWIDTH, WINDOWHEIGHT);
            Console.CursorVisible = false;
            Console.Title = "Dijkstra's Algorithm";
            Grid.SetUp();

        }  

        private static void ChangeSize()
        {
            int choice = 0;


            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("    Enter new grid size: ");
            Int32.TryParse(Console.ReadLine(), out choice);
            while (choice*2 < SMALLESTWIDTH || choice*2 > Console.LargestWindowWidth || choice > Console.LargestWindowHeight )
            {
                Console.WriteLine("    Invalid input, try again: ");
                Int32.TryParse(Console.ReadLine(), out choice);
            }
            WINDOWWIDTH = choice*2;
            WINDOWHEIGHT = choice;

            Create();
            Console.Clear();
        }

        private static void ChangeDelay()
        {
            int choice = 0;

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("    Enter new delay: ");
            Int32.TryParse(Console.ReadLine(), out choice);
            while (choice < 0)
            {
                Console.WriteLine("    Invalid input, try again: ");
                Int32.TryParse(Console.ReadLine(), out choice);
            }
            DELAY = choice;
            Console.Clear();
        }

        private static void ChangeRefreshRate()
        {
            int choice = 0;

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("    Enter new refreshrate (recommended to be at least double the grid height): ");
            Int32.TryParse(Console.ReadLine(), out choice);
            while (choice < 0)
            {
                Console.WriteLine("    Invalid input, try again: ");
                Int32.TryParse(Console.ReadLine(), out choice);
            }
            REFRESHRATE = choice;
            Console.Clear();
        }

        public static void Update()
        {
            Grid.ClearDynamicGrid();
        }

    }
}
