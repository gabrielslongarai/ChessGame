using ChessGame.Board;
using ChessGame.Board.Enums;


namespace ChessGame.Roles
{
    internal class Bishop(EColor color, GameBoard board) : Piece(color, board)
    {
        public override string ToString()
        {
            return "♝";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMoves = new bool[Board.Lines, Board.Columns];
            Position position = new Position(0, 0);

            // Up-Right
            position.SetValues(Position.Line - 1, Position.Column + 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
                if (Board.HasPiece(position) && Board.GetPiece(position).Color != this.Color)
                {
                    break;
                }
                position.SetValues(position.Line - 1, position.Column + 1);
            }

            // Up-Left
            position.SetValues(Position.Line - 1, Position.Column - 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
                if (Board.HasPiece(position) && Board.GetPiece(position).Color != this.Color)
                {
                    break;
                }
                position.SetValues(position.Line - 1, position.Column - 1);
            }

            // Down-Right
            position.SetValues(Position.Line + 1, Position.Column + 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
                if (Board.HasPiece(position) && Board.GetPiece(position).Color != this.Color)
                {
                    break;
                }
                position.SetValues(position.Line + 1, position.Column + 1);
            }

            // Down-Left
            position.SetValues(Position.Line + 1, Position.Column - 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
                if (Board.HasPiece(position) && Board.GetPiece(position).Color != this.Color)
                {
                    break;
                }
                position.SetValues(position.Line + 1, position.Column - 1);
            }

            return possibleMoves;
        }
    }
}
