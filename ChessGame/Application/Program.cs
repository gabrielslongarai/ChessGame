using System.Globalization;
using System.Text;
using ChessGame.Roles;


namespace ChessGame.Application
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
                Console.OutputEncoding = Encoding.UTF8;

                ChessMatch chessMatch = new();
                GameController.InitializeGame(chessMatch);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nAn unexpected error occurred: " + e.Message);
            }
            finally
            {
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
