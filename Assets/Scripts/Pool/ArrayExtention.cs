public static class ArrayExtention
{
    public static T[,] ArrayTo2DArray<T>(this T[] input, int height, int width)
    {
        T[,] output = new T[height, width];

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                output[i, j] = input[i * width + j];

        return output;
    }
}
