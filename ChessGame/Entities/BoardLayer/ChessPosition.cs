using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Entities.BoardLayer
{
    internal class ChessPosition
    {
        public char Line { get; set; }
        public int Column { get; set; }

        public ChessPosition(char line, int column)
        {
            Line = line;
            Column = column;
        }

        public Position ToPosition()
        {
            return new Position(8 - Column, Line - 'a');
        }

        public override string ToString()
        {
            return $"{Line}{Column}";
        }
    }
}
