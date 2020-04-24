using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    class Program
    {


        public static Random rnd;

        static void Main(string[] args)
        {
            rnd = new Random();

            Display.Create();
            Display.Grid.StartPoint = new int[] { 2, 13 };
            Display.Grid.EndPoint = new int[] { 15,13 };
            Display.Grid.AddWalls(new int[][] {
                new int[] { 3,0 },
                new int[] { 3,1 },
                new int[] { 3,2 },
                new int[] { 3,3 },
                new int[] { 3,3 },
                new int[] { 3,4 },
                new int[] { 3,5 },
                new int[] { 3,6 },
                new int[] { 3,7 },
                new int[] { 3,8 },
                new int[] { 3,9 },
                new int[] { 3,10 },
                new int[] { 3,11 },
                new int[] { 3,12 },
                new int[] { 3,13 },
                new int[] { 3,14 },
                new int[] { 3,15 },
                new int[] { 7,16 },
                new int[] { 7,17 },
                new int[] { 7,18 },
                new int[] { 7,7 },
                new int[] { 7,8 },
                new int[] { 7,9 },
                new int[] { 7,10 },
                new int[] { 7,11 },
                new int[] { 7,12 },
                new int[] { 7,13 },
                new int[] { 7,14 },
                new int[] { 7,15 },
                new int[] { 12,4 },
                new int[] { 12,5 },
                new int[] { 12,6 },
                new int[] { 12,7 },
                new int[] { 12,8 },
                new int[] { 12,9 },
                new int[] { 12,10 },
                new int[] { 12,11 },
                new int[] { 12,12 },
                new int[] { 12,13 },
                new int[] { 12,14 },
                new int[] { 12,15 }

            });

            Display.Update();
            //Display.Grid.Display();

            Dijkstra dijkstra = new Dijkstra(Display.Grid.StartPoint,Display.Grid.EndPoint,Display.Grid.Width,Display.Grid.Height);

            dijkstra.PathFind();

            while (true)
            {
                Console.ReadKey();
            }

        }

      
    }
}
