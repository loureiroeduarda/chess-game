using GameBoard;

namespace Chess
{
    public class Bishop : Piece
    {
        public Bishop(Color color, Board board)
            : base(color, board) { }

        private bool BishopCanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMovesBishop = new bool[Board.Lines, Board.Columns];
            Position p = new Position(0, 0);

            //north-east
            p.DefineNewPosition(Position.Line - 1, Position.Column + 1);
            while (Board.ValidatePosition(p) && BishopCanMove(p))
            {
                possibleMovesBishop[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.DefineNewPosition(p.Line - 1, p.Column + 1);
            }

            //south-east
            p.DefineNewPosition(Position.Line + 1, Position.Column + 1);
            while (Board.ValidatePosition(p) && BishopCanMove(p))
            {
                possibleMovesBishop[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.DefineNewPosition(p.Line + 1, p.Column + 1);
            }

            //south-west
            p.DefineNewPosition(Position.Line + 1, Position.Column - 1);
            while (Board.ValidatePosition(p) && BishopCanMove(p))
            {
                possibleMovesBishop[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.DefineNewPosition(p.Line + 1, p.Column - 1);
            }

            //north-west
            p.DefineNewPosition(Position.Line - 1, Position.Column - 1);
            while (Board.ValidatePosition(p) && BishopCanMove(p))
            {
                possibleMovesBishop[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.DefineNewPosition(p.Line - 1, p.Column - 1);
            }
            
            return possibleMovesBishop;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
