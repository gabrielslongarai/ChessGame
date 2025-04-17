using ChessGame.Application;
using ChessGame.Board;
using ChessGame.Board.Enums;
using ChessGame.Exceptions;
using System.Drawing;
using System.IO.Pipelines;



namespace ChessGame.Roles
{
    internal class ChessMatch
    {
        public GameBoard GameBoard { get; private set; }
        public int TurnCount { get; private set; }
        public EColor CurrentColor { get; private set; }
        public bool Finished { get; private set; }
        public HashSet<Piece> PiecesOnBoard { get; private set; }
        public HashSet<Piece> CapturedPieces { get; private set; }
        public bool Check { get; private set; }


        public ChessMatch()
        {
            GameBoard gameBoard = new(8, 8);
            GameBoard = gameBoard;
            TurnCount = 1;
            CurrentColor = EColor.Green;
            Finished = false;
            Check = false;
            PiecesOnBoard = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            SetupPieces();
        }

        public void SetupNewPiece(char column, int line, Piece piece)
        {
            GameBoard.AddPiece(piece, new ChessNotation(column, line).ToPosition());
            PiecesOnBoard.Add(piece);
        }

        private void SetupPieces()
        {
            /*
            SetupNewPiece('a', 1, new Rook(EColor.Green, GameBoard));
            SetupNewPiece('b', 1, new Knight(EColor.Green, GameBoard));
            SetupNewPiece('c', 1, new Bishop(EColor.Green, GameBoard));
            SetupNewPiece('d', 1, new King(EColor.Green, GameBoard));
            SetupNewPiece('e', 1, new Queen(EColor.Green, GameBoard));
            SetupNewPiece('f', 1, new Bishop(EColor.Green, GameBoard));
            SetupNewPiece('g', 1, new Knight(EColor.Green, GameBoard));
            SetupNewPiece('h', 1, new Rook(EColor.Green, GameBoard));
            for (int i = 0; i < 8; i++)
            {
               SetupNewPiece((char)('a' + i), 2, new Pawn(EColor.Green, GameBoard));
            }

            SetupNewPiece('a', 8, new Rook(EColor.Red, GameBoard));
            SetupNewPiece('b', 8, new Knight(EColor.Red, GameBoard));
            SetupNewPiece('c', 8, new Bishop(EColor.Red, GameBoard));
            SetupNewPiece('d', 8, new King(EColor.Red, GameBoard));
            SetupNewPiece('e', 8, new Queen(EColor.Red, GameBoard));
            SetupNewPiece('f', 8, new Bishop(EColor.Red, GameBoard));
            SetupNewPiece('g', 8, new Knight(EColor.Red, GameBoard));
            SetupNewPiece('h', 8, new Rook(EColor.Red, GameBoard));
            for (int i = 0; i < 8; i++)
            {
              SetupNewPiece((char)('a' + i), 7, new Pawn(EColor.Red, GameBoard));
            }
            */
            SetupNewPiece('d', 1, new King(EColor.Green, GameBoard));
            SetupNewPiece('c', 1, new Rook(EColor.Green, GameBoard));
            SetupNewPiece('h', 7, new Rook(EColor.Green, GameBoard));

            SetupNewPiece('b', 8, new Rook(EColor.Red, GameBoard));
            SetupNewPiece('h', 8, new Rook(EColor.Red, GameBoard));
            SetupNewPiece('a', 8, new King(EColor.Red, GameBoard));


        }

        public bool[,] GetPossibleMoves(Position origin)
        {
            if (GameBoard.HasPiece(origin) == false)
            {
                throw new BoardExceptions("\nNo piece at this position.");
            }

            bool[,] possibleMoves = GameBoard.GetPiece(origin).PossibleMoves();
            return possibleMoves;
        }

        private void ValidateMove(Position origin, Position destination)
        {
            if (!GameBoard.IsValidPosition(origin))
            {
                throw new BoardExceptions("\nInvalid origin position!");
            }
            if (GameBoard.GetPiece(origin) == null)
            {
                throw new BoardExceptions("\nNo piece at the origin position!");
            }
            if (GameBoard.GetPiece(origin).Color != CurrentColor)
            {
                throw new BoardExceptions("\nIt's not your turn!");
            }
            if (!GameBoard.GetPiece(origin).IsThereAnyPossibleMove())
            {
                throw new BoardExceptions("\nNo possible moves for this piece!");
            }
            if (!GameBoard.IsValidPosition(destination))
            {
                throw new BoardExceptions("\nInvalid destination position!");
            }
            if (origin.Line == destination.Line && origin.Column == destination.Column)
            {
                throw new BoardExceptions("\nOrigin and destination positions are the same!");
            }
            if (GameBoard.HasPiece(destination) && GameBoard.GetPiece(destination).Color == CurrentColor)
            {
                throw new BoardExceptions("\nYou cannot capture your own piece!");
            }
            //if (!GameBoard.GetPiece(origin).PossibleMoves()[destination.Line, destination.Column])
            //{
            //  throw new BoardExceptions("\nInvalid move for this piece!");
            //}
        }

        private Piece? Move(Position origin, Position destination)
        {
            Piece piece = GameBoard.GetPiece(origin);

            Piece? capturedPiece = CapturePiece(destination);

            GameBoard.RemovePiece(origin);
            GameBoard.AddPiece(piece, destination);
            piece.IncreaseMoveCount();

            return capturedPiece;
        }

        private Piece? CapturePiece(Position position)
        {
            if (GameBoard.HasPiece(position))
            {
                Piece capturedPiece = GameBoard.RemovePiece(position);
                CapturedPieces.Add(capturedPiece);
                return capturedPiece;
            }

            return null;
        }

        private void UndoMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece piece = GameBoard.RemovePiece(destination);
            piece.DecreaseMoveCount();

            if (capturedPiece != null)
            {
                GameBoard.AddPiece(capturedPiece, destination);
                CapturedPieces.Remove(capturedPiece);
            }

            GameBoard.AddPiece(piece, origin);
        }

        public void PerformPlay(Position origin, Position destination)
        {
            ValidateMove(origin, destination);

            Piece? capturedPiece = Move(origin, destination);

            Piece piece = GameBoard.GetPiece(destination);

            if (IsInCheck(piece.Color))
            {
                UndoMove(origin, destination, capturedPiece);
                throw new BoardExceptions("\nYou cannot put yourself in check!");
            }

            Check = IsInCheck(GetOpponentColor(piece.Color));

            if (IsCheckMate(GetOpponentColor(piece.Color)))
            {
                Finished = true;
            }
            else
            {
                ChangeTurn();
            }

        }

        private void ChangeTurn()
        {
            TurnCount++;
            CurrentColor = (CurrentColor == EColor.Green) ? EColor.Red : EColor.Green;
        }

        public HashSet<Piece> GetCapturedPieces(EColor color)
        {
            HashSet<Piece> pieces = new HashSet<Piece>();
            foreach (Piece piece in CapturedPieces)
            {
                if (piece.Color == color)
                {
                    pieces.Add(piece);
                }
            }
            return pieces;
        }

        private HashSet<Piece> GetPiecesOnBoard(EColor color)
        {
            HashSet<Piece> pieces = new HashSet<Piece>();
            foreach (Piece piece in PiecesOnBoard)
            {
                if (piece.Color == color)
                {
                    pieces.Add(piece);
                }
            }
            pieces.ExceptWith(GetCapturedPieces(color));
            return pieces;
        }

        public bool IsInCheck(EColor color)
        {
            Piece king = GetKing(color);

            foreach (Piece piece in GetPiecesOnBoard(GetOpponentColor(color)))
            {
                bool[,] possibleMoves = piece.PossibleMoves();
                if (possibleMoves[king.Position.Line, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsCheckMate(EColor color)
        {
            if (IsInCheck(color) == false)
            {
                return false;
            }

            foreach (Piece piece in GetPiecesOnBoard(color))
            {
                bool[,] possibleMoves = piece.PossibleMoves();

                for (int x = 0; x < GameBoard.Lines; x++)
                {
                    for (int y = 0; y < GameBoard.Columns; y++)
                    {
                        if (possibleMoves[x, y] == true)
                        {
                            Position origin = piece.Position;
                            Position destination = new(x, y);
                            Piece capturedPiece = Move(origin, destination);
                            bool isInCheck = IsInCheck(color);
                            UndoMove(origin, destination, capturedPiece);

                            if (isInCheck == false)
                            {
                                return false;
                            }
                        }

                    }
                }
            }

            return true;
        }

        private Piece GetKing(EColor color)
        {
            foreach (Piece piece in GetPiecesOnBoard(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        private EColor GetOpponentColor(EColor color)
        {
            return (color == EColor.Green) ? EColor.Red : EColor.Green;
        }
    }
}
