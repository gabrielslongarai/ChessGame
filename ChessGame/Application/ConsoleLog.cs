using ChessGame.Board.Enums;
using ChessGame.Board;
using ChessGame.Exceptions;
using ChessGame.Roles;
using System.Collections.Generic;

namespace ChessGame.Application
{
    internal class ConsoleLog
    {
        public static void RenderBoard(ChessMatch chessMatch, bool[,] possibleMoves)
        {
            for (int i = 0; i < chessMatch.GameBoard.Columns; i++)
            {
                Console.Write(chessMatch.GameBoard.Columns - i + " ");

                for (int x = 0; x < chessMatch.GameBoard.Lines; x++)
                {
                    RenderPiece(chessMatch.GameBoard.Pieces[i, x], i, x, possibleMoves[i, x]);
                }

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            ShowTurn(chessMatch.TurnCount);
            ShowCurrentColor(chessMatch.CurrentColor);
            ShowCapturedPieces(chessMatch);
        }

        public static void RenderBoard(ChessMatch chessMatch)
        {
            for (int i = 0; i < chessMatch.GameBoard.Columns; i++)
            {
                Console.Write(chessMatch.GameBoard.Columns - i + " ");

                for (int x = 0; x < chessMatch.GameBoard.Lines; x++)
                {
                    RenderPiece(chessMatch.GameBoard.Pieces[i, x], i, x);
                }

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            ShowTurn(chessMatch.TurnCount);
            ShowCurrentColor(chessMatch.CurrentColor);
            ShowCapturedPieces(chessMatch);
        }

        private static void ShowTurn(int turn)
        {
            Console.WriteLine($"\nTurn: {turn}");
        }

        private static void ShowCurrentColor(EColor color)
        {
            if (color == EColor.Green)
            {
                Console.Write($"Current color: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(color);
            }
            else if (color == EColor.Red)
            {
                Console.Write($"Current color: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(color);
            }
            else
            {
                Console.Write($"Current color: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(color);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ShowCapturedPieces(ChessMatch chessMatch)
        {
            Console.WriteLine("\nCaptured Pieces");
            Console.Write("Green: ");
            HashSet<Piece> greenPieces = chessMatch.GetCapturedPieces(EColor.Green);

            foreach (Piece piece in greenPieces)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(piece + " ");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Write("\nRed: ");
            HashSet<Piece> redPieces = chessMatch.GetCapturedPieces(EColor.Red);

            foreach (Piece piece in redPieces)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(piece + " ");
                Console.ForegroundColor = ConsoleColor.White;
            }
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

        private static void ValidateChessNotation(string input)
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
