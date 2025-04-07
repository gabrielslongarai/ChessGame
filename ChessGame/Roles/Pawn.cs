using ChessGame.Board;
using ChessGame.Board.Enums;


namespace ChessGame.Roles
{
    internal class Pawn(EColor color, GameBoard board) : Piece(color, board)
    {
        public override string ToString()
        {
            return "♙";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMoves = new bool[Board.Lines, Board.Columns];
            Position position = new Position(0, 0);

            // Set movement direction according to the color
            int direction = (Color == EColor.Green) ? -1 : 1;

            // Foward Movement (Just one time)
            position.SetValues(Position.Line + direction, Position.Column);
            if (Board.IsValidPosition(position) && !Board.HasPiece(position))
            {
                possibleMoves[position.Line, position.Column] = true;

                // Foward Movement (Twice) - Just for the first movement
                if (MoveCount == 0)
                {
                    position.SetValues(Position.Line + (direction * 2), Position.Column);
                    if (Board.IsValidPosition(position) && !Board.HasPiece(position))
                    {
                        possibleMoves[position.Line, position.Column] = true;
                    }
                }
            }

            // Diagonal movement to capture (left)
            position.SetValues(Position.Line + direction, Position.Column - 1);
            if (Board.IsValidPosition(position) && Board.HasPiece(position) && Board.GetPiece(position).Color != this.Color)
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            // Diagonal movement to capture (right)
            position.SetValues(Position.Line + direction, Position.Column + 1);
            if (Board.IsValidPosition(position) && Board.HasPiece(position) && Board.GetPiece(position).Color != this.Color)
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            return possibleMoves;
        }
    }
}
