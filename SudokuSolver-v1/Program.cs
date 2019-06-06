using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Sudoku;

namespace Sudoku
{
    public class atomicBox
    {
        static int gameLenght { get; set; } = 9;
        public readonly int CurrentBoxInfo;
        public int value { get; set; } = 0;
        public readonly int Row;
        public readonly int Col;
        public readonly int childBoxId;
        public List<int> nonNumbers { get; set; }
        public atomicBox(int col, int row)
        {
            Col = col;
            Row = row;
            CurrentBoxInfo = row * gameLenght + col;
            nonNumbers = new List<int>();
            childBoxId = (row - row % 3) + (col - col % 3) / 3;
        }
        public bool addNonNumbers(int x)
        {
            if (!nonNumbers.Contains(x) && x <= gameLenght && x > 0)
            {
                nonNumbers.Add(x);
                return true;
            }
            else
                return false;
        }
        public List<int> getNonNumberList()
        {
            return nonNumbers;
        }
    }
}
public class optionsMethod
{
    static int gameLenght { get; set; } = 9;

    public Random rnd = new Random();
    public atomicBox[,] initilizer()
    {
        atomicBox[,] aa = new atomicBox[gameLenght, gameLenght];
        for (int row = 0; row < gameLenght; row++)
            for (int col = 0; col < gameLenght; col++)
            {
                bool x = rnd.Next(0, 4) == 0 ? true : false;
                aa[row, col] = new atomicBox(col, row) { value = 0 };
            }
        return aa;
    }
    public atomicBox[,] setValues(atomicBox[,] y)
    {
        y[0, 0].value = 1;
        y[0, 1].value = 3;
        y[0, 3].value = 8;
        y[0, 8].value = 7;
        y[1, 0].value = 6;
        y[1, 4].value = 3;
        y[1, 7].value = 1;
        y[1, 8].value = 9;
        y[2, 2].value = 8;
        y[2, 4].value = 1;
        y[2, 5].value = 5;
        y[2, 6].value = 2;
        y[3, 1].value = 8;
        y[3, 4].value = 5;
        y[3, 6].value = 1;
        y[3, 8].value = 6;
        y[4, 1].value = 1;
        y[4, 4].value = 9;
        y[4, 7].value = 8;
        y[5, 0].value = 7;
        y[5, 2].value = 2;
        y[5, 4].value = 8;
        y[5, 5].value = 3;
        y[5, 7].value = 5;
        y[6, 2].value = 1;
        y[6, 3].value = 3;
        y[6, 4].value = 7;
        y[6, 6].value = 6;
        y[7, 0].value = 8;
        y[7, 1].value = 9;
        y[7, 4].value = 4;
        y[7, 8].value = 1;
        y[8, 0].value = 3;
        y[8, 5].value = 1;
        y[8, 7].value = 2;
        y[8, 8].value = 8;

        return y;
    }
    public List<int> getNonNumberInChildRow(in atomicBox a, in atomicBox[,] allboxes)
    {
        List<int> nonNumberListCurrentAtomicBox = a.nonNumbers;
        int Col = a.Col;
        int Row = a.Row;
        for (int col = 0; col < gameLenght; col++)
        {
            var item = allboxes[Row, col].value;
            if (!nonNumberListCurrentAtomicBox.Contains(item) && item > 0 && item < 10)
                nonNumberListCurrentAtomicBox.Add(item);
        }
        return nonNumberListCurrentAtomicBox;
    }
    public List<int> getNonNumberInChildCol(in atomicBox a, in atomicBox[,] allboxes)
    {
        List<int> nonNumberListCurrentAtomicBox = a.nonNumbers;
        int Col = a.Col;
        int Row = a.Row;

        for (int row = 0; row < gameLenght; row++)
        {
            var item = allboxes[row, Col].value;
            if (!nonNumberListCurrentAtomicBox.Contains(item) && item > 0 && item < 10)
                nonNumberListCurrentAtomicBox.Add(item);
        }
        return nonNumberListCurrentAtomicBox;
    }
    public atomicBox getFirstChildBoxItem(in atomicBox a, in atomicBox[,] allboxes)
    {
        var Col = a.Col;
        var Row = a.Row;
        var currentChildBoxId = a.childBoxId;
        int firstChilBoxCol = Col - Col % ((int)Math.Sqrt((double)gameLenght));
        int firstChilBoxRow = Row - (Row % (int)Math.Sqrt((double)gameLenght));
        atomicBox returnModel = allboxes[firstChilBoxRow, firstChilBoxCol];
        return returnModel;
    }

    public List<int> getNonNumberInChildBox(in atomicBox a, in atomicBox[,] allboxes, in atomicBox b)
    {
        var nonNumberListCurrentAtomicBox = b.nonNumbers;
        var firstBoxCol = a.Col;
        var firstBoxRow = a.Row;
        for (int row = firstBoxRow; row < firstBoxRow + Math.Sqrt(gameLenght); row++)
            for (int col = firstBoxCol; col < firstBoxCol + Math.Sqrt(gameLenght); col++)
            {
                var item = allboxes[row, col].value;
                if (item > 0 && item < 10)
                {
                    if (!nonNumberListCurrentAtomicBox.Contains(item))
                        nonNumberListCurrentAtomicBox.Add(item);
                }
            }
        return nonNumberListCurrentAtomicBox;
    }

}
class Program
{
    static void Main()
    {
        optionsMethod op = new optionsMethod();
        var x = op.initilizer();
        var hh = op.setValues(x);
        x = hh;
        var template = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
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
                System.Console.Write(x[i, ii].value + " ");
            }
        }
        bool used = false;
        bool changed = false;

        for (int i = 0; i < 9; i++)
        {
            for (int ii = 0; ii < 9; ii++)
            {
                if (used && !changed)
                {
                    i = 0;
                    ii = 0;
                    changed = true;
                }
                if (x[i, ii].value == 0)
                {
                    // if (i == 7 && ii == 6)
                    // {
                    //     System.Console.WriteLine();
                    //     for (int dd = 0; dd < 9; dd++)
                    //     {
                    //         if (dd % 3 == 0)
                    //             System.Console.WriteLine("\n---------------------");
                    //         else
                    //             System.Console.WriteLine();
                    //         for (int gg = 0; gg < 9; gg++)
                    //         {
                    //             if (gg % 3 == 0)
                    //                 System.Console.Write("| ");
                    //             System.Console.Write(x[dd, gg].value + "  ");
                    //         }
                    //     }
                    //     System.Console.WriteLine();
                    // }
                    var deneme = x;
                    var t = op.getFirstChildBoxItem(x[i, ii], deneme);
                    x[i, ii].nonNumbers = op.getNonNumberInChildBox(t, deneme, x[i, ii]);
                    x[i, ii].nonNumbers = op.getNonNumberInChildRow(x[i, ii], deneme);
                    x[i, ii].nonNumbers = op.getNonNumberInChildCol(x[i, ii], deneme);
                    if (x[i, ii].nonNumbers.Count() == 8)
                    {
                        List<int> tt = template.Except(x[i, ii].nonNumbers).ToList();
                        x[i, ii].value = tt.First();
                        if (!x[i, ii].nonNumbers.Contains(tt.First()))
                            x[i, ii].nonNumbers.Add(tt.First());
                        used = true;
                        changed = false;

                    }
                    else
                    {
                        used = false;
                    }

                }
            }

        }
        System.Console.WriteLine();
        System.Console.WriteLine();
        System.Console.WriteLine();
        for (int i = 0; i < 9; i++)
        {
            if (i % 3 == 0)
                System.Console.WriteLine("\n---------------------");
            else
                System.Console.WriteLine();
            for (int ii = 0; ii < 9; ii++)
            {
                if (ii % 3 == 0)
                    System.Console.Write("| ");
                System.Console.Write(x[i, ii].value + "  ");
            }
        }
        System.Console.WriteLine();
        System.Console.WriteLine();
        System.Console.WriteLine();
        // for (int i = 0; i < 2; i++)
        // {
        //     System.Console.WriteLine(i);
        //     for (int ii = 0; ii < 2; ii++)
        //     {
        //         i = 0;
        //     }
        // }
    }
}
