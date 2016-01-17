using System.Collections.Generic;
using System.Linq;

namespace model
{


    public abstract class Cellfinder
    {
        private readonly Cell[,] field;

        protected Cellfinder(Cell[,] field)
        {
            this.field = field;
        }

        protected Coordinate[] GetNeighboursWithAgent(Coordinate coordinate)
        {
            var neighbours = WithAgent(GetAllNeighbouringCells(coordinate));

            return neighbours.ToArray();
        }
        protected Coordinate[] GetNeighboursWithoutAgent(Coordinate coordinate)
        {
            var neighbours = WithoutAgent(GetAllNeighbouringCells(coordinate));

            return neighbours.ToArray();
        }

        private List<Coordinate> WithAgent(IEnumerable<Coordinate> coordinates)
        {
            return (from coordinate in coordinates
                let cell = field[coordinate.Row, coordinate.Column]
                where cell.Agent != null
                select coordinate).ToList();
        }

        private List<Coordinate> WithoutAgent(IEnumerable<Coordinate> coordinates)
        {
            return (from coordinate in coordinates
                    let cell = field[coordinate.Row, coordinate.Column]
                    where cell.Agent == null
                    select coordinate).ToList();
        }

        public List<Coordinate> GetAllNeighbouringCells(Coordinate coordinate)
        {
            var rowLength = field.GetLength(0);
            var columnLength = field.GetLength(1);
            var row = coordinate.Row;
            var column = coordinate.Column;


            var neighbours = new List<Coordinate>();

            // up
            if (row - 1 >= 0)
            {
                neighbours.Add(new Coordinate(row - 1, column));
            }

            // up right
            if (row - 1 >= 0 && column + 1 < columnLength)
            {
                neighbours.Add(new Coordinate(row - 1, column + 1));
            }

            // right
            if (column + 1 < columnLength)
            {
                neighbours.Add(new Coordinate(row, column + 1));
            }
            // down right
            if (column + 1 < columnLength && row + 1 < rowLength)
            {
                neighbours.Add(new Coordinate(row + 1, column + 1));
            }
            //down
            if (row + 1 < rowLength)
            {
                neighbours.Add(new Coordinate(row + 1, column));
            }
            // down left
            if (row + 1 < rowLength && column - 1 >= 0)
            {
                neighbours.Add(new Coordinate(row + 1, column - 1));
            }
            //left
            if (column - 1 >= 0 && field[row, column - 1].Agent != null)
            {
                neighbours.Add(new Coordinate(row, column - 1));
            }
            // left up
            if (column - 1 >= 0 && row - 1 >= 0)
            {
                neighbours.Add(new Coordinate(row - 1, column - 1));
            }
            return neighbours;
        }


        public abstract void Pick(Cell[,] field, Coordinate coordinate, out Coordinate newCoordinate);
        public abstract void PickWithoutAgent(Cell[,] field, Coordinate coordinate, out Coordinate newCoordinate);

    }
}