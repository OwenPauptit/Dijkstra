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

            Display.Grid.SetUp();
            Display.Grid.StartPoint = new int[] { 5, 5 };
            Display.Grid.EndPoint = new int[] { 15, 15 };
            Display.Grid.AddWalls(new int[][] {
                new int[] { 7, 7 },
                new int[] { 7, 8 },
                new int[] { 7, 9 },
                new int[] { 7, 10 } 
            });

            Display.Update();
            Display.Grid.Display();



            while (true)
            {
                Console.ReadKey();
            }

        }

      
    }
}
