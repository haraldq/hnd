namespace model
{
    public class Coordinate
    {
        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }

        public static Coordinate Empty = new Coordinate(-1, -1);
    }

}