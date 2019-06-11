using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using src;

namespace Test
{
    public class UnitTest1
    {

        //InıtilizerTest
        [Fact]
        public void Should_ArrayItem_Equel_Zero()
        {
            //Arrange
            int toplam = 0;
            int gameLimit = 2;
            Game gm = new Game(gameLimit);
            int[,] ar = new int[gameLimit, gameLimit];
            //Act
            var x = gm.initilizer(ar);
            foreach (var item in x)
            {
                toplam += item;
            }
            //Assert
            Assert.Equal(0, toplam);
        }
        //InıtilizerTest
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(9)]
        [InlineData(16)]
        public void Shouldnt_each_ArrayItem_Equel_Zero(int value)
        {
            //Arrange
            int gameLimit = value;
            Game gm = new Game(gameLimit);
            int[,] ar = new int[gameLimit, gameLimit];
            //Act
            var x = gm.initilizer(ar);
            //Assert
            foreach (var item in x)
                Assert.True(item == 0);
        }
        //looperTest
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(9)]
        [InlineData(16)]
        public void looper_Metot_Should_Return_All_Sum(int value)
        {
            //Arrange
            int sum = 0;
            int doubleValue = value * value;
            List<int> SumList = new List<int>();
            int gameLimit = value;
            Game gm = new Game(gameLimit);
            int[,] ar = new int[gameLimit, gameLimit];
            for (int i = 0; i < gameLimit; i++)
                for (int ii = 0; ii < gameLimit; ii++)
                    ar[i, ii] = i * gameLimit + ii;
            Action<int, int, int[,]> localact = (q, w, actar) =>
            {
                SumList.Add(actar[q, w]);
            };
            //Act
            gm.looper(gameLimit, gameLimit, ar, localact);
            //Assert
            foreach (var i in SumList)
                sum += i;
            Assert.Equal(doubleValue * (doubleValue - 1) / 2, sum);
        }
        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(9)]
        [InlineData(16)]
        public void getColChildValue_Metot_Should_Return_All_CollompLenght(int value)
        {
            //Arrange
            int listLenght = 0;
            List<int> colList = new List<int>();
            Random rnd = new Random();
            var rndRow = rnd.Next(0, value);
            var rndCol = rnd.Next(0, value);
            int gameLimit = value;
            Game gm = new Game(gameLimit);
            int[,] ar = new int[gameLimit, gameLimit];
            for (int i = 0; i < gameLimit; i++)
                for (int ii = 0; ii < gameLimit; ii++)
                    ar[i, ii] = i * gameLimit + ii + 1;
            //Act
            colList = gm.getColChildItems(rndRow, rndCol, ar);
            listLenght = colList.ToArray().Length;
            //Assert
            Assert.Equal(value, listLenght);
        }
        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(9)]
        [InlineData(16)]
        public void getCRowChildValue_Metot_Should_Return_All_RowLenght(int value)
        {
            //Arrange
            int listLenght = 0;
            List<int> colList = new List<int>();
            Random rnd = new Random();
            var rndRow = rnd.Next(0, value);
            var rndCol = rnd.Next(0, value);
            int gameLimit = value;
            Game gm = new Game(gameLimit);
            int[,] ar = new int[gameLimit, gameLimit];
            for (int i = 0; i < gameLimit; i++)
                for (int ii = 0; ii < gameLimit; ii++)
                    ar[i, ii] = i * gameLimit + ii + 1;
            //Act
            colList = gm.getRowChildItems(rndRow, rndCol, ar);
            listLenght = colList.ToArray().Length;
            //Assert
            Assert.Equal(value, listLenght);
        }
        [Theory]
        [InlineData(2, 9)]
        [InlineData(3, 9)]
        [InlineData(8, 9)]
        [InlineData(12, 16)]
        [InlineData(15, 16)]
        public void getColChildItems_Metot_Should_Return_All_Selected_ColValueList(int col, int gameLimit)
        {
            //Arrange
            List<int> colList = new List<int>();
            Random rnd = new Random();
            int slctCol = col;
            Game gm = new Game(gameLimit);
            int[,] ar = new int[gameLimit, gameLimit];
            for (int rw = 0; rw < gameLimit; rw++)
                for (int cl = 0; cl < gameLimit; cl++)
                    ar[rw, cl] = rw * gameLimit + cl;
            for (int rw = 0; rw < gameLimit; rw++)
            {
                int item = rw * gameLimit + slctCol;
                colList.Add(item);
            }
            //Act
            var smpleColList = gm.getColChildItems(0, slctCol, ar);
            List<int> tt = colList.Except(smpleColList).ToList();
            //Assert
            Assert.Equal(0, tt.Count());
        }

        [Theory]
        [InlineData(2, 9)]
        [InlineData(3, 9)]
        [InlineData(8, 9)]
        [InlineData(12, 16)]
        public void getRowChildItems_Metot_Should_Return_All_Selected_RowValueList(int row, int gameLimit)
        {
            //Arrange
            List<int> rowList = new List<int>();
            Random rnd = new Random();
            int slctRow = row;
            Game gm = new Game(gameLimit);
            int[,] ar = new int[gameLimit, gameLimit];
            for (int i = 0; i < gameLimit; i++)
                for (int ii = 0; ii < gameLimit; ii++)
                    ar[i, ii] = i * gameLimit + ii;
            for (int ii = 0; ii < gameLimit; ii++)
            {
                int item = gameLimit * slctRow + ii;
                rowList.Add(item);
            }
            //Act
            var smpleColList = gm.getRowChildItems(slctRow, 0, ar);
            List<int> tt = rowList.Except(smpleColList).ToList();
            //Assert
            Assert.Equal(0, tt.Count());
        }
        [Theory]
        [InlineData(0, 0, 9, 0)]
        [InlineData(2, 6, 9, 2)]
        [InlineData(5, 3, 9, 4)]
        [InlineData(8, 8, 9, 8)]
        [InlineData(10, 10, 16, 10)]
        [InlineData(11, 11, 16, 10)]
        [InlineData(15, 15, 16, 15)]
        public void getChildBoxInfo_Metot_Should_Return_Selected_ChildBoxInfo(int row, int col, int gameLimit, int expected)
        {
            //Arrange
            int getBoxInfo = -1;
            Game gm = new Game(gameLimit);
            //Act
            getBoxInfo = gm.getChildBoxInfo(row, col);
            //Assert
            Assert.Equal(expected, getBoxInfo);
        }
        [Theory]
        [InlineData(0, 9)]
        [InlineData(3, 9)]
        [InlineData(6, 9)]
        [InlineData(8, 9)]
        [InlineData(12, 16)]
        [InlineData(15, 16)]
        public void getChildBoxItems_Metot_Should_Equal_Selected_ChildBoxItems(int boxInfo, int gameLimit)
        {
            //Arrange
            int expected = gameLimit;
            Game gm = new Game(gameLimit);
            int[,] ar = new int[gameLimit, gameLimit];
            for (int i = 0; i < gameLimit; i++)
                for (int ii = 0; ii < gameLimit; ii++)
                    ar[i, ii] = i * gameLimit + ii + 1;
            //Act
            var getBoxItems = gm.getChildBoxItems(boxInfo, ar);
            //Assert
            Assert.Equal(expected, getBoxItems.Count());
        }
        [Theory]
        [InlineData(0, 9)]
        public void getChildBoxItems_Metot_Should_Return_Selected_ChildBoxItems(int boxInfo, int gameLimit)
        {
            //Arrange
            List<int> boxİtemList = new List<int>() { 1, 2, 3, 10, 11, 12, 19, 20, 21 };
            Game gm = new Game(gameLimit);
            int[,] ar = new int[gameLimit, gameLimit];
            for (int i = 0; i < gameLimit; i++)
                for (int ii = 0; ii < gameLimit; ii++)
                    ar[i, ii] = i * gameLimit + ii + 1;
            //Act
            var getBoxItems = gm.getChildBoxItems(boxInfo, ar);
            List<int> tt = getBoxItems.Except(boxİtemList).ToList();
            //Assert
            Assert.Equal(0, tt.Count());
        }
        [Theory]
        [InlineData(new int[] { 1, 10, 3, 4 }, new int[] { 3, 1, 7, 3 }, new int[] { 5 })]
        public void getAllUniqValues_Metot_Should_Return_Uniq_Value_inLists(int[] list1, int[] list2, int[] list3)
        {
            //Arrange
            List<int> addedList = new List<int>() { 1, 3, 4, 5, 7, 10 };
            Game gm = new Game(9);
            //Act
            List<int> uniqItemsofList = gm.getAllUniqValues(list1.ToList(), list2.ToList(), list3.ToList());
            var zero = addedList.Except(uniqItemsofList).OrderBy(x => x);
            //Asser
            Assert.Equal(addedList.OrderBy(x => x), uniqItemsofList);
        }
        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4 }, new int[] { 5, 6, 7, 8 }, new int[] { 9, 10, 10 }, 10)]
        [InlineData(new int[] { 5, 1, 9, 3 }, new int[] { 7, 0, 2, 2 }, new int[] { 5, 4, 3 }, 7)]
        [InlineData(new int[] { 1, 2, 3, 1 }, new int[] { 1, 3, 2, 1 }, new int[] { 1, 1, 2, 2, 3, 3 }, 3)]
        [InlineData(new int[] { }, new int[] { }, new int[] { }, 0)]
        public void getAllUniqValues_Metot_Should_Return_Expected_Lenght(int[] list1, int[] list2, int[] list3, int expectedLenght)
        {
            //Arrange
            Game gm = new Game(9);
            int lenght = 0;
            //Act
            List<int> uniqItemsofList = gm.getAllUniqValues(list1.ToList(), list2.ToList(), list3.ToList());
            lenght = uniqItemsofList.Count();
            //Asser
            Assert.Equal(expectedLenght, lenght);
        }
        [Theory]
        [InlineData(0, 9)]
        [InlineData(8, 9)]
        [InlineData(3, 16)]
        public void get2NeighborItem_Metot_Should_Return_Two_NeighborRow(int row, int gameLimit)
        {
            //Arrange
            Game gm = new Game(gameLimit);
            int expectedLenght = (int)Math.Sqrt((double)gameLimit) - 1;
            //Act
            List<int> neigborRow = gm.get2NeighborItem(row, gameLimit);
            //Assert
            Assert.Equal(expectedLenght, neigborRow.Count());
        }
        [Theory]
        [InlineData(0, new int[] { 1 }, 4)]
        [InlineData(0, new int[] { 1, 2 }, 9)]
        [InlineData(8, new int[] { 6, 7 }, 9)]
        [InlineData(4, new int[] { 3, 5 }, 9)]
        [InlineData(4, new int[] { 5, 6, 7 }, 16)]
        [InlineData(14, new int[] { 12, 13, 15 }, 16)]
        public void get2NeighborItem_Metot_Should_Return_List_NeighborRow(int row, int[] expectedList, int gameLimit)
        {
            //Arrange
            Game gm = new Game(gameLimit);
            //Act
            List<int> neigborRow = gm.get2NeighborItem(row, gameLimit);
            //Assert
            Assert.Equal(expectedList.ToList(), neigborRow);
        }
        [Theory]
        [InlineData(0, 4, new int[] { 1 })]
        [InlineData(2, 4, new int[] { 3 })]
        [InlineData(0, 9, new int[] { 1, 2 })]
        [InlineData(5, 9, new int[] { 3, 4 })]
        [InlineData(8, 9, new int[] { 6, 7 })]
        [InlineData(14, 16, new int[] { 12, 13, 15 })]
        [InlineData(10, 16, new int[] { 8, 9, 11 })]
        public void get2NeighborItem_Metot_Should_Return_Expected_List(int row, int gameLimit, int[] expectedList)
        {
            //Arrange
            Game gm = new Game(gameLimit);
            //Act
            List<int> neigborRow = gm.get2NeighborItem(row, gameLimit);
            List<int> shouldZero = expectedList.Except(neigborRow).ToList();
            //Assert
            Assert.Equal(0, shouldZero.Count());
        }

        [Theory]
        [InlineData(0, 9)]
        [InlineData(3, 9)]
        [InlineData(6, 9)]
        [InlineData(12, 16)]
        public void getChildBoxItemsCordinat_Metot_Should_Return_FirstBoxItem_ColompZero(int boxInfo, int gameLimit)
        {
            //Arrange
            Game gm = new Game(gameLimit);
            int gameLimitsqr = (int)Math.Sqrt((double)gameLimit);

            //Act
            int[,] ar = new int[gameLimit, gameLimit];
            for (int i = 0; i < gameLimit; i++)
                for (int ii = 0; ii < gameLimit; ii++)
                    ar[i, ii] = i % gameLimitsqr + ii;
            List<(int r, int c)> neigborRow = gm.getChildBoxItemsCordinat(boxInfo, ar);
            //Assert
            Assert.Equal(0, neigborRow[0].c);
        }
        [Theory]
        [InlineData(0, 3, 9, 0)]
        [InlineData(7, 5, 9, 5)]
        [InlineData(4, 5, 9, 5)]
        public void getRankingInChildBox_Metot_Should_Return_InRankChildBox(int row, int col, int gameLimit, int expected)
        {
            //Arrange
            Game gm = new Game(gameLimit);
            int rank;
            //Act
            rank = gm.getRankingInChildBox(row, col);
            //Assert
            Assert.Equal(expected, rank);

        }
    }
}
