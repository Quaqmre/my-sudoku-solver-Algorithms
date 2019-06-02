using System;
using Xunit;
using new_born_sudoku;
using src;
using System.Collections.Generic;
using System.Linq;

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
                Assert.Equal(true, item == 0);
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
            colList = gm.getColChildValue(rndRow, rndCol, ar);
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
            colList = gm.getRowChildValue(rndRow, rndCol, ar);
            listLenght = colList.ToArray().Length;
            //Assert
            Assert.Equal(value, listLenght);
        }
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(8)]
        public void getColChildValues_Metot_Should_Return_All_Selected_ColValueList(int col)
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
            var smpleColList = gm.getColChildValue(0, slctCol, ar);
            List<int> tt = colList.Except(smpleColList).ToList();
            //Assert
            Assert.Equal(0, tt.Count());
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(8)]
        public void getRowChildValues_Metot_Should_Return_All_Selected_RowValueList(int row)
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
            var smpleColList = gm.getRowChildValue(slctRow, 0, ar);
            List<int> tt = rowList.Except(smpleColList).ToList();
            //Assert
            Assert.Equal(0, tt.Count());
        }
    }
}
