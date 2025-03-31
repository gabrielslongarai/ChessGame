using ChessGame.Entities;
using System;
using System.Text.Encodings;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(board.Pieces[i, x] + " ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
