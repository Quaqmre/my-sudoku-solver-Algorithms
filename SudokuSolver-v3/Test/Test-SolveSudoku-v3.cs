using System;
using Sıl;
using Xunit;

namespace Test
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(2, 3)]
        [InlineData(5, 6)]
        [InlineData(3, 3)]
        [InlineData(0, 8)]
        public void getNextBox_Metot_Should_Return_Same_Input_Locatation(int locRow, int locCol)
        {
            //Arrange
            Gamev3 gm = new Gamev3();
            int[,] ar = new int[9, 9];
            for (int i = 0; i < 9; i++)
                for (int ii = 0; ii < 9; ii++)
                    ar[i, ii] = i * 9 + ii + 1;
            ar[locRow, locCol] = 0;
            //Act
            (int x, int y) = gm.getNextBox(ar);
            var sum = x + y;
            //Assert
            Assert.Equal(locRow + locCol, sum);
        }
        [Theory]
        [InlineData(2, 3, 10, true)]
        [InlineData(5, 6, 8, false)]
        [InlineData(3, 3, 3, false)]
        [InlineData(0, 8, 11, true)]
        public void isVaild_Metot_If_Uniq_Input_Return_True(int locRow, int locCol, int e, bool expected)
        {
            //Arrange
            Gamev3 gm = new Gamev3();
            int[,] ar = new int[9, 9];
            for (int i = 0; i < 9; i++)
                for (int ii = 0; ii < 9; ii++)
                    ar[i, ii] = ii;
            //Act
            bool checker = gm.isVaild(locRow, locCol, ar, e);
            //Assert
            Assert.Equal(expected, checker);
        }
        [Fact]
        public void solveSudoku_Metot_If_Uniq_Input_Return_True()
        {
            int[,] ar = { { 9, 2, 0, 0, 0, 3, 0, 0, 1 }// 0
                        , { 1, 0, 0, 0, 0, 0, 0, 0, 0 }// 1
                        , { 7, 0, 0, 0, 0, 0, 0, 0, 0 }// 3
                        , { 5, 0, 0, 0, 0, 0, 0, 0, 0 }// 3
                        , { 0, 0, 0, 5, 0, 0, 0, 0, 0 }// 9
                        , { 0, 0, 0, 0, 9, 0, 0, 0, 0 }// 5
                        , { 0, 0, 0, 0, 0, 0, 0, 0, 0 }// 6
                        , { 0, 0, 0, 0, 0, 0, 5, 0, 0 }// 7
                        , { 0, 0, 0, 9, 0, 0, 0, 0, 0 }// 8
                        };
            //Arrange
            Gamev3 gm = new Gamev3();
            bool isSolved = gm.solveSudoku(ar);
            //Act
            //Assert
            Assert.Equal(true, isSolved);
        }
    }
}
