using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    //https://en.wikipedia.org/wiki/Pathfinding
    class Dijkstra
    {

        private List<int[]> _elements;
        private int[,] _grid;
        private int[] _endPoint;

        public bool Complete { get; private set; }

        public Dijkstra(int[] start, int[] end, int width, int height)
        {
            _elements = new List<int[]>();
            _elements.Add(new int[] { start[0], start[1], 0 });

            _grid = new int[width, height];
            _endPoint = end.Clone() as int[];

            Complete = false;

        }

        private void SelectPath()
        {
            int[] currentCell = Display.Grid.EndPoint.Clone() as int[];
            int lowest;
            int[] lowestCell = new int[3];
            List<int[]> cells;
            do
            {
                cells = Display.Grid.GetAvailableCells(currentCell[0], currentCell[1]);
                if (cells.Count > 0)
                {
                    lowest = 999;
                    for (int i = 0; i < cells.Count(); ++i)
                    {
                        if (_grid[cells[i][1], cells[i][0]] < lowest && _grid[cells[i][1], cells[i][0]] != 0)
                        {
                            lowest = _grid[cells[i][1], cells[i][0]];
                            lowestCell = cells[i];
                        }
                    }
                    Display.Grid.AddDynamicPoint(lowestCell[0], lowestCell[1], ConsoleColor.Cyan);
                    currentCell = lowestCell.Clone() as int[];
                }
                else
                {
                    break;
                }
            } while (lowest > 1);
        }

        public void PathFind()
        {
            List<int[]> cells;
            int k = 0;
            while (k < _elements.Count())
            {
                cells = Display.Grid.GetAvailableCells(_elements[k][0], _elements[k][1]);

                

                foreach(var c in cells)
                {
                    c[2] += _elements[k][2];

                    if (_grid[c[1],c[0]] == 0)
                    {

                        _grid[c[1], c[0]] = c[2];
                        _elements.Add(c);
                        if (Display.Grid.IsStaticPoint(c[0], c[1], Display.Points.end))
                        {
                            Complete = true;
                        }

                    }
                    else if (c[2] >= _grid[c[1], c[0]])
                    {

                        continue;
                    }

                    else
                    {
                        _grid[c[1], c[0]] = c[2];
                        
                        for (int i = 0; i < _elements.Count(); ++i)
                        {
                            if (_elements[i][0] == c[0] && _elements[i][1] == c[1])
                            {
                                _elements[i] = c.Clone() as int[];

                                if (Display.Grid.IsStaticPoint(c[0], c[1], Display.Points.end))
                                {
                                    Complete = true;
                                }

                                break;
                            }
                        }

                    }


       
                    Display.Grid.AddDynamicPoint(c[0], c[1], ConsoleColor.Blue);

                    if(Complete)
                    {
                        Display.Grid.Display();
                        SelectPath();
                        Display.Grid.Display();
                        return;
                    }
                    
                }
                if (k % 20 == 0)
                {
                    Display.Grid.Display();
                    System.Threading.Thread.Sleep(30);
                }

                ++k;
            }

        }

    }
}
