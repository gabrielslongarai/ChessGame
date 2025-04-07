using ChessGame.Board.Enums;
using ChessGame.Board;

namespace ChessGame.Roles
{
    internal class Knight(EColor color, GameBoard board) : Piece(color, board)
    {
        public override string ToString()
        {
            return "♞";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMoves = new bool[Board.Lines, Board.Columns];
            Position position = new Position(0, 0);

            // Up 2, Right 1
            position.SetValues(Position.Line - 2, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            // Up 2, Left 1
            position.SetValues(Position.Line - 2, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            // Down 2, Right 1
            position.SetValues(Position.Line + 2, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            // Down 2, Left 1
            position.SetValues(Position.Line + 2, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            // Right 2, Up 1
            position.SetValues(Position.Line - 1, Position.Column + 2);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            // Right 2, Down 1
            position.SetValues(Position.Line + 1, Position.Column + 2);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            // Left 2, Up 1
            position.SetValues(Position.Line - 1, Position.Column - 2);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            // Left 2, Down 1
            position.SetValues(Position.Line + 1, Position.Column - 2);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            return possibleMoves;
        }
    }
}
