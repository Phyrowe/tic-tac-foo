using System;

namespace TicTacFoo.Application.Common.Extensions
{
    public static class ArrayExtensions
    {
        public static  T[,] ToMultiDimensional<T>(this T[] flatArray, int width)
        {
            int height = (int)Math.Ceiling(flatArray.Length / (double)width);
            T[,] result = new T[height, width];
            int rowIndex, colIndex;

            for (int index = 0; index < flatArray.Length; index++)
            {
                rowIndex = index / width;
                colIndex = index % width;
                result[rowIndex, colIndex] = flatArray[index];
            }
            return result;
        }
    }
}