using Chess;

namespace GameBoard
{
    public class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] _pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            _pieces = new Piece[Lines, Columns];
        }

        public Piece Piece(int line, int column)
        {
            return _pieces[line, column];
        }

        public Piece Piece(Position position)
        {
            return _pieces[position.Line, position.Column];
        }

        public bool PieceExists(Position position)
        {
            CheckPosition(position);
            return Piece(position) != null;
        }

        public void InsertPiece(Piece piece, Position position)
        {
            if (PieceExists(position))
            {
                throw new BoardException("There is already a piece in that position!!");
            }
            _pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (Piece(position) == null)
            {
                return null;
            }
            Piece piece = Piece(position);
            piece.Position = null;
            _pieces[position.Line, position.Column] = null;
            return piece;
        }

        public bool ValidatePosition(Position position)
        {
            if (position.Line < 0 || position.Line >= Lines || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void CheckPosition(Position position)
        {
            if (!ValidatePosition(position))
            {
                throw new BoardException("Invalid position!!");
            }
        }

        internal void InsertPiece(Rook rook, ChessPosition chessPosition)
        {
            throw new NotImplementedException();
        }
    }
}