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
        public int gameLimitpub { get; set; }
        public Game(int limit)
        {
            gameLimit = limit;
            gameLimit = limit;

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
        public List<int> getAllNonValues(List<int> lrow, List<int> lcol, List<int> lbox)
        {
            List<int> allNonValuesToDict = new List<int>();
            List<int> allNonValues = new List<int>();
            var dict = new ConcurrentDictionary<int, int>();
            if (lrow != null)
                allNonValuesToDict.AddRange(lrow);
            if (lcol != null)
                allNonValuesToDict.AddRange(lcol);
            if (lbox != null)
                allNonValuesToDict.AddRange(lbox);

            foreach (var item in allNonValuesToDict)
            {
                if (item != 0)
                    dict.AddOrUpdate(item, 1, (key, currenvalue) => ++currenvalue);
            }
            foreach (var item in dict.Keys)
                allNonValues.Add(item);

            return allNonValues;
        }
        public int[,] setValues(int[,] y)
        {
            y[0, 0] = 3;
            y[0, 1] = 0;
            y[0, 2] = 0;
            y[0, 3] = 6;
            y[0, 4] = 0;
            y[0, 5] = 8;
            y[0, 6] = 0;
            y[0, 7] = 0;
            y[0, 8] = 1;
            y[1, 0] = 0;
            y[1, 1] = 5;
            y[1, 2] = 0;
            y[1, 3] = 0;
            y[1, 4] = 1;
            y[1, 5] = 0;
            y[1, 6] = 0;
            y[1, 7] = 7;
            y[1, 8] = 0;
            y[2, 0] = 0;
            y[2, 1] = 0;
            y[2, 2] = 2;
            y[2, 3] = 0;
            y[2, 4] = 0;
            y[2, 5] = 0;
            y[2, 6] = 4;
            y[2, 7] = 0;
            y[2, 8] = 0;
            y[3, 0] = 6;
            y[3, 1] = 0;
            y[3, 2] = 0;
            y[3, 3] = 4;
            y[3, 4] = 0;
            y[3, 5] = 7;
            y[3, 6] = 0;
            y[3, 7] = 0;
            y[3, 8] = 5;
            y[4, 0] = 0;
            y[4, 1] = 2;
            y[4, 2] = 0;
            y[4, 3] = 0;
            y[4, 4] = 8;
            y[4, 5] = 0;
            y[4, 6] = 0;
            y[4, 7] = 6;
            y[4, 8] = 0;
            y[5, 0] = 9;
            y[5, 1] = 0;
            y[5, 2] = 0;
            y[5, 3] = 2;
            y[5, 4] = 0;
            y[5, 5] = 6;
            y[5, 6] = 0;
            y[5, 7] = 0;
            y[5, 8] = 4;
            y[6, 0] = 0;
            y[6, 1] = 0;
            y[6, 2] = 9;
            y[6, 3] = 0;
            y[6, 4] = 0;
            y[6, 5] = 0;
            y[6, 6] = 8;
            y[6, 7] = 0;
            y[6, 8] = 0;
            y[7, 0] = 0;
            y[7, 1] = 4;
            y[7, 2] = 0;
            y[7, 3] = 0;
            y[7, 4] = 6;
            y[7, 5] = 0;
            y[7, 6] = 0;
            y[7, 7] = 3;
            y[7, 8] = 0;
            y[8, 0] = 2;
            y[8, 1] = 0;
            y[8, 2] = 0;
            y[8, 3] = 5;
            y[8, 4] = 0;
            y[8, 5] = 1;
            y[8, 6] = 0;
            y[8, 7] = 0;
            y[8, 8] = 9;

            return y;
        }



        public List<int> getNeighborBox(int currentBox, int gamelimit)
        {
            int gamelimitsqr = (int)Math.Sqrt((double)gamelimit);
            List<int> neighborBox = new List<int>();
            int localrow = currentBox;
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

            List<int> returnedItem = constPosItems.Except(possİtems).ToList();
            return returnedItem;
        }
    }
}
