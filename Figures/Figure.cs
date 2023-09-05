using chess.Mechanics;
using System;
using System.Collections.Generic;

namespace chess.Movement
{
    public enum Side
    {
        White,
        Black
    }

    public abstract class Figure
    {
        public Desk Desk { get; set; }
        public String ImgCode { get; set; }
        public Side Side { get; set; }
        public Cell PreviousPos { get; set; }
        public Cell CurrentPos { get; set; }
        public int MovesCount { get; set; }

        public abstract IEnumerable<Cell> GetMoves(bool forDefence = false);

        public Figure(Desk desk, String imgCode, Side side, Cell currentPos)
        {
            Desk = desk;
            ImgCode = imgCode;
            Side = side;
            CurrentPos = currentPos;
        }

        protected bool IsOwnFigure(Cell cell)
        {            
            return Desk.GetFigure(cell)?.Side == this.Side;
        }

        protected IEnumerable<Cell> GetMovesForLines(Line[] lines, bool forDefence)
        {
            foreach (var line in lines)
            {
                foreach (var cell in line)
                {
                    if (!Desk.IsCellInside(cell)) break;
                    var cellFig = Desk.GetFigure(cell);
                    if (cellFig != null)
                    {
                        //клетка с чужой фигуркой или своей, но для защиты
                        if (!IsOwnFigure(cell) || forDefence)
                            yield return cell;
                        break;
                    }
                    else
                    {
                        //пустая клетка
                        yield return cell;
                    }                   
                }
            }
        }

        public virtual ConsequenceType MoveConsequences(Cell cell)
        {
            return ConsequenceType.None;
        }

        public Figure Clone(Desk desk)
        {
            var f = this.MemberwiseClone() as Figure;
            f.Desk = desk;
            return f;
        }
    }
}
