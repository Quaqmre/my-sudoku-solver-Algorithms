using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
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
            var item = ar;
            Action<int, int, int[,]> localact = (rw, cl, tab) =>
            {
                tab[rw, cl] = 0;
            };
            looper(gameLimit, gameLimit, item, localact);
            return item;
        }

        public List<int> getRowChildValue(int row, int col, in int[,] ar)
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
        public List<int> getColChildValue(int row, int col, in int[,] ar)
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
    }
}