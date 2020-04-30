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

            Display.MainMenu.Run();
        }


        public static void Run()
        {
            Display.Update();

            Dijkstra dijkstra = new Dijkstra(Display.Grid.StartPoint, Display.Grid.EndPoint, Display.Grid.Width, Display.Grid.Height);

            dijkstra.PathFind();

            while (true)
            {
                if(Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }

      
    }
}
