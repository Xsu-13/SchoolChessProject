using chess.Mechanics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace chess.Movement
{
    public class Peshka : Figure
    {
        public Peshka(Desk desk, String imgCode, Side side, Cell currentPos)
            : base(desk, imgCode, side, currentPos)
        {
        }

        public override IEnumerable<Cell> GetMoves(bool forDefence = false)
        {
            Cell cell;

            //клетки для прохода
            if (Side == Side.White)
            {
                if (Desk.IsCellInside(cell = new Cell(CurrentPos.X, CurrentPos.Y + 1, false)) && Desk.IsEmptyCell(cell))
                    yield return cell;

                if (CurrentPos.Y == 1)
                    //стартовая позиция - прыг через одну
                    if (Desk.IsEmptyCell(cell = new Cell(CurrentPos.X, CurrentPos.Y + 2, false)) &&
                        Desk.IsEmptyCell(new Cell(CurrentPos.X, CurrentPos.Y + 1)))
                        yield return cell;
            }
            else
            {
                if (Desk.IsCellInside(cell = new Cell(CurrentPos.X, CurrentPos.Y - 1, false)) && Desk.IsEmptyCell(cell))
                    yield return cell;

                if (CurrentPos.Y == 6)
                    //стартовая позиция - прыг через одну
                    if (Desk.IsEmptyCell(cell = new Cell(CurrentPos.X, CurrentPos.Y - 2, false)) &&
                        Desk.IsEmptyCell(new Cell(CurrentPos.X, CurrentPos.Y - 1)))
                        yield return cell;
            }

            //клетки для взятия
            var killCells = Side == Side.White
                ? new Cell[] { new Cell(CurrentPos.X - 1, CurrentPos.Y + 1), new Cell(CurrentPos.X + 1, CurrentPos.Y + 1) }
                : new Cell[] { new Cell(CurrentPos.X - 1, CurrentPos.Y - 1), new Cell(CurrentPos.X + 1, CurrentPos.Y - 1) };

            foreach (var killCell in killCells)
            {
                if (!Desk.IsCellInside(killCell)) continue;
                if (Desk.GetFigure(killCell) == null && !forDefence) continue;
                //клетка с чужой фигуркой или своей, но для защиты
                if (!IsOwnFigure(killCell) || forDefence)
                    yield return killCell;
            }
        }

        public override ConsequenceType MoveConsequences(Cell cell)
        {
            if (Side == Side.White)
            {
                if (cell.Y == 7)
                {
                    Desk.ChangeType(cell, "wq");
                    return ConsequenceType.QueenGrows;
                }
            }
            else
            {
                if (cell.Y == 0)
                {
                    Desk.ChangeType(cell, "bq");
                    return ConsequenceType.QueenGrows;
                }
            }

            return ConsequenceType.None;
        }
    }
}
