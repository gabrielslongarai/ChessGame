using ChessGame.Board.Enums;
using ChessGame.Board;
using ChessGame.Exceptions;

namespace ChessGame.Application
{
    internal class ConsoleLog
    {
        public static void RenderBoard(GameBoard board)
        {
            for (int i = 0; i < board.Columns; i++)
            {
                Console.Write(board.Columns - i + " ");

                for (int x = 0; x < board.Lines; x++)
                {
                    RenderPiece(board.Pieces[i, x], i, x);
                }

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void RenderBoard(GameBoard board, bool[,] possibleMoves)
        {
            for (int i = 0; i < board.Columns; i++)
            {
                Console.Write(board.Columns - i + " ");

                for (int x = 0; x < board.Lines; x++)
                {
                    RenderPiece(board.Pieces[i, x], i, x, possibleMoves[i, x]);
                }

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        private static void RenderPiece(Piece piece, int i, int x, bool isPossibleMove)
        { 
            if (piece == null)
            {
                if (isPossibleMove)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write((i + x) % 2 == 0 ? "░░" : "██");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write((i + x) % 2 == 0 ? "░░" : "██");
                }

            }
            else
            {
                if (isPossibleMove)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = GetConsoleColor(piece.Color);
                    Console.Write(piece + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
        }

        private static void RenderPiece(Piece piece, int i, int x)
        {
            if (piece == null)
            {
                Console.Write((i + x) % 2 == 0 ? "░░" : "██");
            }
            else
            {
                Console.ForegroundColor = GetConsoleColor(piece.Color);
                Console.Write(piece + " ");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void ValidateChessNotation(string input)
        {
            if (input == "")
            {
                throw new BoardExceptions("\nIt can't be empty!");
            }
            if (input.Length != 2)
            {
                throw new BoardExceptions("\nInvalid input! It must be two characters.");
            }
        }

        public static ChessNotation ReadChessNotation()
        {
            string input = Console.ReadLine().ToLower();

            ValidateChessNotation(input);

            char column = input[0];
            int line = int.Parse(input[1].ToString());

            ChessNotation notation = new ChessNotation(column, line);
            Position position = notation.ToPosition();

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
