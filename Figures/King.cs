using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using chess.Mechanics;

namespace chess.Movement
{
    public class King : Figure
    {
        public King(Desk desk, String imgCode, Side side, Cell currentPos)
            : base(desk, imgCode, side, currentPos)
        {
        }

        public override IEnumerable<Cell> GetMoves(bool forDefence = false)
        {
            var attackedCells = Desk.GetAttackedCellsBy(Desk.GetOppositeSide(this.Side)).Distinct().ToList();

            Cell cell;
            //простые ходы вокруг
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    if ((i != 0 || j != 0) 
                        && Desk.IsCellInside(cell = new Cell(CurrentPos.X + i, CurrentPos.Y + j))
                        //клетка с чужой фигуркой или своей, но для защиты
                        && (!IsOwnFigure(cell) || forDefence)
                        && !attackedCells.Contains(cell))
                        yield return cell;

            //путь рокировки налево свободен
            if (new List<int> { 1, 2, 3 }.TrueForAll(x =>
                {
                    var c = new Cell(x, CurrentPos.Y);
                    return Desk.IsEmptyCell(c) && !attackedCells.Contains(c);
                })
                //в углу стоит именно ладья
                && Desk.GetFigure(new Cell(0, CurrentPos.Y)) is Ladiya ladya1
                //не ходили и не под обстрелом
                && ladya1.MovesCount == 0 && this.MovesCount == 0)
                yield return new Cell(CurrentPos.X - 2, CurrentPos.Y);

            //путь рокировки направо свободен
            if (new List<int> { 5, 6 }.TrueForAll(x =>
                {
                    var c = new Cell(x, CurrentPos.Y);
                    return Desk.IsEmptyCell(c) && !attackedCells.Contains(c);
                })
                //в углу стоит именно ладья
                && Desk.GetFigure(new Cell(7, CurrentPos.Y)) is Ladiya ladya2
                //не ходили и не под обстрелом
                && ladya2.MovesCount == 0 && this.MovesCount == 0)
                yield return new Cell(CurrentPos.X + 2, CurrentPos.Y);
        }

        public override ConsequenceType MoveConsequences(Cell cell)
        {
            //рокировка двигает и ладью
            if (PreviousPos.X - CurrentPos.X == 2)
                Desk.Move(new Move(new Cell(0, cell.Y), new Cell(cell.X + 1, cell.Y)));

            if (PreviousPos.X - CurrentPos.X == -2)
                Desk.Move(new Move(new Cell(7, cell.Y), new Cell(cell.X - 1, cell.Y)));

            return ConsequenceType.None;
        }
    }
}
