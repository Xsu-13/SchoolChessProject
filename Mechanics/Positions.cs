namespace chess
{
    public static class Positions
    {
        public static double[,] PeshkaEvalBlack = {
            { 0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0 },
            { 5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0 },
            { 1.0,  1.0,  2.0,  3.0,  3.0,  2.0,  1.0,  1.0 },
            { 0.5,  0.5,  1.0,  2.5,  2.5,  1.0,  0.5,  0.5 },
            { 0.0,  0.0,  0.0,  2.0,  2.0,  0.0,  0.0,  0.0 },
            { 0.5, -0.5, -1.0,  0.0,  0.0, -1.0, -0.5,  0.5 },
            { 0.5,  1.0,  1.0, -2.0, -2.0,  1.0,  1.0,  0.5 },
            { 0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0 }
        };

        public static double[,] PeshkaEvalWhite = ReverseArray(PeshkaEvalBlack);

        public static double[,] KonyakaEval = {
            { -5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0 },
            { -4.0, -2.0,  0.0,  0.0,  0.0,  0.0, -2.0, -4.0 },
            { -3.0,  0.0,  1.0,  1.5,  1.5,  1.0,  0.0, -3.0 },
            { -3.0,  0.5,  1.5,  2.0,  2.0,  1.5,  0.5, -3.0 },
            { -3.0,  0.0,  1.5,  2.0,  2.0,  1.5,  0.0, -3.0 },
            { -3.0,  0.5,  1.0,  1.5,  1.5,  1.0,  0.5, -3.0 },
            { -4.0, -2.0,  0.0,  0.5,  0.5,  0.0, -2.0, -4.0 },
            { -5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0 }
        };

        public static double[,] SlonEvalBlack = {
            { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 },
            { -1.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -1.0 },
            { -1.0,  0.0,  0.5,  1.0,  1.0,  0.5,  0.0, -1.0 },
            { -1.0,  0.5,  0.5,  1.0,  1.0,  0.5,  0.5, -1.0 },
            { -1.0,  0.0,  1.0,  1.0,  1.0,  1.0,  0.0, -1.0 },
            { -1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0, -1.0 },
            { -1.0,  0.5,  0.0,  0.0,  0.0,  0.0,  0.5, -1.0 },
            { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0 }
        };

        public static double[,] SlonEvalWhite = ReverseArray(SlonEvalBlack);

        public static double[,] LadiyaEvalBlack = {
            {  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0 },
            {  0.5,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  0.5 },
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5 },
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5 },
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5 },
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5 },
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5 },
            {  0.0,  0.0,  0.0,  0.5,  0.5,  0.0,  0.0,  0.0 }
        };

        public static double[,] LadiyaEvalWhite = ReverseArray(LadiyaEvalBlack);

        public static double[,] EvalQueen = {
            { -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0 },
            { -1.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -1.0 },
            { -1.0,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -1.0 },
            { -0.5,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -0.5 },
            {  0.0,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -0.5 },
            { -1.0,  0.5,  0.5,  0.5,  0.5,  0.5,  0.0, -1.0 },
            { -1.0,  0.0,  0.5,  0.0,  0.0,  0.0,  0.0, -1.0 },
            { -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0 }
        };

        public static double[,] KingEvalBlack = {
            { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
            { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
            { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
            { -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0 },
            { -2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0 },
            { -1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0 },
            {  2.0,  2.0,  0.0,  0.0,  0.0,  0.0,  2.0,  2.0 },
            {  2.0,  3.0,  1.0,  0.0,  0.0,  1.0,  3.0,  2.0 }
        };

        public static double[,] KingEvalWhite = ReverseArray(KingEvalBlack);

        private static double[,] ReverseArray(double[,] array)
        {
            var copy = array.Clone() as double[,];
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    var tmp = copy[y, x];
                    copy[y, x] = copy[8 - y - 1, x];
                    copy[8 - y - 1, x] = tmp;
                }
            }
            return copy;
        }

        static Positions()
        {
            PeshkaEvalBlack = Helpers.Transpose(PeshkaEvalBlack);
            PeshkaEvalWhite = Helpers.Transpose(PeshkaEvalWhite);
            KonyakaEval = Helpers.Transpose(KonyakaEval);
            SlonEvalBlack = Helpers.Transpose(SlonEvalBlack);
            SlonEvalWhite = Helpers.Transpose(SlonEvalWhite);
            LadiyaEvalBlack = Helpers.Transpose(LadiyaEvalBlack);
            LadiyaEvalWhite = Helpers.Transpose(LadiyaEvalWhite);
            EvalQueen = Helpers.Transpose(EvalQueen);
            KingEvalBlack = Helpers.Transpose(KingEvalBlack);
            KingEvalWhite = Helpers.Transpose(KingEvalWhite);
        }
    }
}
