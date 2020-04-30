using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    static class Reader
    {

        public static void ReadFromTextFile(string fileName)
        {
            string[] contents = System.IO.File.ReadAllLines(fileName);

            int[,] importGrid = new int[contents.Length, contents[0].Length];

            for (int i = 0; i < contents.Length; ++i)
            {
                for (int k = 0; k < contents[i].Length; ++k)
                {
                    switch(contents[i][k])
                    {
                        case 'X':
                            Display.Grid.AddWalls(new int[][] { new int[] { k,i } });
                            break;
                        case 'S':
                            Display.Grid.StartPoint = new int[] { k,i };
                            break;
                        case 'F':
                            Display.Grid.EndPoint = new int[] { k,i };
                            break;
                        
                    }
                }
            }
        }

        public static void ReadInCoords()
        {
            int[] temp = new int[2];

            Console.WriteLine();
            Console.WriteLine("Startpoint ");
            Display.Grid.StartPoint = GetCoords();

            Console.WriteLine();
            Console.WriteLine("Endpoint ");
            Display.Grid.EndPoint = GetCoords();

            Console.WriteLine();
            Console.WriteLine("Walls - enter -1 to stop");

            while (true)
            {
                temp = GetCoords(true);

                if (temp[0] == -1 || temp[1] == -1)
                {
                    break;
                }

                Display.Grid.AddWalls(new int[][] { temp });
            }

        }

        private static int[] GetCoords(bool wall=false)
        {
            int[] coords = new int[2];
            Console.WriteLine("    Enter x-coord: ");
            while (!Int32.TryParse(Console.ReadLine(), out coords[0]) || coords[0] < (wall? -1: 1) || coords[0] >= Display.Grid.Width) { Console.WriteLine("Invalid Input, try again"); }
            Console.WriteLine("    Enter y-coord: ");
            while (!Int32.TryParse(Console.ReadLine(), out coords[1]) || coords[1] < (wall ? -1 : 1) || coords[1] >= Display.Grid.Height) { Console.WriteLine("Invalid Input, try again"); }
            return coords;
        }
    }
}
