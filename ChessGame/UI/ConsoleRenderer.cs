using ChessGame.Entities;
using System;
using System.Text.Encodings;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.Entities.Enums;


namespace ChessGame.UI
{
    internal class ConsoleRenderer
    {
        public static void RenderBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
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
        }

        private static ConsoleColor GetConsoleColor(Color color)
        {
            switch (color)
            {
                case Color.Green:
                    return ConsoleColor.Green;
                case Color.Red:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
