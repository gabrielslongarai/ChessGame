using ChessGame.Board.Enums;

namespace ChessGame.Board
{
    internal class Piece(EColor color, ChessGame.Board.GameBoard board)
    {
        public Position? Position { get; set; } = null;
        public EColor Color { get; protected set; } = color;
        public int MoveCount { get; protected set; } = 0;
        public ChessGame.Board.GameBoard Board { get; protected set; } = board;

        public void IncreaseMoveCount()
        {
            MoveCount++;
        }
    }
}
