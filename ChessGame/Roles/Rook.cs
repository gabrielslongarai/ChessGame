using ChessGame.Board;
using ChessGame.Board.Enums;

namespace ChessGame.Roles
{
    internal class Rook(EColor color, GameBoard board) : Piece(color, board)
    {
        public override string ToString()
        {
            return "♜";
        }
    }
}
