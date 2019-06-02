using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using src;

namespace new_born_sudoku
{
    class Program
    {
        static int gameLimit = 9;
        static int toplam = 0;
        static void Main(string[] args)
        {
            // Action<int, int, int[,]> d = (r, c, ar) =>
            // {
            //     t = ar[r, c];
            // };
            object t = new object();
            Game game = new Game(gameLimit);
            int[,] array = new int[gameLimit, gameLimit];
            array[0, 0] = 10;
            var childbox = game.getChildBoxItems(3, array);
            System.Console.WriteLine(toplam);
        }
    }
}
