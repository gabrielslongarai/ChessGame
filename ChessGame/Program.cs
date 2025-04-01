using System.Text;
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

            board.AddPiece(new Piece(new Position(0, 0), Color.Red, board, "♜"), new Position(0, 0));
            board.AddPiece(new Piece(new Position(0, 1), Color.Red, board, "♞"), new Position(0, 1));
            board.AddPiece(new Piece(new Position(0, 2), Color.Red, board, "♝"), new Position(0, 2));
            board.AddPiece(new Piece(new Position(0, 3), Color.Red, board, "♛"), new Position(0, 3));
            board.AddPiece(new Piece(new Position(0, 4), Color.Red, board, "♚"), new Position(0, 4));
            board.AddPiece(new Piece(new Position(0, 5), Color.Red, board, "♝"), new Position(0, 5));
            board.AddPiece(new Piece(new Position(0, 6), Color.Red, board, "♞"), new Position(0, 6));
            board.AddPiece(new Piece(new Position(0, 7), Color.Red, board, "♜"), new Position(0, 7));

            UI.ConsoleRenderer.RenderBoard(board);
        }
    }
}
