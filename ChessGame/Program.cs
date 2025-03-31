using System.Diagnostics;
using System.Text;
using System.Text.Encodings;
using ChessGame.Entities;
using ChessGame.Entities.Enums;

namespace ChessGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            

            Board board = new Board(8, 8);

            board.Pieces[0, 0] = new Piece(new Position(0, 0), Color.Green, board, "♚");

            UI.ConsoleRenderer.RenderBoard(board);
        }
    }
}
