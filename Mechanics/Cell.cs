using System;

namespace chess.Movement
{
    public class Cell
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool Attack { get; private set; }

        public Cell(int x, int y, bool attack = true)
        {
            X = x;
            Y = y;
            Attack = attack;
        }

        public static Cell Parse(string cell)
        {
            return new Cell(cell[0] - 'a', int.Parse(cell[1].ToString()) - 1);
        }

        public int[] ToArray()
        {
            return new int[] { X, Y };
        }

        public override bool Equals(object second)
        {
            return this.X == ((Cell)second).X && this.Y == ((Cell)second).Y;
        }

        public override string ToString()
        {
            return "" + ((char)('a' + X)) + (Y + 1);
        }
    }
}