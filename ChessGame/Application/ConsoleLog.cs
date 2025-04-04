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

            char column = input[0];
            int line = int.Parse(input[1].ToString());

            ChessNotation notation = new ChessNotation(column, line);
            Position position = notation.ToPosition();

            GameBoard board = new GameBoard(8, 8);
            board.ValidatePosition(position);

            return notation;
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
