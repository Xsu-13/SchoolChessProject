using System;
using System.Collections;
using System.Collections.Generic;

namespace chess.Movement
{
    public class Konyaka : Figure
    {
        public Konyaka(Desk desk, String imgCode, Side side, Cell currentPos)
            : base(desk, imgCode, side, currentPos)
        {
        }

        public override IEnumerable<Cell> GetMoves(bool forDefence = false)
        {
            Cell cell;
            for (int i = -2; i <= 2; i++)
                for (int j = -2; j <= 2; j++)
                    if ((Math.Abs(i * j) == 2) 
                      && Desk.IsCellInside(cell = new Cell(CurrentPos.X + i, CurrentPos.Y + j)) 
                      //клетка с чужой фигуркой или своей, но для защиты
                      && (!IsOwnFigure(cell) || forDefence))
                        yield return cell;
        }
    }
}
