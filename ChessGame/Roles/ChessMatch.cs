using ChessGame.Board;
using ChessGame.Board.Enums;
using ChessGame.Exceptions;
using ChessGame.Application;


namespace ChessGame.Roles
{
    internal class ChessMatch
    {
        public GameBoard GameBoard { get; private set; }
        private int TurnCount { get; set; }
        private EColor CurrentColor { get; set; }
        public bool Finished { get; set; }


        public ChessMatch()
        {
            GameBoard gameBoard = new(8, 8);
            GameBoard = gameBoard;
            TurnCount = 1;
            CurrentColor = EColor.Green;
            Finished = false;
            SetupPieces();
        }

        private void SetupPieces()
        {
            

            GameBoard.AddPiece(new Rook(EColor.Red, GameBoard), new Position(0, 0));
            GameBoard.AddPiece(new Knight(EColor.Red, GameBoard), new Position(0, 1));
            GameBoard.AddPiece(new Bishop(EColor.Red, GameBoard), new Position(0, 2));
            GameBoard.AddPiece(new King(EColor.Red, GameBoard), new Position(0, 3));
            GameBoard.AddPiece(new Queen(EColor.Red, GameBoard), new Position(0, 4));
            GameBoard.AddPiece(new Bishop(EColor.Red, GameBoard), new Position(0, 5));
            GameBoard.AddPiece(new Knight(EColor.Red, GameBoard), new Position(0, 6));
            GameBoard.AddPiece(new Rook(EColor.Red, GameBoard), new Position(0, 7));
            for (int i = 0; i < 8; i++)
            {
                GameBoard.AddPiece(new Pawn(EColor.Red, GameBoard), new Position(1, i));
            }

            GameBoard.AddPiece(new Rook(EColor.Green, GameBoard), new Position(7, 0));
            GameBoard.AddPiece(new Knight(EColor.Green, GameBoard), new Position(7, 1));
            GameBoard.AddPiece(new Bishop(EColor.Green, GameBoard), new Position(7, 2));
            GameBoard.AddPiece(new King(EColor.Green, GameBoard), new Position(7, 3));
            GameBoard.AddPiece(new Queen(EColor.Green, GameBoard), new Position(7, 4));
            GameBoard.AddPiece(new Bishop(EColor.Green, GameBoard), new Position(7, 5));
            GameBoard.AddPiece(new Knight(EColor.Green, GameBoard), new Position(7, 6));
            GameBoard.AddPiece(new Rook(EColor.Green, GameBoard), new Position(7, 7));
            for (int i = 0; i < 8; i++)
            {
                GameBoard.AddPiece(new Pawn(EColor.Green, GameBoard), new Position(6, i));
            }
        }

        public void MovePiece(Position origin, Position destination)
        {
           
            if (origin.Equals(destination))
            {
                throw new BoardExceptions("\nOrigin and destination positions are the same!");
            }
            if (!GameBoard.HasPiece(origin))
            {
                throw new BoardExceptions("\nNo piece at the origin position!");
            }

            Piece piece = GameBoard.GetPiece(origin);

            if (piece.Color != CurrentColor)
            {
                throw new BoardExceptions("\nIt's not your turn!");
            }
            if (GameBoard.HasPiece(destination) && GameBoard.GetPiece(destination).Color == CurrentColor)
            {
                throw new BoardExceptions("\nYou cannot capture your own piece!");
            }
            if (GameBoard.HasPiece(destination) && GameBoard.GetPiece(destination).Color != CurrentColor)
            {
                GameBoard.RemovePiece(destination); 
            }

            GameBoard.RemovePiece(origin);
            GameBoard.AddPiece(piece, destination);
            piece.IncreaseMoveCount();
            TurnCount++;
            CurrentColor = (CurrentColor == EColor.Green) ? EColor.Red : EColor.Green;
        }
    }
}
