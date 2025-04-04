using ChessGame.Board.Enums;
using ChessGame.Board;
using ChessGame.Exceptions;

namespace ChessGame.Application
{
    internal class ConsoleLog
    {
        public static void RenderBoard(ChessGame.Board.GameBoard board)
        {
            for (int i = 0; i < board.Columns; i++)
            {
                Console.Write(board.Columns - i + " ");

                for (int x = 0; x < board.Lines; x++)
                {
                    if (board.Pieces[i, x] == null)
                    {
                        Console.Write((i + x) % 2 == 0 ? "░░" : "██");
                    }
                    else
                    {
                        Console.ForegroundColor = GetConsoleColor(board.Pieces[i, x].Color);
                        Console.Write(board.Pieces[i, x] + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static ChessNotation ReadChessNotation()
        {
            string input = Console.ReadLine().ToLower();

            if (string.IsNullOrEmpty(input) ||
                input.Length != 2 ||
                !char.IsLetter(input[0]) ||
                !char.IsDigit(input[1]) ||
                input[0] < 'a' || input[0] > 'h' ||
                input[1] < '1' || input[1] > '8')
            {
                throw new BoardExceptions("Invalid chess notation. Please enter a valid position (e.g., e2).");
            }


            char column = input[0];
            int line = int.Parse(input[1].ToString());

            return new ChessNotation(column, line);
        }

        private static ConsoleColor GetConsoleColor(EColor color)
        {
            switch (color)
            {
                case EColor.Green:
                    return ConsoleColor.Green;
                case EColor.Red:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
