using ChessGame.Board;
using ChessGame.Board.Enums;
using ChessGame.Exceptions;
using ChessGame.Application;


namespace ChessGame.Roles
{
    internal class ChessMatch
    {
        private GameBoard Board { get; set; }
        private int TurnCount { get; set; }
        private EColor CurrentColor { get; set; }
        public bool Finished { get; set; }


        public ChessMatch()
        {
            Board = new GameBoard(8, 8);
            TurnCount = 1;
            CurrentColor = EColor.Green;
            Finished = false;
            StartGame();
        }

        private void StartGame()
        {
            GameBoard board = new(8, 8);

            board.AddPiece(new Rook(EColor.Red, board), new Position(0, 0));
            board.AddPiece(new Knight(EColor.Red, board), new Position(0, 1));
            board.AddPiece(new Bishop(EColor.Red, board), new Position(0, 2));
            board.AddPiece(new King(EColor.Red, board), new Position(0, 3));
            board.AddPiece(new Queen(EColor.Red, board), new Position(0, 4));
            board.AddPiece(new Bishop(EColor.Red, board), new Position(0, 5));
            board.AddPiece(new Knight(EColor.Red, board), new Position(0, 6));
            board.AddPiece(new Rook(EColor.Red, board), new Position(0, 7));
            for (int i = 0; i < 8; i++)
            {
                board.AddPiece(new Pawn(EColor.Red, board), new Position(1, i));
            }

            board.AddPiece(new Rook(EColor.Green, board), new Position(7, 0));
            board.AddPiece(new Knight(EColor.Green, board), new Position(7, 1));
            board.AddPiece(new Bishop(EColor.Green, board), new Position(7, 2));
            board.AddPiece(new King(EColor.Green, board), new Position(7, 3));
            board.AddPiece(new Queen(EColor.Green, board), new Position(7, 4));
            board.AddPiece(new Bishop(EColor.Green, board), new Position(7, 5));
            board.AddPiece(new Knight(EColor.Green, board), new Position(7, 6));
            board.AddPiece(new Rook(EColor.Green, board), new Position(7, 7));
            for (int i = 0; i < 8; i++)
            {
                board.AddPiece(new Pawn(EColor.Green, board), new Position(6, i));
            }

            ConsoleLog.RenderBoard(board);
        }

        public void MovePiece(Position origin, Position destination)
        {
            if (!Board.IsValidPosition(origin) || !Board.IsValidPosition(destination))
            {
                throw new BoardExceptions("\nInvalid position!");
            }
            if (origin.Equals(destination))
            {
                throw new BoardExceptions("\nOrigin and destination positions are the same!");
            }
            if (!Board.HasPiece(origin))
            {
                throw new BoardExceptions("\nNo piece at the origin position!");
            }

            Piece piece = Board.GetPiece(origin);

            if (piece.Color != CurrentColor)
            {
                throw new BoardExceptions("\nIt's not your turn!");
            }
            if (Board.HasPiece(destination) && Board.GetPiece(destination).Color == CurrentColor)
            {
                throw new BoardExceptions("\nYou cannot capture your own piece!");
            }
            if (Board.HasPiece(destination) && Board.GetPiece(destination).Color != CurrentColor)
            {
                Board.RemovePiece(destination); 
            }

            Board.RemovePiece(origin);
            Board.AddPiece(piece, destination);
            piece.IncreaseMoveCount();
            TurnCount++;
            CurrentColor = (CurrentColor == EColor.Green) ? EColor.Red : EColor.Green;
        }
    }
}
