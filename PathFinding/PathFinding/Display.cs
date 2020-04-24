using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public const int WINDOWWIDTH = 50;
        public const int WINDOWHEIGHT = 20;
        public const ConsoleColor DEFAULTCOLOUR = ConsoleColor.Black;

        public static class Grid
        {
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

            public static void SetUp()
            {
                _staticGrid = new ConsoleColor[WINDOWHEIGHT - 1, WINDOWWIDTH];
                _dynamicGrid = new ConsoleColor[WINDOWHEIGHT - 1, WINDOWWIDTH];

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
                        Console.Write('█');
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

            public static void Update()
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
                    if (w[0] <= _staticGrid.GetLength(1) && w[1] <= _staticGrid.GetLength(0) && w[0] > 0 && w[1] > 0)
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

            public static List<int[]> GetAvailableCells(int x, int y)
            {

                var available = new List<int[]>();

                if (x <= _staticGrid.GetLength(1) && y <= _staticGrid.GetLength(0) && x > 0 && y > 0)
                {

                    if (x > 0 && _staticGrid[y, x - 1] != (ConsoleColor)Points.wall)
                    {
                        available.Add(new int[] { y, x - 1 });
                    }

                    if (y > 0 && _staticGrid[y - 1, x] != (ConsoleColor)Points.wall)
                    {
                        available.Add(new int[] { y - 1, x });
                    }

                    if (x < _staticGrid.GetLength(1) - 1 && _staticGrid[y, x + 1] != (ConsoleColor)Points.wall)
                    {
                        available.Add(new int[] { y, x + 1 });
                    }

                    if (y < _staticGrid.GetLength(0) - 1 && _staticGrid[y + 1, x] != (ConsoleColor)Points.wall)
                    {
                        available.Add(new int[] { y + 1, x });
                    }
                }

                return available;

            }
        }

        public static void Create()
        {
            Console.SetWindowSize(WINDOWWIDTH, WINDOWHEIGHT);
            Console.SetBufferSize(WINDOWWIDTH, WINDOWHEIGHT);
            Console.CursorVisible = false;
            Console.Title = "Path Finding";
            Grid.SetUp();
        }  

        public static void Update()
        {
            Grid.Update();
        }



    }
}
