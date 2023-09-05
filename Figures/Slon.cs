using System;
using System.Collections;
using System.Collections.Generic;

namespace chess.Movement
{
    public class Slon : Figure
    {
        public Slon(Desk desk, String imgCode, Side side, Cell currentPos)
            : base(desk, imgCode, side, currentPos)
        {
        }

        public override IEnumerable<Cell> GetMoves(bool forDefence = false)
        {
            var lines = new Line[4];
            for (int i = 0; i < lines.Length; i++) lines[i] = new Line();

            for (int i = 1; i <= 8; i++)
                lines[0].Add(new Cell(CurrentPos.X + i, CurrentPos.Y + i));

            for (int i = 1; i <= 8; i++)
                lines[1].Add(new Cell(CurrentPos.X + i, CurrentPos.Y - i));

            for (int i = 1; i <= 8; i++)
                lines[2].Add(new Cell(CurrentPos.X - i, CurrentPos.Y - i));

            for (int i = 1; i <= 8; i++)
                lines[3].Add(new Cell(CurrentPos.X - i, CurrentPos.Y + i));

            return GetMovesForLines(lines, forDefence);
        }
    }
}
