using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Collections.Concurrent;
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
            List<int> desk = Enumerable.Range(1, gameLimit).ToList();

            game.drowTable(table);
            for (int i = 0; i < gameLimit; i++)
                for (int ii = 0; ii < gameLimit; ii++)
                {
                    List<int> allColPossItems = new List<int>();
                    List<int> allRowPossItems = new List<int>();
                    var oneChangeAtomicBox = new ConcurrentDictionary<int, (int value, int rw, int cl)>();
                    List<int> possibilty = new List<int>();
                    int childboxinf = -1;
                    List<(int rw, int cl)> v = new List<(int rw, int cl)>();
                    bool changed = false;


                    if (table[i, ii] == 0)
                    {
                        if (i == 4 && ii == 3)
                        {

                        }
                        var row1 = game.getRowChildItems(i, ii, table);
                        var col1 = game.getColChildItems(i, ii, table);
                        var boxinfo1 = game.getChildBoxInfo(i, ii);
                        var box1 = game.getChildBoxItems(boxinfo1, table);
                        var nonItems1 = game.getAllUniqValues(row1, col1, box1);
                        if (nonItems1.Count() == 8)
                        {
                            var deneme = desk.Except(nonItems1).ToList();
                            table[i, ii] = deneme.First();
                            changed = true;
                        }
                        else
                        {
                            childboxinf = game.getChildBoxInfo(i, ii);
                            v = game.getChildBoxItemsCordinat(childboxinf, table);
                            foreach (var atomicBox in v)
                            {
                                var row = game.getRowChildItems(atomicBox.rw, atomicBox.cl, table);
                                var col = game.getColChildItems(atomicBox.rw, atomicBox.cl, table);
                                var boxinfo = game.getChildBoxInfo(atomicBox.rw, atomicBox.cl);
                                var box = game.getChildBoxItems(boxinfo, table);
                                var nonItems = game.getAllUniqValues(row, col, box);
                                var rowNeighbors = game.get2NeighborItem(atomicBox.rw, gameLimit);
                                var colNeighbors = game.get2NeighborItem(atomicBox.cl, gameLimit);

                                var firstColPossItem = game.getPossiblyItems(atomicBox.rw, rowNeighbors[0], table, "c");
                                var secondColPossItem = game.getPossiblyItems(atomicBox.rw, rowNeighbors[1], table, "c");
                                allColPossItems.AddRange(firstColPossItem);
                                allColPossItems.AddRange(secondColPossItem);

                                var firstRowPossItem = game.getPossiblyItems(colNeighbors[0], atomicBox.cl, table, "r");
                                var secondRowPossItem = game.getPossiblyItems(colNeighbors[1], atomicBox.cl, table, "r");
                                allRowPossItems.AddRange(firstRowPossItem);
                                allRowPossItems.AddRange(secondRowPossItem);

                                var allPossItem = game.getAllUniqValues(allColPossItems, allRowPossItems);
                                var localPossItems = allPossItem.Except(nonItems).ToList();
                                foreach (var localpossitem in localPossItems)
                                    oneChangeAtomicBox.AddOrUpdate(localpossitem, (1, atomicBox.rw, atomicBox.cl), (key, currenvalue) => (++currenvalue.value, currenvalue.rw, currenvalue.cl));
                                possibilty.AddRange(localPossItems);
                            }
                        }
                        foreach (var item in oneChangeAtomicBox)
                            if (item.Value.value == 1)
                            {
                                table[item.Value.rw, item.Value.cl] = item.Key;
                                changed = true;
                            }

                        if (changed)
                        {
                            i = 0; ii = 0;
                            changed = false;
                        }
                    }
                }
            System.Console.WriteLine();
            System.Console.WriteLine("----------------------");
            game.drowTable(table);







        }
    }
}
