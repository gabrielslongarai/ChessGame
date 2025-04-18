using ChessGame.Board;
using ChessGame.Board.Enums;

namespace ChessGame.Roles
{
    internal class King : Piece
    {
        public ChessMatch ChessMatch { get; private set; }

        public King(EColor color, GameBoard board, ChessMatch chessMatch) : base(color, board)
        {
            ChessMatch = chessMatch;
        }

        public override string ToString()
        {
            return "♚";
        }

        private bool ValidateRookForCastling(Position position)
        {
            Piece piece = Board.GetPiece(position);

            return piece != null
                && piece is Rook
                && piece.Color == Color
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


            if (MoveCount == 0 && ChessMatch.Check == false)
            {
                //Kingside castling
                Position kingsideRookPosition = new Position(Position.Line, Position.Column + 3);

                if (ValidateRookForCastling(kingsideRookPosition))
                {
                    Position kingPlusOne = new(Position.Line, Position.Column + 1);
                    Position kingPlusTwo = new(Position.Line, Position.Column + 2);

                    if (Board.GetPiece(kingPlusOne) == null && Board.GetPiece(kingPlusTwo) == null)
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

                    if (Board.GetPiece(kingMinusOne) == null && Board.GetPiece(kingMinusTwo) == null && Board.GetPiece(kingMinusThree) == null)
                    {
                        possibleMoves[Position.Line, Position.Column - 2] = true; 
                    }
                }
            }

            return possibleMoves;
        }
    }
}