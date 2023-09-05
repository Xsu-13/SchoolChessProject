namespace chess
{
    public static class Helpers
    {
        public static T[,] Transpose<T>(T[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);
            T[,] result = new T[h, w];
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    result[j, i] = matrix[i, j];
            return result;
        }   
    }
}
