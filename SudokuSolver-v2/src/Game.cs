using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Collections.Concurrent;

namespace src
{
    public class Game
    {
        public int[,] gameTable { get; set; }
        static int gameLimit;
        static int gameLimitsqr;
        public int gameLimitpub { get; set; }
        public Game(int limit)
        {
            gameLimit = limit;
            gameLimitsqr = (int)Math.Sqrt((double)limit);

        }
        public int[,] setValues(int[,] y)
        {
            //             0  1  2  3  4  5  6  7  8 
            int[,] s = { { 0, 0, 0, 0, 0, 0, 6, 8, 0 }// 0
                       , { 0, 0, 0, 0, 7, 3, 0, 0, 9 }// 1
                       , { 3, 0, 9, 0, 0, 0, 0, 4, 5 }// 2
                       , { 4, 9, 0, 0, 0, 0, 0, 0, 0 }// 3
                       , { 8, 0, 3, 0, 5, 0, 9, 0, 2 }// 4
                       , { 0, 0, 0, 0, 0, 0, 0, 3, 6 }// 5
                       , { 9, 6, 0, 0, 0, 0, 3, 0, 8 }// 6
                       , { 7, 0, 0, 6, 8, 0, 0, 0, 0 }// 7
                       , { 0, 2, 8, 0, 0, 0, 0, 0, 0 }// 8
                       };
            return s;
        }
        public void looper(int row, int col, int[,] array, Action<int, int, int[,]> act, int startrow = 0, int startcol = 0)
        {
            for (int i = startrow; i < row; i++)
                for (int ii = startcol; ii < col; ii++)
                {
                    act(i, ii, array);
                }
        }
        public int[,] initilizer(in int[,] ar)
        {
            var localarray = ar;
            Action<int, int, int[,]> localact = (rw, cl, tab) =>
            {
                tab[rw, cl] = 0;
            };
            looper(gameLimit, gameLimit, localarray, localact);
            return localarray;
        }
        public List<int> getRowChildItems(int row, int col, in int[,] ar)
        {
            List<int> rowValueList = new List<int>();
            var localrow = row;
            var localcol = gameLimit;
            var localarray = ar;
            Action<int, int, int[,]> localact = (r, c, gt) =>
            {
                if (gt[r, c] != 0)
                    rowValueList.Add(gt[r, c]);
            };
            looper(localrow + 1, localcol, localarray, localact, localrow);
            return rowValueList;
        }
        public List<int> getColChildItems(int row, int col, in int[,] ar)
        {
            List<int> colValueList = new List<int>();
            var localrow = gameLimit;
            var localcol = col;
            var localarray = ar;
            Action<int, int, int[,]> localact = (r, c, gt) =>
            {
                if (gt[r, c] != 0)
                    colValueList.Add(gt[r, c]);
            };
            looper(localrow, localcol + 1, localarray, localact, 0, localcol);
            return colValueList;
        }
        public int getChildBoxInfo(in int row, in int col)
        {
            int localrow = row;
            int localcol = col;
            int childBoxInfo = -1;
            int gameLimitsqr = (int)Math.Sqrt((double)gameLimit);

            childBoxInfo = (localrow - localrow % gameLimitsqr)
                         + (localcol - localcol % gameLimitsqr) / gameLimitsqr;

            return childBoxInfo;
        }
        public List<int> getChildBoxItems(in int boxInfo, in int[,] ar)
        {
            int gameLimitsqr = (int)Math.Sqrt((double)gameLimit);
            List<int> boxValueList = new List<int>();
            int[,] localarray = ar;
            int startcol = boxInfo % gameLimitsqr * gameLimitsqr;
            int startrow = boxInfo - boxInfo % gameLimitsqr;

            Action<int, int, int[,]> localact = (r, c, gt) =>
           {
               if (gt[r, c] != 0)
                   boxValueList.Add(gt[r, c]);
           };
            looper(startrow + gameLimitsqr, startcol + gameLimitsqr,
                    localarray, localact, startrow, startcol);
            return boxValueList;
        }
        public List<(int rw, int cl)> getChildBoxItemsCordinat(in int boxInfo, in int[,] ar)
        {
            int gameLimitsqr = (int)Math.Sqrt((double)gameLimit);
            List<(int, int)> boxValueCordinateList = new List<(int, int)>();
            int[,] localarray = ar;
            int startcol = boxInfo % gameLimitsqr * gameLimitsqr;
            int startrow = boxInfo - boxInfo % gameLimitsqr;

            Action<int, int, int[,]> localact = (r, c, gt) =>
           {
               if (gt[r, c] == 0)
                   boxValueCordinateList.Add((r, c));
           };
            looper(startrow + gameLimitsqr, startcol + gameLimitsqr,
                    localarray, localact, startrow, startcol);
            return boxValueCordinateList;
        }

        public List<int> getAllUniqValues(List<int> lr1 = null, List<int> lc2 = null, List<int> lb3 = null)
        {
            List<int> AllUniqValuesToDict = new List<int>();
            List<int> AllUniqValues = new List<int>();
            var dict = new ConcurrentDictionary<int, int>();
            if (lr1 != null)
                AllUniqValuesToDict.AddRange(lr1);
            if (lc2 != null)
                AllUniqValuesToDict.AddRange(lc2);
            if (lb3 != null)
                AllUniqValuesToDict.AddRange(lb3);

            foreach (var item in AllUniqValuesToDict)
            {
                if (item != 0)
                    dict.AddOrUpdate(item, 1, (key, currenvalue) => ++currenvalue);
            }
            foreach (var item in dict.Keys)
                AllUniqValues.Add(item);

            return AllUniqValues;
        }
        public List<int> get2NeighborItem(int currentId, int gamelimit)
        {
            int gamelimitsqr = (int)Math.Sqrt((double)gamelimit);
            List<int> neighborBox = new List<int>();
            int localrow = currentId;
            int trimedRow = localrow - localrow % gamelimitsqr;

            for (int i = 0; i < gamelimitsqr; i++)
                if (trimedRow + i != localrow)
                {
                    neighborBox.Add(trimedRow + i);
                }
            return neighborBox;
        }
        public List<int> getPossiblyItems(int row, int col, int[,] ar, in string selector)
        {
            int localrow = row;
            int localcol = col;
            int[,] localarray = ar;
            List<int> constPosItems = Enumerable.Range(1, gameLimit).ToList();
            List<int> possİtems = new List<int>();
            if (selector == "r")
                possİtems = getRowChildItems(localrow, localcol, localarray);
            if (selector == "c")
                possİtems = getColChildItems(localrow, localcol, localarray);
            return possİtems;
        }
        public int getRankingInChildBox(in int row, in int col)
        {
            int rank = (row % gameLimitsqr) * gameLimitsqr + col % gameLimitsqr;
            return rank;
        }
        public void drowTable(in int[,] ar)
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
                    System.Console.Write(ar[i, ii] + " ");
                }
            }
        }
    }
}
