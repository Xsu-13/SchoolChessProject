using System;

namespace chess.Movement
{
    public class FigFactory
    {
        public static Figure Fabricate(Desk desk, string imgCode, Cell currentPos)
        {
            switch (imgCode)
            {
                case "bp":
                    return new Peshka(desk, imgCode, Side.Black, currentPos);
                case "wp":
                    return new Peshka(desk, imgCode, Side.White, currentPos);
                case "bk":
                    return new King(desk, imgCode, Side.Black, currentPos);
                case "wk":
                    return new King(desk, imgCode, Side.White, currentPos);
                case "bb":
                    return new Slon(desk, imgCode, Side.Black, currentPos);
                case "wb":
                    return new Slon(desk, imgCode, Side.White, currentPos);
                case "bq":
                    return new Queen(desk, imgCode, Side.Black, currentPos);
                case "wq":
                    return new Queen(desk, imgCode, Side.White, currentPos);
                case "bn":
                    return new Konyaka(desk, imgCode, Side.Black, currentPos);
                case "wn":
                    return new Konyaka(desk, imgCode, Side.White, currentPos);
                case "br":
                    return new Ladiya(desk, imgCode, Side.Black, currentPos);
                case "wr":
                    return new Ladiya(desk, imgCode, Side.White, currentPos);
                case "":
                case "-":
                    return null;
                default:
                    throw new NotImplementedException("Такую фигуру еще не закодировали");
            }
        }
    }
}
