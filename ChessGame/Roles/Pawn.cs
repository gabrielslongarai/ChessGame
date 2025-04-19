using ChessGame.Board;
using ChessGame.Board.Enums;


namespace ChessGame.Roles
{
    internal class Pawn : Piece
    {
        public ChessMatch ChessMatch { get; private set; }

        public Pawn(EColor color, GameBoard board, ChessMatch chessMatch) : base(color, board)
        {
            ChessMatch = chessMatch;
        }

        public override string ToString()
        {
            return "♙";
        }

        private bool ExistsEnemy(Position positin)
        {
            Piece piece = Board.GetPiece(positin);
            return piece != null && piece.Color != this.Color;
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

            //EnPassant
            if (Color == EColor.Green && Position.Line == 3)
            {
                Position left = new(Position.Line, Position.Column - 1);

                if (Board.IsValidPosition(left) && ExistsEnemy(left) && Board.GetPiece(left) == ChessMatch.CanTakeEnPassant)
                {
                    possibleMoves[left.Line - 1, left.Column] = true;
                }

                Position right = new(Position.Line, Position.Column + 1);

                if (Board.IsValidPosition(right) && ExistsEnemy(right) && Board.GetPiece(right) == ChessMatch.CanTakeEnPassant)
                {
                    possibleMoves[right.Line - 1, right.Column] = true;
                }
            }

            if (Color == EColor.Red && Position.Line == 4)
            {
                Position left = new(Position.Line, Position.Column - 1);

                if (Board.IsValidPosition(left) && ExistsEnemy(left) && Board.GetPiece(left) == ChessMatch.CanTakeEnPassant)
                {
                    possibleMoves[left.Line + 1, left.Column] = true;
                }

                Position right = new(Position.Line, Position.Column + 1);

                if (Board.IsValidPosition(right) && ExistsEnemy(right) && Board.GetPiece(right) == ChessMatch.CanTakeEnPassant)
                {
                    possibleMoves[right.Line + 1, right.Column] = true;
                }
            }

            return possibleMoves;
        }
    }
}
