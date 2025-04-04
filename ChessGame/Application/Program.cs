using System.Globalization;
using System.Text;
using ChessGame.Roles;
using ChessGame.Exceptions;


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

                ChessMatch newGame = new();

                do
                {
                    Console.WriteLine("\n\nDo you want to play again?");
                    Console.WriteLine("\nPress 1 for Yes or 2 for No.");
                    char response = char.Parse(Console.ReadLine()!);
                    if (response == '2')
                    {
                        break;
                    }
                    else if (response != '1')
                    {
                        Console.WriteLine("\nInvalid option. Please try again.");
                        continue;
                    }
                    else
                    {
                        Console.Clear();
                        newGame = new ChessMatch();
                    }
                } while (newGame.Finished == false);
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
