using GameBoard;

namespace Chess
{
    public class Knight : Piece
    {
        public Knight(Color color, Board board)
            : base(color, board) { }

        private bool KnightCanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMovesKnight = new bool[Board.Lines, Board.Columns];
            Position p = new Position(0, 0);

            p.DefineNewPosition(Position.Line - 1, Position.Column - 2);
            if (Board.ValidatePosition(p) && KnightCanMove(p))
            {
                possibleMovesKnight[p.Line, p.Column] = true;
            }

            p.DefineNewPosition(Position.Line - 2, Position.Column - 1);
            if (Board.ValidatePosition(p) && KnightCanMove(p))
            {
                possibleMovesKnight[p.Line, p.Column] = true;
            }

            p.DefineNewPosition(Position.Line - 2, Position.Column + 1);
            if (Board.ValidatePosition(p) && KnightCanMove(p))
            {
                possibleMovesKnight[p.Line, p.Column] = true;
            }

            p.DefineNewPosition(Position.Line - 1, Position.Column + 2);
            if (Board.ValidatePosition(p) && KnightCanMove(p))
            {
                possibleMovesKnight[p.Line, p.Column] = true;
            }

            p.DefineNewPosition(Position.Line + 1, Position.Column + 2);
            if (Board.ValidatePosition(p) && KnightCanMove(p))
            {
                possibleMovesKnight[p.Line, p.Column] = true;
            }

            p.DefineNewPosition(Position.Line + 2, Position.Column + 1);
            if (Board.ValidatePosition(p) && KnightCanMove(p))
            {
                possibleMovesKnight[p.Line, p.Column] = true;
            }

            p.DefineNewPosition(Position.Line + 2, Position.Column - 1);
            if (Board.ValidatePosition(p) && KnightCanMove(p))
            {
                possibleMovesKnight[p.Line, p.Column] = true;
            }

            p.DefineNewPosition(Position.Line + 1, Position.Column - 2);
            if (Board.ValidatePosition(p) && KnightCanMove(p))
            {
                possibleMovesKnight[p.Line, p.Column] = true;
            }

            return possibleMovesKnight;
        }

        public override string ToString()
        {
            return "N";
        }
    }
}
