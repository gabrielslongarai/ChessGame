using ChessGame.Entities.Enums;
using ChessGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Entities
{
    internal class Piece
    {
        public string Icon { get; set; }
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveCount { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Position position, Color color, Board board, string icon)
        {
            Position = position;
            Color = color;
            Board = board;
            MoveCount = 0;
            Icon = icon;
        }

        public override string ToString()
        {
            return $"{Icon}";
        }


    }
}
