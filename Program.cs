using Chess;
using GameBoard;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch chessMatch = new ChessMatch();

                while (!chessMatch.EndGame)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(chessMatch);

                        Console.WriteLine();
                        Console.Write("Enter origin position: ");
                        Position origin = Screen.ReadPosition().ConvertPosition();
                        chessMatch.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = chessMatch.Board.Piece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(chessMatch.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Enter destiny position: ");
                        Position destiny = Screen.ReadPosition().ConvertPosition();
                        chessMatch.ValidateDestinyPosition(origin, destiny);

                        chessMatch.PerformMovement(origin, destiny);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.PrintMatch(chessMatch);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}