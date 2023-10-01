namespace GameBoard
{
    public abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QuantityMoves { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Color color, Board board)
        {
            Color = color;
            Board = board;
            Position = null;
            QuantityMoves = 0;
        }

        public void IncrementsMovements()
        {
            QuantityMoves++;
        }

        public void DecreaseMovements()
        {
            QuantityMoves--;
        }

        public bool ThereArePossibleMoves()
        {
            bool[,] movement = PossibleMoves();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (movement[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position position)
        {
            return PossibleMoves()[position.Line, position.Column];
        }

        public abstract bool[,] PossibleMoves();
    }
}