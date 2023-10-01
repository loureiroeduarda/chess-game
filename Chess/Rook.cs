using GameBoard;

namespace Chess
{
    public class Rook : Piece
    {
        public Rook(Color color, Board board) : base(color, board)
        {
        }

        private bool RookCanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMovesRook = new bool[Board.Lines, Board.Columns];
            Position p = new Position(0, 0);

            //above
            p.DefineNewPosition(Position.Line - 1, Position.Column);
            while (Board.ValidatePosition(p) && RookCanMove(p))
            {
                possibleMovesRook[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.Line = p.Line - 1;
            }

            //below
            p.DefineNewPosition(Position.Line + 1, Position.Column);
            while (Board.ValidatePosition(p) && RookCanMove(p))
            {
                possibleMovesRook[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.Line = p.Line + 1;
            }

            //right
            p.DefineNewPosition(Position.Line, Position.Column + 1);
            while (Board.ValidatePosition(p) && RookCanMove(p))
            {
                possibleMovesRook[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.Column = p.Column + 1;
            }

            //left
            p.DefineNewPosition(Position.Line, Position.Column - 1);
            while (Board.ValidatePosition(p) && RookCanMove(p))
            {
                possibleMovesRook[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.Column = p.Column - 1;
            }

            return possibleMovesRook;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}

