using System;
using Xunit;
using new_born_sudoku;
using src;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;

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
                    ar[i, ii] = i * gameLimit + ii;
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
                    ar[i, ii] = i * gameLimit + ii;
            //Act
            colList = gm.getRowChildItems(rndRow, rndCol, ar);
            listLenght = colList.ToArray().Length;
            //Assert
            Assert.Equal(value, listLenght);
        }
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(8)]
        public void getColChildItems_Metot_Should_Return_All_Selected_ColValueList(int col)
        {
            //Arrange
            List<int> colList = new List<int>();
            Random rnd = new Random();
            int gameLimit = 9;
            int slctCol = col;
            Game gm = new Game(gameLimit);
            int[,] ar = new int[gameLimit, gameLimit];
            for (int rw = 0; rw < gameLimit; rw++)
                for (int cl = 0; cl < gameLimit; cl++)
                    ar[rw, cl] = rw * gameLimit + cl;
            for (int rw = 0; rw < 9; rw++)
            {
                int item = rw * 9 + slctCol;
                colList.Add(item);
            }
            //Act
            var smpleColList = gm.getColChildItems(0, slctCol, ar);
            List<int> tt = colList.Except(smpleColList).ToList();
            //Assert
            Assert.Equal(0, tt.Count());
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(8)]
        public void getRowChildItems_Metot_Should_Return_All_Selected_RowValueList(int row)
        {
            //Arrange
            List<int> rowList = new List<int>();
            Random rnd = new Random();
            int gameLimit = 9;
            int slctRow = row;
            Game gm = new Game(gameLimit);
            int[,] ar = new int[gameLimit, gameLimit];
            for (int i = 0; i < gameLimit; i++)
                for (int ii = 0; ii < gameLimit; ii++)
                    ar[i, ii] = i * gameLimit + ii;
            for (int ii = 0; ii < 9; ii++)
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
        [InlineData(0, 0, 0)]
        [InlineData(2, 6, 2)]
        [InlineData(5, 3, 4)]
        [InlineData(8, 8, 8)]
        public void getChildBoxInfo_Metot_Should_Return_Selected_ChildBoxInfo(int row, int col, int expected)
        {
            //Arrange
            int getBoxInfo = -1;
            int gameLimit = 9;
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
        public void getChildBoxItems_Metot_Should_Equal_Selected_ChildBoxItems(int boxInfo, int gameLimit)
        {
            //Arrange
            int expected = gameLimit;
            Game gm = new Game(gameLimit);
            int[,] ar = new int[gameLimit, gameLimit];
            for (int i = 0; i < gameLimit; i++)
                for (int ii = 0; ii < gameLimit; ii++)
                    ar[i, ii] = i * gameLimit + ii;
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
            List<int> boxİtemList = new List<int>() { 0, 1, 2, 9, 10, 11, 18, 19, 20 };
            Game gm = new Game(gameLimit);
            int[,] ar = new int[gameLimit, gameLimit];
            for (int i = 0; i < gameLimit; i++)
                for (int ii = 0; ii < gameLimit; ii++)
                    ar[i, ii] = i * gameLimit + ii;
            //Act
            var getBoxItems = gm.getChildBoxItems(boxInfo, ar);
            List<int> tt = getBoxItems.Except(boxİtemList).ToList();
            //Assert
            Assert.Equal(0, tt.Count());
        }
        [Theory]
        [InlineData(new int[] { 1, 10, 3, 4 }, new int[] { 3, 1, 7, 3 }, new int[] { 5 })]
        public void getAllNonValues_Metot_Should_Return_Uniq_Value_inLists(int[] list1, int[] list2, int[] list3)
        {
            //Arrange
            List<int> addedList = new List<int>() { 1, 3, 4, 5, 7, 10 };
            Game gm = new Game(9);
            //Act
            List<int> uniqItemsofList = gm.getAllNonValues(list1.ToList(), list2.ToList(), list3.ToList());
            var zero = addedList.Except(uniqItemsofList).OrderBy(x => x);
            //Asser
            Assert.Equal(addedList.OrderBy(x => x), uniqItemsofList);
        }
        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4 }, new int[] { 5, 6, 7, 8 }, new int[] { 9, 10, 10 }, 10)]
        [InlineData(new int[] { 5, 1, 9, 3 }, new int[] { 7, 0, 2, 2 }, new int[] { 5, 4, 3 }, 8)]
        [InlineData(new int[] { 1, 2, 3, 1 }, new int[] { 1, 3, 2, 1 }, new int[] { 1, 1, 2, 2, 3, 3 }, 3)]
        [InlineData(new int[] { }, new int[] { }, new int[] { }, 0)]
        public void getAllNonValues_Metot_Should_Return_Expected_Lenght(int[] list1, int[] list2, int[] list3, int expectedLenght)
        {
            //Arrange
            Game gm = new Game(9);
            int lenght = 0;
            //Act
            List<int> uniqItemsofList = gm.getAllNonValues(list1.ToList(), list2.ToList(), list3.ToList());
            lenght = uniqItemsofList.Count();
            //Asser
            Assert.Equal(expectedLenght, lenght);
        }
        [Theory]
        [InlineData(0, 9)]
        [InlineData(8, 9)]
        [InlineData(3, 3)]
        public void getNeighborRowBox_Metot_Should_Return_Two_NeighborRow(int row, int gameLimit)
        {
            //Arrange
            Game gm = new Game(gameLimit);
            int expectedLenght = 2;
            //Act
            List<int> neigborRow = gm.getNeighborBox(row);
            //Assert
            Assert.Equal(expectedLenght, neigborRow.Count());
        }
        [Theory]
        [InlineData(0, new int[] { 1, 2 })]
        [InlineData(8, new int[] { 6, 7 })]
        [InlineData(4, new int[] { 3, 5 })]
        public void getNeighborRowBox_Metot_Should_Return_List_NeighborRow(int row, int[] expectedList)
        {
            //Arrange
            Game gm = new Game(9);
            //Act
            List<int> neigborRow = gm.getNeighborBox(row);
            //Assert
            Assert.Equal(expectedList.ToList(), neigborRow);
        }
        [Theory]
        [InlineData(0, new int[] { 1, 2 })]
        [InlineData(5, new int[] { 3, 4 })]
        [InlineData(8, new int[] { 6, 7 })]
        public void getNeighborRowBox_Metot_Should_Return_Expected_List(int row, int[] expectedList)
        {
            //Arrange
            Game gm = new Game(9);
            //Act
            List<int> neigborRow = gm.getNeighborBox(row);
            var shouldZero = expectedList.ToList().Except(neigborRow);
            //Assert
            Assert.Equal(expectedList.ToList(), neigborRow);
        }
    }
}
