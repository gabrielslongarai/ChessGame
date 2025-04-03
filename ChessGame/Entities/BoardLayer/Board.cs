using ChessGame.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Entities.BoardLayer
{
    internal class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        public Piece[,] Pieces { get; private set; }

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines, columns];
        }

        public void AddPiece(Piece piece, Position position)
        {
            if(HasPiece(position))
            {
                throw new BoardExceptions("There is already a piece in this position!");
            }
            Pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece GetPiece(Position position)
        {
            ValidatePosition(position);
            return Pieces[position.Line, position.Column];
        }

        public Piece GetPiece(int line, int column)
        {
            Position position = new Position(line, column);
            ValidatePosition(position);
            return Pieces[position.Line, position.Column];
        }

        public bool HasPiece(Position position)
        {
            ValidatePosition(position);
            return GetPiece(position) != null;
        }

        public bool IsValidPosition(Position position)
        {
            if (position.Line < 0 || position.Line >= Lines || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!IsValidPosition(position))
            {
                throw new BoardExceptions("Invalid position!");
            }
        }
    }
}
