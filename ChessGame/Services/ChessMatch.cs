using ChessGame.Entities;
using ChessGame.Entities.BoardLayer;
using ChessGame.Entities.ChessLayer;
using ChessGame.Entities.Enums;
using ChessGame.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Services
{
    internal class ChessMatch
    {
        private Board Board { get; set; }
        private int Turn { get; set; }
        private Color CurrentColor { get; set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentColor = Color.Green;
            StartGame();
        }

        private void StartGame()
        {
            Board board = new(8, 8);

            board.AddPiece(new Rook(Color.Red, board), new Position(0, 0));
            board.AddPiece(new Knight(Color.Red, board), new Position(0, 1));
            board.AddPiece(new Bishop(Color.Red, board), new Position(0, 2));
            board.AddPiece(new King(Color.Red, board), new Position(0, 3));
            board.AddPiece(new Queen(Color.Red, board), new Position(0, 4));
            board.AddPiece(new Bishop(Color.Red, board), new Position(0, 5));
            board.AddPiece(new Knight(Color.Red, board), new Position(0, 6));
            board.AddPiece(new Rook(Color.Red, board), new Position(0, 7));
            for (int i = 0; i < 8; i++)
            {
                board.AddPiece(new Pawn(Color.Red, board), new Position(1, i));
            }

            board.AddPiece(new Rook(Color.Green, board), new Position(7, 0));
            board.AddPiece(new Knight(Color.Green, board), new Position(7, 1));
            board.AddPiece(new Bishop(Color.Green, board), new Position(7, 2));
            board.AddPiece(new King(Color.Green, board), new Position(7, 3));
            board.AddPiece(new Queen(Color.Green, board), new Position(7, 4));
            board.AddPiece(new Bishop(Color.Green, board), new Position(7, 5));
            board.AddPiece(new Knight(Color.Green, board), new Position(7, 6));
            board.AddPiece(new Rook(Color.Green, board), new Position(7, 7));
            for (int i = 0; i < 8; i++)
            {
                board.AddPiece(new Pawn(Color.Green, board), new Position(6, i));
            }

            UI.ConsoleRenderer.RenderBoard(board);
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
            Turn++;
            CurrentColor = (CurrentColor == Color.Green) ? Color.Red : Color.Green;
        }
    }
}
