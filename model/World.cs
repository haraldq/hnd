namespace model
{
    public class World
    {
        public World(Cell[,] field)
        {
            Field = field;
        }

        public Cell[,] Field { get; }
        
        public Cell this[Coordinate c] => Field[c.Row, c.Column];
    }
}