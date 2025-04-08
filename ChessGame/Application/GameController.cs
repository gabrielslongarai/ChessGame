using ChessGame.Board;
using ChessGame.Exceptions;
using ChessGame.Roles;

namespace ChessGame.Application
{
    internal class GameController
    {
        public static void InitializeGame(ChessMatch chessMatch)
        {
            while (!chessMatch.Finished)
            {
                try
                {
                    ConsoleLog.RenderBoard(chessMatch);

                    Console.Write("\nOrigin: ");
                    Position origin = ConsoleLog.ReadChessNotation().ToPosition();
                    Console.WriteLine();


                    Console.Clear();

                    chessMatch.ValidateOriginPosition(origin);
                    bool[,] possibleMoves = chessMatch.GameBoard.GetPiece(origin).PossibleMoves();
                    ConsoleLog.RenderBoard(chessMatch, possibleMoves);



                    Console.Write("\nDestination: ");
                    Position destination = ConsoleLog.ReadChessNotation().ToPosition();
                    chessMatch.MovePiece(origin, destination);
                    Console.Clear();
                }
                catch (BoardExceptions e)
                {
                    ConsoleLog.RenderBoard(chessMatch);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{e.Message}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
            }
        }
    }
}
