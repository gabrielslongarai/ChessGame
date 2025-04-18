using ChessGame.Board;
using ChessGame.Board.Enums;

namespace ChessGame.Roles
{
    internal class King(EColor color, GameBoard board, ChessMatch chessMatch) : Piece(color, board)
    {
        public override string ToString()
        {
            return "♚";
        }

        private bool ValidateRookForCastling(Position position)
        {
            Piece piece = board.GetPiece(position);

            return piece != null
                && piece is Rook
                && piece.Color == color
                && piece.MoveCount == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibleMoves = new bool[Board.Lines, Board.Columns];
            Position position = new Position(0, 0);

            //Up
            position.SetValues(Position.Line - 1, Position.Column);
            if (Board.IsValidPosition(position) && CanMove(position))
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
            if (Board.IsValidPosition(position) && CanMove(position))
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


            if (MoveCount == 0 && chessMatch.Check == false)
            {
                //Kingside castling
                Position kingsideRookPosition = new Position(Position.Line, Position.Column + 3);

                if (ValidateRookForCastling(kingsideRookPosition))
                {
                    Position kingPlusOne = new(Position.Line, Position.Column + 1);
                    Position kingPlusTwo = new(Position.Line, Position.Column + 2);

                    if (board.GetPiece(kingPlusOne) == null && board.GetPiece(kingPlusTwo) == null)
                    {
                        possibleMoves[Position.Line, Position.Column + 2] = true;
                    }
                }

                //Queenside castling
                Position queensideRookPosition = new Position(Position.Line, Position.Column - 4);

                if (ValidateRookForCastling(kingsideRookPosition))
                {
                    Position kingMinusOne = new(Position.Line, Position.Column - 1);
                    Position kingMinusTwo = new(Position.Line, Position.Column - 2);
                    Position kingMinusThree = new(Position.Line, Position.Column - 3);

                    if (board.GetPiece(kingMinusOne) == null && board.GetPiece(kingMinusTwo) == null && board.GetPiece(kingMinusThree) == null)
                    {
                        possibleMoves[Position.Line, Position.Column - 2] = true; 
                    }
                }
            }

            return possibleMoves;
        }
    }
}