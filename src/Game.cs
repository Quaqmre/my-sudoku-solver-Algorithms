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
                dict.AddOrUpdate(item, 1, (key, currenvalue) => ++currenvalue);
            }
            foreach (var item in dict.Keys)
                allNonValues.Add(item);

            return allNonValues;
        }



        public List<int> getNeighborBox(int currentBox)
        {
            List<int> neighborBox = new List<int>();
            int localrow = currentBox;
            int trimedrow = localrow - localrow % 3;

            for (int i = 0; i < 3; i++)
                if (trimedrow + i != localrow)
                {
                    neighborBox.Add(trimedrow + i);
                }
            return neighborBox;
        }
        public List<int> getPossiblyItems(int row, int col, int[,] ar, in string selecter)
        {
            int localrow = row;
            int localcol = col;
            int[,] localarray = ar;
            List<int> constPosItems = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> possİtems = new List<int>();

            if (selecter == "r")
                possİtems = getRowChildItems(localrow, localcol, localarray);
            if (selecter == "c")
                possİtems = getColChildItems(localrow, localcol, localarray);

            List<int> returnedItem = constPosItems.Except(possİtems).ToList();
            return returnedItem;
        }
    }
}
