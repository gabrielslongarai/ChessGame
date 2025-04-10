﻿using ChessGame.Board;
using ChessGame.Board.Enums;
using ChessGame.Exceptions;
using ChessGame.Application;



namespace ChessGame.Roles
{
    internal class ChessMatch
    {
        public GameBoard GameBoard { get; private set; }
        public int TurnCount { get; private set; }
        public EColor CurrentColor { get; private set; }
        public bool Finished { get; set; }
        private HashSet<Piece> PiecesOnBoard { get; set; }
        private HashSet<Piece> CapturedPieces { get; set; }


        public ChessMatch()
        {
            GameBoard gameBoard = new(8, 8);
            GameBoard = gameBoard;
            TurnCount = 1;
            CurrentColor = EColor.Green;
            Finished = false;
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
        }

        internal void ValidateOriginPosition(Position origin)
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
        }

        public void ValidateMove(Position origin, Position destination)
        {
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
        }

        internal void MovePiece(Position origin, Position destination)
        {
            ValidateOriginPosition(origin);
            ValidateMove(origin, destination);

            Piece piece = GameBoard.GetPiece(origin);

            CapturePiece(destination);

            GameBoard.RemovePiece(origin);
            GameBoard.AddPiece(piece, destination);
            ChangeTurn(piece);
        }

        private void CapturePiece(Position position)
        {
            if (GameBoard.HasPiece(position) && GameBoard.GetPiece(position).Color != CurrentColor)
            {
                Piece capturedPiece = GameBoard.RemovePiece(position);
                CapturedPieces.Add(capturedPiece);
            }
        }

        private HashSet<Piece> GetCapturedPieces(EColor color)
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

        private void ChangeTurn(Piece piece)
        {
            piece.IncreaseMoveCount();
            TurnCount++;
            CurrentColor = (CurrentColor == EColor.Green) ? EColor.Red : EColor.Green;
        }
    }
}
