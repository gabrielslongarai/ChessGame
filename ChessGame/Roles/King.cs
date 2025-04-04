using ChessGame.Board;
using ChessGame.Board.Enums;

namespace ChessGame.Roles
{
    internal class King(EColor color, GameBoard board) : Piece(color, board)
    {
        public override string ToString()
        {
            return "♚";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMoves = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            //Above
            position.SetValues(Position.Line - 1, Position.Column);
            if(Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            //Below
            position.SetValues(Position.Line + 1, Position.Column);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            //Left

            position.SetValues(Position.Line, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            //Right
            position.SetValues(Position.Line, Position.Column + 1);
            if(Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            //Northeast
            position.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            //Northwest
            position.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            //Southeast
            position.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            //Southwest
            position.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMoves[position.Line, position.Column] = true;
            }

            return possibleMoves;
        }
    }
}
