using System;

namespace chess.Movement
{
    public class Move
    {
        public Cell From { get; private set; }
        public Cell To { get; private set; }
        public Figure Victim { get; set; }

        public bool Shah { get; set; }
        public bool Mat { get; set; }

        public Move(Cell from, Cell to)
        {
            From = from;
            To = to;
        }

        public static Move Parse(string move)
        {
            var from = Cell.Parse(move.Substring(0, 2));
            var to = Cell.Parse(move.Substring(2));
            return new Move(from, to);
        }

        public override string ToString()
        {
            return From.ToString() + To;
        }
    }
}