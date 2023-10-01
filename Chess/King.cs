using GameBoard;

namespace Chess
{
    public class King : Piece
    {
        private ChessMatch Match;

        public King(Color color, Board board, ChessMatch match)
            : base(color, board)
        {
            Match = match;
        }

        private bool KingCanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        private bool RookTestForRock(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null
                && piece is Rook
                && piece.Color == Color
                && piece.QuantityMoves == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMovesKing = new bool[Board.Lines, Board.Columns];
            Position p = new Position(0, 0);

            //above
            p.DefineNewPosition(Position.Line - 1, Position.Column);
            if (Board.ValidatePosition(p) && KingCanMove(p))
            {
                possibleMovesKing[p.Line, p.Column] = true;
            }

            //north-east
            p.DefineNewPosition(Position.Line - 1, Position.Column + 1);
            if (Board.ValidatePosition(p) && KingCanMove(p))
            {
                possibleMovesKing[p.Line, p.Column] = true;
            }

            //right
            p.DefineNewPosition(Position.Line, Position.Column + 1);
            if (Board.ValidatePosition(p) && KingCanMove(p))
            {
                possibleMovesKing[p.Line, p.Column] = true;
            }

            //south-east
            p.DefineNewPosition(Position.Line + 1, Position.Column + 1);
            if (Board.ValidatePosition(p) && KingCanMove(p))
            {
                possibleMovesKing[p.Line, p.Column] = true;
            }

            //below
            p.DefineNewPosition(Position.Line + 1, Position.Column);
            if (Board.ValidatePosition(p) && KingCanMove(p))
            {
                possibleMovesKing[p.Line, p.Column] = true;
            }

            //south-west
            p.DefineNewPosition(Position.Line + 1, Position.Column - 1);
            if (Board.ValidatePosition(p) && KingCanMove(p))
            {
                possibleMovesKing[p.Line, p.Column] = true;
            }

            //left
            p.DefineNewPosition(Position.Line, Position.Column - 1);
            if (Board.ValidatePosition(p) && KingCanMove(p))
            {
                possibleMovesKing[p.Line, p.Column] = true;
            }

            //north-west
            p.DefineNewPosition(Position.Line - 1, Position.Column - 1);
            if (Board.ValidatePosition(p) && KingCanMove(p))
            {
                possibleMovesKing[p.Line, p.Column] = true;
            }

            //special move: castle
            if (QuantityMoves == 0 && !Match.Check)
            {
                //special move: small castle
                Position pRook1 = new Position(Position.Line, Position.Column + 3);
                if (RookTestForRock(pRook1))
                {
                    Position pKing1 = new Position(Position.Line, Position.Column + 1);
                    Position pKing2 = new Position(Position.Line, Position.Column + 2);
                    if (Board.Piece(pKing1) == null && Board.Piece(pKing2) == null)
                    {
                        possibleMovesKing[Position.Line, Position.Column + 2] = true;
                    }
                }

                //special move: big castle
                Position pRook2 = new Position(Position.Line, Position.Column - 4);
                if (RookTestForRock(pRook2))
                {
                    Position pKing1 = new Position(Position.Line, Position.Column - 1);
                    Position pKing2 = new Position(Position.Line, Position.Column - 2);
                    Position pKing3 = new Position(Position.Line, Position.Column - 3);
                    if (
                        Board.Piece(pKing1) == null
                        && Board.Piece(pKing2) == null
                        && Board.Piece(pKing3) == null
                    )
                    {
                        possibleMovesKing[Position.Line, Position.Column - 2] = true;
                    }
                }
            }

            return possibleMovesKing;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
