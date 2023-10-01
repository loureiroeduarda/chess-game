using GameBoard;

namespace Chess
{
    public class Queen : Piece
    {
        public Queen(Color color, Board board)
            : base(color, board) { }

        private bool QueenCanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMovesQueen = new bool[Board.Lines, Board.Columns];
            Position p = new Position(0, 0);

            //above
            p.DefineNewPosition(Position.Line - 1, Position.Column);
            while (Board.ValidatePosition(p) && QueenCanMove(p))
            {
                possibleMovesQueen[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.DefineNewPosition(p.Line - 1, p.Column);
            }

            //north-east
            p.DefineNewPosition(Position.Line - 1, Position.Column + 1);
            while (Board.ValidatePosition(p) && QueenCanMove(p))
            {
                possibleMovesQueen[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.DefineNewPosition(p.Line - 1, p.Column + 1);
            }

            //right
            p.DefineNewPosition(Position.Line, Position.Column + 1);
            while (Board.ValidatePosition(p) && QueenCanMove(p))
            {
                possibleMovesQueen[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.DefineNewPosition(p.Line, p.Column + 1);
            }

            //south-east
            p.DefineNewPosition(Position.Line + 1, Position.Column + 1);
            while (Board.ValidatePosition(p) && QueenCanMove(p))
            {
                possibleMovesQueen[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.DefineNewPosition(p.Line + 1, p.Column + 1);
            }

            //below
            p.DefineNewPosition(Position.Line + 1, Position.Column);
            while (Board.ValidatePosition(p) && QueenCanMove(p))
            {
                possibleMovesQueen[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.DefineNewPosition(p.Line + 1, p.Column);
            }

            //south-west
            p.DefineNewPosition(Position.Line + 1, Position.Column - 1);
            while (Board.ValidatePosition(p) && QueenCanMove(p))
            {
                possibleMovesQueen[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.DefineNewPosition(p.Line + 1, p.Column - 1);
            }

            //left
            p.DefineNewPosition(Position.Line, Position.Column - 1);
            while (Board.ValidatePosition(p) && QueenCanMove(p))
            {
                possibleMovesQueen[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.DefineNewPosition(p.Line, p.Column - 1);
            }

            //north-west
            p.DefineNewPosition(Position.Line - 1, Position.Column - 1);
            while (Board.ValidatePosition(p) && QueenCanMove(p))
            {
                possibleMovesQueen[p.Line, p.Column] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.DefineNewPosition(p.Line - 1, p.Column - 1);
            }

            return possibleMovesQueen;
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}
