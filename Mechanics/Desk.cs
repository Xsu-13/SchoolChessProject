using chess.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    public class Desk
    {
        //синглтон
        private static Desk _instance;
        public static Desk Instance => _instance ?? (_instance = new Desk());

        //поля доски
        public Side CurrentSide = Side.White;
        public String MovesReadableString = "";

        //матрица фигур
        public Figure[,] Matrix { get; set; }

        public Desk()
        {
            InitInitialPosition();
        }

        private void InitInitialPosition()
        {
            FillMatrix(Helpers.Transpose(new string[,] {
            {"wr", "wn", "wb", "wq", "wk", "wb", "wn", "wr" },
            {"wp", "wp", "wp", "wp", "wp", "wp", "wp", "wp" },
            {"", "", "", "", "", "", "", "" },
            {"", "", "", "", "", "", "", "" },
            {"", "", "", "", "", "", "", "" },
            {"", "", "", "", "", "", "", "" },
            {"bp", "bp", "bp", "bp", "bp", "bp", "bp", "bp" },
            {"br", "bn", "bb", "bq", "bk", "bb", "bn", "br"}
            }));

            CurrentSide = Side.White;
            MovesReadableString = "";
        }

        private void FillMatrix(string[,] layout)
        {
            Matrix = new Figure[8, 8];
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var typ = layout[x, y];
                    Matrix[x, y] = FigFactory.Fabricate(this, typ, new Cell(x, y));
                }
            }
        }

        public Figure GetFigure(Cell cell)
        {
            return (Figure)Matrix.GetValue(cell.ToArray());
        }

        public void Move(Move move)
        {
            var fromCoord = move.From.ToArray();
            var toCoord = move.To.ToArray();
            move.Victim = (Figure)Matrix.GetValue(toCoord);
            var f = (Figure)Matrix.GetValue(fromCoord);
            f.PreviousPos = move.From;
            f.CurrentPos = move.To;
            f.MovesCount++;
            Matrix.SetValue(null, fromCoord);
            Matrix.SetValue(f, toCoord);
        }

        public void ChangeType(Cell cell, string type)
        {
            var coord = cell.ToArray();
            Matrix.SetValue(FigFactory.Fabricate(this, type, cell), coord);
        }

        public void Reset()
        {
            InitInitialPosition();
        }

        public void TestLayout()
        {
            Reset();

            FillMatrix(Helpers.Transpose(new string[,] {
            { "wr", "",   "",   "wq", "",   "wb", "wn", "wr" },
            { "wp", "wp", "wp", "wp", "",   "wp", "wp", "" },
            { "",   "",   "",   "",   "wp", "",   "",   "" },
            { "",   "",   "",   "",   "",   "",   "bb", "" },
            { "bb", "",   "br", "",   "",   "",   "",   "" },
            { "",   "",   "",   "",   "",   "wk", "",   "" },
            { "",   "",   "",   "",   "",   "",   "",   "" },
            { "br", "",   "",   "wr", "bk", "bb", "bn", "" },
            }));

            CurrentSide = Side.Black;
        }

        public bool IsCellInside(Cell cell)
        {
            if (cell.X < 0) return false;
            if (cell.Y < 0) return false;
            if (cell.X > 7) return false;
            if (cell.Y > 7) return false;
            return true;
        }

        public bool IsEmptyCell(Cell cell)
        {
            return GetFigure(cell) == null;
        }

        public Side GetOppositeSide(Side side)
        {
            if (side == Side.White)
                return Side.Black;
            else
                return Side.White;
        }

        public King FindKing(Side forSide)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var f = Matrix[x, y];
                    if (f?.Side == forSide && f is King king)
                        return king;
                }
            }
            throw new Exception("Куда делся король?");
        }

        public void ChangeSide()
        {
            CurrentSide = GetOppositeSide(CurrentSide);
        }

        public IEnumerable<Cell> GetAttackedCellsBy(Side bySide)
        {
            if (bySide == CurrentSide)
                yield break;

            //насобираем фигуры
            var figures = GetFiguresBySide(bySide);

            //обход фигур
            foreach (var f in figures)
                foreach (var cell in f.GetMoves(true).Where(x => x.Attack))
                    yield return cell;
        }

        public List<Figure> GetFiguresBySide(Side bySide)
        {
            var figures = new List<Figure>();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 7; y >= 0; y--)
                {
                    var f = Matrix[x, y];
                    if (f?.Side == bySide)
                        figures.Add(f);
                }
            }

            return figures;
        }

        public void CheckShahmat(Figure f, Move move)
        {
            var oppKing = FindKing(GetOppositeSide(f.Side));
            if (IsKingAttacked(GetOppositeSide(f.Side)))
            {
                move.Shah = true;

                //или же мат? если все фигуры не могут ходить
                var figures = GetFiguresBySide(oppKing.Side);
                if (figures.Sum(x => GetAllowedMoves(x).Count()) == 0)
                    move.Mat = true;
            }
        }

        public Desk Clone()
        {
            Figure f;
            var clonedDesk = this.MemberwiseClone() as Desk;
            clonedDesk.Matrix = this.Matrix.Clone() as Figure[,];
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    if ((f = clonedDesk.Matrix[x, y]) != null)
                        clonedDesk.Matrix[x, y] = f.Clone(clonedDesk);
            return clonedDesk;
        }

        public bool IsKingAttacked(Side side)
        {
            var king = FindKing(side);
            var attackedCells = GetAttackedCellsBy(GetOppositeSide(side)).Distinct().ToList();
            return attackedCells.Contains(king.CurrentPos);
        }

        public IEnumerable<Cell> GetAllowedMoves(Figure f)
        {
            var moves = f.GetMoves().ToList();

            //если король под шахом, то нужно разрешить только ходы спасающие от шаха
            //или разрешить еще только ходы которые не приводят к шаху
            for (int i = moves.Count - 1; i >= 0; i--)
            {
                //проверяем каждый ход на клонированной доске
                var newDesk = this.Clone();
                newDesk.Move(new Move(f.CurrentPos, moves[i]));
                //спасает ли от шаха, если король все еще атакован,
                if (newDesk.IsKingAttacked(f.Side))
                    //то ход убираем
                    moves.RemoveAt(i);
            }

            return moves;
        }

        public async Task<Move> BotMove()
        {
            await Task.Delay(200);
            var move = FindBotMove();
            if (move == null) return null;
            Desk.Instance.Move(move);
            Desk.Instance.MovesReadableString += move + " ";
            var figObj = Desk.Instance.GetFigure(move.To);
            figObj.MoveConsequences(move.To);
            Desk.Instance.ChangeSide();
            Desk.Instance.CheckShahmat(figObj, move);
            return move;
        }

        private Move FindBotMove()
        {
            //старый алгоритм случайного выбора хода
            //Move[] allMoves = GetAllAllowedMoves(Side.Black);
            //if (allMoves.Length == 0) return null;
            //var moveIdx = new Random().Next(0, allMoves.Length);
            //return allMoves[moveIdx];

            //новый алгоритм выбора хода с оценкой 
            return Bot.MinimaxRoot(Desk.Instance); 
        }
    }
}
