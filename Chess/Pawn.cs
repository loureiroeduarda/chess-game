using GameBoard;

namespace Chess
{
    public class Pawn : Piece
    {
        private ChessMatch Match;

        public Pawn(Color color, Board board, ChessMatch match)
            : base(color, board)
        {
            Match = match;
        }

        private bool ThereIsAnEnemy(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }

        private bool Free(Position position)
        {
            return Board.Piece(position) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMovesPawn = new bool[Board.Lines, Board.Columns];
            Position p = new Position(0, 0);

            if (Color == Color.White)
            {
                p.DefineNewPosition(Position.Line - 1, Position.Column);
                if (Board.ValidatePosition(p) && Free(p))
                {
                    possibleMovesPawn[p.Line, p.Column] = true;
                }

                p.DefineNewPosition(Position.Line - 2, Position.Column);
                Position pAux = new Position(Position.Line - 1, Position.Column);
                if (
                    Board.ValidatePosition(pAux)
                    && Free(pAux)
                    && Board.ValidatePosition(p)
                    && Free(p)
                    && QuantityMoves == 0
                )
                {
                    possibleMovesPawn[p.Line, p.Column] = true;
                }

                p.DefineNewPosition(Position.Line - 1, Position.Column - 1);
                if (Board.ValidatePosition(p) && ThereIsAnEnemy(p))
                {
                    possibleMovesPawn[p.Line, p.Column] = true;
                }

                p.DefineNewPosition(Position.Line - 1, Position.Column + 1);
                if (Board.ValidatePosition(p) && ThereIsAnEnemy(p))
                {
                    possibleMovesPawn[p.Line, p.Column] = true;
                }

                //special move en passant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (
                        Board.ValidatePosition(left)
                        && ThereIsAnEnemy(left)
                        && Board.Piece(left) == Match._vulnerableEnPassant
                    )
                    {
                        possibleMovesPawn[left.Line - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (
                        Board.ValidatePosition(right)
                        && ThereIsAnEnemy(right)
                        && Board.Piece(right) == Match._vulnerableEnPassant
                    )
                    {
                        possibleMovesPawn[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                p.DefineNewPosition(Position.Line + 1, Position.Column);
                if (Board.ValidatePosition(p) && Free(p))
                {
                    possibleMovesPawn[p.Line, p.Column] = true;
                }

                p.DefineNewPosition(Position.Line + 2, Position.Column);
                Position pAux = new Position(Position.Line + 1, Position.Column);
                if (
                    Board.ValidatePosition(pAux)
                    && Free(pAux)
                    && Board.ValidatePosition(p)
                    && Free(p)
                    && QuantityMoves == 0
                )
                {
                    possibleMovesPawn[p.Line, p.Column] = true;
                }

                p.DefineNewPosition(Position.Line + 1, Position.Column - 1);
                if (Board.ValidatePosition(p) && ThereIsAnEnemy(p))
                {
                    possibleMovesPawn[p.Line, p.Column] = true;
                }

                p.DefineNewPosition(Position.Line + 1, Position.Column + 1);
                if (Board.ValidatePosition(p) && ThereIsAnEnemy(p))
                {
                    possibleMovesPawn[p.Line, p.Column] = true;
                }

                //special move en passant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (
                        Board.ValidatePosition(left)
                        && ThereIsAnEnemy(left)
                        && Board.Piece(left) == Match._vulnerableEnPassant
                    )
                    {
                        possibleMovesPawn[left.Line + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (
                        Board.ValidatePosition(right)
                        && ThereIsAnEnemy(right)
                        && Board.Piece(right) == Match._vulnerableEnPassant
                    )
                    {
                        possibleMovesPawn[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return possibleMovesPawn;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
