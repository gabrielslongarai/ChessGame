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

            Piece piece = new Piece(new Position(0, 0), Color.Black, new Board(8, 8), "♚");
            Console.WriteLine(piece);

            UI.ConsoleRenderer.RenderBoard(new Board(8, 8));
        }
    }
}
