using ChessGame.Board;
using ChessGame.Board.Enums;

namespace ChessGame.Roles
{
    internal class Rook(EColor color, GameBoard board) : Piece(color, board)
    {
        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMoves = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            // Up
            position.SetValues(position.Line - 1, position.Column);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
                if (Board.HasPiece(position) && Board.GetPiece(position).Color != this.Color)
                {
                    break;
                }
                position.Line--;
            }

            // Down
            position.SetValues(position.Line + 1, position.Column);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
                if (Board.HasPiece(position) && Board.GetPiece(position).Color != this.Color)
                {
                    break;
                }
                position.Line++;
            }

            // Left
            position.SetValues(position.Line, position.Column - 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
                if (Board.HasPiece(position) && Board.GetPiece(position).Color != this.Color)
                {
                    break;
                }
                position.Column--;
            }

            // Right
            position.SetValues(position.Line, position.Column + 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
                if (Board.HasPiece(position) && Board.GetPiece(position).Color != this.Color)
                {
                    break;
                }
                position.Column++;
            }

            return possibleMoves;
        }

        public override string ToString()
        {
            return "♜";
        }
    }
}
