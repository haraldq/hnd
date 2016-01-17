using System;
using System.Linq;

namespace model
{
    public class RandomCellFinder : Cellfinder
    {
        public RandomCellFinder(Cell[,] field) : base(field)
        {
        }

        public override void Pick(Cell[,] field, Coordinate coordinate, out Coordinate newCoordinate)
        {
            var neighbours = GetAllNeighbouringCells(coordinate).ToArray();

            newCoordinate = PickRandom(neighbours);
        }

        private static Coordinate PickRandom(Coordinate[] neighbours)
        {
            //var random = new Random().Next(0, neighbours.Length);
            var random = RandomNumber.Between(0, neighbours.Length - 1);

            return neighbours[random];
        }

        public override void PickWithoutAgent(Cell[,] field, Coordinate coordinate, out Coordinate newCoordinate)
        {
            var neighbours = GetNeighboursWithoutAgent(coordinate).ToArray();

            if (neighbours.Length == 0)
            {
                newCoordinate = Coordinate.Empty;
                return;
            }
            newCoordinate = PickRandom(neighbours);
        }
    }
}