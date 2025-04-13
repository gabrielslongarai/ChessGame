using ChessGame.Board.Enums;

namespace ChessGame.Board
{
    internal abstract class Piece(EColor color, ChessGame.Board.GameBoard board)
    {
        public Position? Position { get; set; } = null;
        public EColor Color { get; protected set; } = color;
        public int MoveCount { get; protected set; } = 0;
        public ChessGame.Board.GameBoard Board { get; protected set; } = board;


        public abstract bool[,] PossibleMoves();

        public bool IsThereAnyPossibleMove()
        {
            bool[,] mat = PossibleMoves();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMove(Position position)
        {
            Piece? piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }
        public void IncreaseMoveCount()
        {
            MoveCount++;
        }
        public void DecreaseMoveCount()
        {
            MoveCount--;
        }
    }
}
