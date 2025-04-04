using ChessGame.Board.Enums;
using ChessGame.Board;

namespace ChessGame.Roles
{
    internal class Queen(EColor color, GameBoard board) : Piece(color, board)
    {
        public override bool[,] PossibleMoves()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "♛";
        }
    }
}
