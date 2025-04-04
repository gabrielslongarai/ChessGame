using ChessGame.Board.Enums;

namespace ChessGame.Application
{
    internal class ConsoleLog
    {
        public static void RenderBoard(ChessGame.Board.GameBoard board)
        {
            Console.WriteLine();
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(board.Lines - i + " ");

                for (int x = 0; x < board.Columns; x++)
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
