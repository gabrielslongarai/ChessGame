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

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMoves = new bool[Board.Lines, Board.Columns];
            Position position = new Position(0, 0);

            // Up
            position.SetValues(Position.Line - 1, Position.Column);
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
            position.SetValues(Position.Line + 1, Position.Column);
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
            position.SetValues(Position.Line, Position.Column - 1);
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
            position.SetValues(Position.Line, Position.Column + 1);
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


    }
}
