using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Sıl
{
    public class Gamev3
    {
        static int[,] ar = { { 9, 2, 0, 0, 0, 3, 0, 0, 1 }// 0
                    , { 1, 0, 0, 0, 0, 0, 0, 0, 0 }// 1
                    , { 7, 0, 0, 0, 0, 0, 0, 0, 0 }// 3
                    , { 5, 0, 0, 0, 0, 0, 0, 0, 0 }// 3
                    , { 0, 0, 0, 5, 0, 0, 0, 0, 0 }// 9
                    , { 0, 0, 0, 0, 9, 0, 0, 0, 0 }// 5
                    , { 0, 0, 0, 0, 0, 0, 0, 0, 0 }// 6
                    , { 0, 0, 0, 0, 0, 0, 5, 0, 0 }// 7
                    , { 0, 0, 0, 9, 0, 0, 0, 0, 0 }// 8
                       };
        // static int0,] ar = { { 0, 0, 0, 0 }// 0
        //                    , { 0, 0, 0, 0 }// 1
        //                    , { 0, 0, 0, 0 }// 2
        //                    , { 0, 0, 0, 0}// 3
        //                };
        static int iterator = 0;
        public (int, int) getNextBox(int[,] array)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (array[i, j] == 0)
                        return (i, j);
            return (-1, -1);
        }
        public bool isVaild(int i, int j, int[,] array, int e)
        {
            for (int row = 0; row < 9; row++)
                if (array[row, j] == e)
                    return false;
            for (int col = 0; col < 9; col++)
                if (array[i, col] == e)
                    return false;
            for (int row = (i - i % 3); row < (i - i % 3) + 3; row++)
                for (int col = (j - j % 3); col < (j - j % 3) + 3; col++)
                    if (array[row, col] == e)
                        return false;
            return true;
        }
        public bool solveSudoku(int[,] array, int row = 0, int col = 0)
        {
            iterator += 1;
            (row, col) = getNextBox(array);
            if (row == -1)
                return true;
            for (int item = 1; item < 10; item++)
                if (isVaild(row, col, array, item))
                {
                    array[row, col] = item;
                    if (solveSudoku(array, row, col))
                    {
                        return true;
                    }
                    else
                        array[row, col] = 0;
                }
            return false;
        }
        public void drowTable(int[,] array)
        {
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
                    System.Console.Write(array[i, ii] + " ");
                }
            }
        }

        static void Main(string[] args)
        {
            Gamev3 prg = new Gamev3();
            prg.drowTable(ar);
            System.Console.WriteLine();
            prg.solveSudoku(ar);
            prg.drowTable(ar);
            System.Console.WriteLine();
            System.Console.WriteLine("İterarions=" + iterator);

        }
    }
}
