using ChessGame.Entities.BoardLayer;
using ChessGame.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Entities.ChessLayer
{
    internal class Knight(Color color, Board board) : Piece(color, board)
    {
        public override string ToString()
        {
            return "♞";
        }
    }
}
