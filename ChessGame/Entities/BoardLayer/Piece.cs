using ChessGame.Entities.Enums;
using ChessGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Entities.BoardLayer
{
    internal class Piece(Color color, Board board)
    {
        public Position? Position { get; set; } = null;
        public Color Color { get; protected set; } = color;
        public int MoveCount { get; protected set; } = 0;
        public Board Board { get; protected set; } = board;

        public override string ToString()
        {
            return "◼";
        }
    }
}
