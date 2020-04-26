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
                Console.WriteLine();
            }
        }

    }
}
