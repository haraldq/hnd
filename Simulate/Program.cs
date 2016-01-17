using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using model;

namespace Simulate
{
    class Program
    {
        private static int maxRow = 200;
        private static int maxCol = 200;

        static void Main(string[] args)
        {
            //Console.SetWindowSize(maxCol + 10, maxRow + 10);
            //Console.SetWindowPosition(0, 0);

            var field = new Cell[maxCol, maxRow];
            Init(field);

            int[,] payoffMatrix = { { -25, 50 }, { 0, 15 } };

            var cellfinder = new RandomCellFinder(field);

            var evolution = new Evolution(field, cellfinder, payoffMatrix);
            for (int i = 0; i < 5000; i++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                //DrawField(field);
                Console.CursorLeft = 0;
                Console.CursorTop = 0;

                PrintInfo(evolution);
                evolution.Tick();
                sw.Stop();
                Console.WriteLine("Ms per tick: " + sw.ElapsedMilliseconds);
                Console.WriteLine("Iteration nr: " + i);
            }
        }

        private static void PrintInfo(Evolution evolution)
        {
            decimal h, d, f, dd;
            evolution.GetInfo(out h, out d, out f, out dd);
            Console.WriteLine($"Hawks:{h:P}");
            Console.WriteLine($"Doves:{d:P}");
            Console.WriteLine($"Average fitness:{f}");
            Console.WriteLine($"Density:{dd:P}");
        }

        private static void DrawField(Cell[,] field)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;

            for (int y = 0; y < maxRow; y++)
            {
                for (int x = 0; x < maxCol; x++)
                {
                    var agent = field[x, y].Agent;
                    Console.Write(agent == null ? ' ' : agent is Hawk ? 'H' : 'D');
                }

                Console.WriteLine();
            }
        }

        private static void Init(Cell[,] field)
        {
            for (int y = 0; y < maxRow; y++)
                for (int x = 0; x < maxCol; x++)
                    field[x, y] = new Cell();


            // Initialize
            int startX = maxCol / 2;
            int startY = maxRow / 2;
            // cross
            //for (int x = 0; x < maxCol; x++)
            //{
            //    field[x, startY] = new Cell(new Hawk());
            //}
            //for (int y = 0; y < maxRow; y++)
            //{
            //    field[startX, y] = new Cell(new Dove());
            //}

            // 2x2 block 
            //field[startX, startY] = new Cell(new Hawk());
            //field[startX + 1, startY] = new Cell(new Dove());
            //field[startX, startY + 1] = new Cell(new Hawk());
            //field[startX + 1, startY + 1] = new Cell(new Dove());

            // Random
            var random = new Random((int)DateTime.Now.Ticks);
            for (int y = 0; y < maxRow; y++)
                for (int x = 0; x < maxCol; x++)
                {
                    var next = random.Next(0, 10);
                    Cell cell = new Cell();
                    if (next == 1) cell = new Cell(new Hawk());
                    else if (next == 2) cell = new Cell(new Dove());
                    field[x, y] = cell;
                }
        }

    }
}
