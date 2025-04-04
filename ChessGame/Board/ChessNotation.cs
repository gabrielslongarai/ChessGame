namespace ChessGame.Board
{
    internal class ChessNotation(char column, int line)
    {
        public char Column { get; set; } = column;
        public int Line { get; set; } = line;

        public Position ToPosition()
        {
            return new Position(8 - Line, Column - 'a');
        }

        public override string ToString()
        {
            return $"{Line}{Column}";
        }
    }
}
