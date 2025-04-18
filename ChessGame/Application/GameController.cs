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

                    Console.Write("\n\nOrigin: ");
                    Position origin = ConsoleLog.ReadChessNotation().ToPosition();
                    Console.WriteLine();

                    Console.Clear();
                    ConsoleLog.RenderBoard(chessMatch, chessMatch.GetPossibleMoves(origin));

                    Console.Write("\n\nDestination: ");
                    Position destination = ConsoleLog.ReadChessNotation().ToPosition();
                    chessMatch.PerformPlay(origin, destination);
                    Console.Clear();
                }
                catch (BoardExceptions e)
                {
                    Console.Clear();
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
            ConsoleLog.RenderBoard(chessMatch);
            Console.WriteLine("\n>>>CHECKMATE!<<<");
        }
    }
}
