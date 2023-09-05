using chess.Movement;
using System;
using System.Linq;

namespace chess
{
    public static class Bot
    {
        public static int PostitionsChecked { get; set; } = 0;

        private static double GetFigureValue(Figure f, int x, int y)
        {
            if (f == null)
                return 0;

            double getAbsoluteValue(Figure f, bool isWhite, int x, int y)
            {
                if (f is Peshka)
                    return 10 + (isWhite ? Positions.PeshkaEvalWhite[x, y] : Positions.PeshkaEvalBlack[x, y]);
                if (f is Ladiya)
                    return 50 + (isWhite ? Positions.LadiyaEvalWhite[x, y] : Positions.LadiyaEvalBlack[x, y]);
                if (f is Konyaka)
                    return 30 + Positions.KonyakaEval[x, y];
                if (f is Slon)
                    return 30 + (isWhite ? Positions.SlonEvalWhite[x, y] : Positions.SlonEvalBlack[x, y]);
                if (f is Queen)
                    return 90 + Positions.EvalQueen[x, y];
                if (f is King)
                    return 900 + (isWhite ? Positions.KingEvalWhite[x, y] : Positions.KingEvalBlack[x, y]);
                throw new NotImplementedException("Unknown piece type: " + f.GetType());
            };

            var absoluteValue = getAbsoluteValue(f, f.Side == Side.White, x, y);
            return f.Side == Side.White ? absoluteValue : -absoluteValue;
        }

        private static double EvaluateBoard(Desk desk)
        {
            var totalEvaluation = 0d;
            for (var x = 0; x < 8; x++)
                for (var y = 0; y < 8; y++)
                    totalEvaluation += GetFigureValue(desk.GetFigure(new Cell(x, y)), x, y);
            return totalEvaluation;
        }

        private static Move[] GetAllAllowedMoves(Desk desk)
        {
            var figures = desk.GetFiguresBySide(desk.CurrentSide);
            return figures
                .SelectMany(x => desk.GetAllowedMoves(x).Select(y => new Move(x.CurrentPos, y)))
                .ToArray();
        }

        private static double Minimax(int depth, Desk desk, bool isMaximisingPlayer)
        {
            PostitionsChecked++;

            if (depth == 0)
                //оценка выполняется в "листьях" дерева
                return -EvaluateBoard(desk);

            var newGameMoves = GetAllAllowedMoves(desk);
            if (isMaximisingPlayer)
            {
                var bestMove = double.MinValue;
                for (var i = 0; i < newGameMoves.Length; i++)
                {
                    //клон доски
                    var newDesk = desk.Clone();

                    //гипотетический ход
                    newDesk.Move(newGameMoves[i]);
                    var figObj = newDesk.GetFigure(newGameMoves[i].To);
                    figObj.MoveConsequences(newGameMoves[i].To);
                    newDesk.ChangeSide();

                    //рекурсивный просмотр вглубь
                    bestMove = Math.Max(bestMove, Minimax(depth - 1, newDesk, !isMaximisingPlayer));
                }
                return bestMove;
            }
            else
            {
                var bestMove = double.MaxValue;
                for (var i = 0; i < newGameMoves.Length; i++)
                {
                    //клон доски
                    var newDesk = desk.Clone();

                    //гипотетический ход
                    newDesk.Move(newGameMoves[i]);
                    var figObj = newDesk.GetFigure(newGameMoves[i].To);
                    figObj.MoveConsequences(newGameMoves[i].To);
                    newDesk.ChangeSide();

                    //рекурсивный просмотр вглубь
                    bestMove = Math.Min(bestMove, Minimax(depth - 1, newDesk, !isMaximisingPlayer));
                }
                return bestMove;
            }
        }

        public static Move MinimaxRoot(Desk desk)
        {
            var newGameMoves = GetAllAllowedMoves(desk);
            var bestMove = double.MinValue;
            Move bestMoveFound = null;
            PostitionsChecked = 0;

            for (var i = 0; i < newGameMoves.Length; i++)
            {
                var newDesk = desk.Clone();
                newDesk.Move(newGameMoves[i]);
                var figObj = newDesk.GetFigure(newGameMoves[i].To);
                figObj.MoveConsequences(newGameMoves[i].To);
                newDesk.ChangeSide();

                var value = Minimax(2, newDesk, false);

                if (value >= bestMove)
                {
                    bestMove = value;
                    bestMoveFound = newGameMoves[i];
                }
            }
            return bestMoveFound;
        }
    }
}
