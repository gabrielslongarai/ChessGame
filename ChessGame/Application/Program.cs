using System.Globalization;
using System.Text;
using ChessGame.Roles;
using ChessGame.Exceptions;
using ChessGame.Board;


namespace ChessGame.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
                Console.OutputEncoding = Encoding.UTF8;

                ChessMatch chessMatch = new();

                while (!chessMatch.Finished)
                {
                   
                    ConsoleLog.RenderBoard(chessMatch.GameBoard);
                    Console.Write("Type the origin position: ");
                    Position origin = ConsoleLog.ReadChessNotation().ToPosition();
                    Console.Write("Type the destination position: ");
                    Position destination = ConsoleLog.ReadChessNotation().ToPosition();
                    chessMatch.MovePiece(origin, destination);
                }


            }
            catch (BoardExceptions e)
            {
                Console.WriteLine($"\n{e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nAn unexpected error occurred: " + e.Message);
            }
            finally
            {
                Console.WriteLine("\nPress any key to exit...");
            }


        }
    }
}
