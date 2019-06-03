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
        static void Main(string[] args)
        {
            Game game = new Game(gameLimit);
            int[,] gameTable = new int[gameLimit, gameLimit];
            var t = game.initilizer(gameTable);
            var table = game.setValues(gameTable);
            bool changed = false;
            List<int> desk = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> desk1 = new List<int>() { 0 };
            List<int> row = new List<int>();
            List<int> col = new List<int>();
            List<int> box = new List<int>();
            List<int> nonItems = new List<int>();
            int boxinfo = 0;

            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0)
                    System.Console.WriteLine("\n---------------------");
                else
                    System.Console.WriteLine();
                for (int ii = 0; ii < 9; ii++)
                {
                    if (ii % 3 == 0)
                        System.Console.Write("|");
                    System.Console.Write(table[i, ii] + " ");
                }
            }
            for (int i = 0; i < gameLimit; i++)
                for (int ii = 0; ii < gameLimit; ii++)
                {
                    if (table[i, ii] == 0)
                    {
                        if (i == 4 && ii == 3)
                        {

                        }
                        if (changed)
                        {
                            i = 0; ii = 0;
                            changed = false;
                        }
                        row = game.getRowChildItems(i, ii, table);
                        col = game.getColChildItems(i, ii, table);
                        boxinfo = game.getChildBoxInfo(i, ii);
                        box = game.getChildBoxItems(boxinfo, table);
                        nonItems = game.getAllNonValues(row, col, box);
                        List<int> nonZeroItems = nonItems.Except(desk1).ToList();

                        if (nonZeroItems.Count() == 8)
                        {
                            table[i, ii] = (int)desk.Except(nonZeroItems).ToList().First();
                            changed = true;
                        }
                    }
                }
            System.Console.WriteLine("bitti");
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0)
                    System.Console.WriteLine("\n---------------------");
                else
                    System.Console.WriteLine();
                for (int ii = 0; ii < 9; ii++)
                {
                    if (ii % 3 == 0)
                        System.Console.Write("|");
                    System.Console.Write(table[i, ii] + " ");
                }
            }



        }
    }
}
