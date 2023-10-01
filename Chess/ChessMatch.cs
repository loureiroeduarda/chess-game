using System.Reflection.PortableExecutable;
using GameBoard;

namespace Chess
{
    public class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool EndGame { get; private set; }
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _captured;
        public bool Check { get; private set; }
        public Piece _vulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            EndGame = false;
            Check = false;
            _vulnerableEnPassant = null;
            _pieces = new HashSet<Piece>();
            _captured = new HashSet<Piece>();
            InsertBoardPiece();
        }

        public Piece ExecuteMovement(Position origin, Position destiny)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementsMovements();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(piece, destiny);
            if (capturedPiece != null)
            {
                _captured.Add(capturedPiece);
            }

            //special move: small rock
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDestiny = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementsMovements();
                Board.InsertPiece(rook, rookDestiny);
            }

            //special move: big rock
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDestiny = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementsMovements();
                Board.InsertPiece(rook, rookDestiny);
            }

            //special move en passant
            if (piece is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == null)
                {
                    Position pPawn;
                    if (piece.Color == Color.White)
                    {
                        pPawn = new Position(destiny.Line + 1, destiny.Column);
                    }
                    else
                    {
                        pPawn = new Position(destiny.Line - 1, destiny.Column);
                    }
                    capturedPiece = Board.RemovePiece(pPawn);
                    _captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(destiny);
            piece.DecreaseMovements();
            if (capturedPiece != null)
            {
                Board.InsertPiece(capturedPiece, destiny);
                _captured.Remove(capturedPiece);
            }
            Board.InsertPiece(piece, origin);

            //special move: small castle
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDestiny = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookDestiny);
                rook.DecreaseMovements();
                Board.InsertPiece(rook, rookOrigin);
            }

            //special move: big castle
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDestiny = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookDestiny);
                rook.DecreaseMovements();
                Board.InsertPiece(rook, rookOrigin);
            }

            //special move en passant
            if (piece is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == _vulnerableEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destiny);
                    Position pPawn;
                    if (piece.Color == Color.White)
                    {
                        pPawn = new Position(3, destiny.Column);
                    }
                    else
                    {
                        pPawn = new Position(4, destiny.Column);
                    }
                    Board.InsertPiece(pawn, pPawn);
                }
            }
        }

        public void PerformMovement(Position origin, Position destiny)
        {
            Piece capturedPiece = ExecuteMovement(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, capturedPiece);
                throw new BoardException("You can't put yourself in check! Make a new move!!");
            }

            Piece piece = Board.Piece(destiny);

            //special move promotion
            if (piece is Pawn)
            {
                if ((piece.Color == Color.White && destiny.Line == 0) || (piece.Color == Color.Black && destiny.Line == 7))
                {
                    piece = Board.RemovePiece(destiny);
                    _pieces.Remove(piece);
                    Piece queen = new Queen(piece.Color, Board);
                    Board.InsertPiece(queen, destiny);
                    _pieces.Add(queen);
                }
            }

            if (IsInCheck(Adversary(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (IsInCheckmate(Adversary(CurrentPlayer)))
            {
                EndGame = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }

            //special move en passant
            if (
                piece is Pawn
                && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2)
            )
            {
                _vulnerableEnPassant = piece;
            }
            else
            {
                _vulnerableEnPassant = null;
            }
        }

        public void ValidateOriginPosition(Position position)
        {
            if (Board.Piece(position) == null)
            {
                throw new BoardException(
                    "There is no piece in the chosen origin position! Try again!!"
                );
            }
            if (CurrentPlayer != Board.Piece(position).Color)
            {
                throw new BoardException("The source part chosen is not yours! Try again!!");
            }
            if (!Board.Piece(position).ThereArePossibleMoves())
            {
                throw new BoardException(
                    "There are no possible moves for the chosen piece! Try again!!"
                );
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.Piece(origin).CanMoveTo(destiny))
            {
                throw new BoardException("Destination position is invalid! Try again!!");
            }
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> assistant = new HashSet<Piece>();
            foreach (Piece piece in _captured)
            {
                if (piece.Color == color)
                {
                    assistant.Add(piece);
                }
            }
            return assistant;
        }

        public HashSet<Piece> PiecesInPlay(Color color)
        {
            HashSet<Piece> assistant = new HashSet<Piece>();
            foreach (Piece piece in _pieces)
            {
                if (piece.Color == color)
                {
                    assistant.Add(piece);
                }
            }
            assistant.ExceptWith(CapturedPieces(color));
            return assistant;
        }

        private static Color Adversary(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach (Piece piece in PiecesInPlay(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece king =
                King(color)
                ?? throw new BoardException("There is no " + color + " color king on the board!");

            foreach (Piece piece in PiecesInPlay(Adversary(color)))
            {
                bool[,] movement = piece.PossibleMoves();
                if (movement[king.Position.Line, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsInCheckmate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }

            foreach (Piece piece in PiecesInPlay(color))
            {
                bool[,] movement = piece.PossibleMoves();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (movement[i, j])
                        {
                            Position origin = piece.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = ExecuteMovement(origin, destiny);
                            bool testCheck = IsInCheck(color);
                            UndoMove(origin, destiny, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void InsertNewPiece(char columnn, int line, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(columnn, line).ConvertPosition());
            _pieces.Add(piece);
        }

        private void InsertBoardPiece()
        {
            InsertNewPiece('a', 1, new Rook(Color.White, Board));
            InsertNewPiece('b', 1, new Knight(Color.White, Board));
            InsertNewPiece('c', 1, new Bishop(Color.White, Board));
            InsertNewPiece('d', 1, new Queen(Color.White, Board));
            InsertNewPiece('e', 1, new King(Color.White, Board, this));
            InsertNewPiece('f', 1, new Bishop(Color.White, Board));
            InsertNewPiece('g', 1, new Knight(Color.White, Board));
            InsertNewPiece('h', 1, new Rook(Color.White, Board));
            InsertNewPiece('a', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('b', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('c', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('d', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('e', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('f', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('g', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('h', 2, new Pawn(Color.White, Board, this));

            InsertNewPiece('a', 8, new Rook(Color.Black, Board));
            InsertNewPiece('b', 8, new Knight(Color.Black, Board));
            InsertNewPiece('c', 8, new Bishop(Color.Black, Board));
            InsertNewPiece('d', 8, new Queen(Color.Black, Board));
            InsertNewPiece('e', 8, new King(Color.Black, Board, this));
            InsertNewPiece('f', 8, new Bishop(Color.Black, Board));
            InsertNewPiece('g', 8, new Knight(Color.Black, Board));
            InsertNewPiece('h', 8, new Rook(Color.Black, Board));
            InsertNewPiece('a', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('b', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('c', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('d', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('e', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('f', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('g', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('h', 7, new Pawn(Color.Black, Board, this));
        }
    }
}
