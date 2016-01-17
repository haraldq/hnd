using System;
using model;
using Xunit;

namespace hnd.tests
{
    public class CellFinderTests
    {
        class TestableCellFinder : Cellfinder
        {
            public TestableCellFinder(Cell[,] field) : base(field)
            {

            }

            private Coordinate[] Neighbours(Coordinate coordinate)
            {
                return GetNeighboursWithAgent(coordinate);
            }
            public Coordinate[] Neighbours(int v1, int v2)
            {
                return Neighbours(new Coordinate(v1, v2));
            }

            public override void Pick(Cell[,] field, Coordinate coordinate, out Coordinate newCoordinate)
            {
                throw new System.NotImplementedException();
            }

            public override void PickWithoutAgent(Cell[,] field, Coordinate coordinate, out Coordinate newCoordinate)
            {
                throw new System.NotImplementedException();
            }

        }

        [Fact]
        public void GetNeighbours_Zero()
        {
            var field = new[,]
            {
                {new Cell(), new Cell (), new Cell ()},
                {new Cell(), new Cell {Agent = new Hawk()}, new Cell () },
                {new Cell(), new Cell(), new Cell()}
            };

            Assert.Equal(0, new TestableCellFinder(field).Neighbours(1, 1).Length);
        }

        [Fact]
        public void GetNeighbours_One()
        {
            var field = new[,]
            {
                {new Cell(), new Cell (), new Cell{Agent = new Hawk()}},
                {new Cell(), new Cell {Agent = new Hawk()}, new Cell () },
                {new Cell(), new Cell(), new Cell()}
            };

            Assert.Equal(1, new TestableCellFinder(field).Neighbours(1, 1).Length);
        }

        [Fact]
        public void RandomCellfinderPicksRandomCell()
        { }
        [Fact]
        public void GetNeighbours_Two()
        {
            var field = new[,]
            {
                {new Cell(), new Cell {Agent = new Hawk()}, new Cell{Agent = new Hawk()}},
                {new Cell(), new Cell {Agent = new Hawk()}, new Cell () },
                {new Cell(), new Cell(), new Cell()}
            };

            Assert.Equal(2, new TestableCellFinder(field).Neighbours(1, 1).Length);
        }

        [Fact]
        public void GetNeighbours_Three()
        {
            var field = new[,]
            {
                {new Cell{Agent = new Hawk()}, new Cell {Agent = new Hawk()}, new Cell{Agent = new Hawk()}},
                {new Cell(), new Cell {Agent = new Hawk()}, new Cell () },
                {new Cell(), new Cell(), new Cell()}
            };

            Assert.Equal(3, new TestableCellFinder(field).Neighbours(1, 1).Length);
        }

        [Fact]
        public void GetNeighbours_Four()
        {
            var field = new[,]
            {
                {new Cell{Agent = new Hawk()}, new Cell {Agent = new Hawk()}, new Cell{Agent = new Hawk()}},
                {new Cell(), new Cell {Agent = new Hawk()}, new Cell () },
                {new Cell(), new Cell(), new Cell{Agent = new Hawk()}}
            };

            Assert.Equal(4, new TestableCellFinder(field).Neighbours(1, 1).Length);
        }
        [Fact]
        public void GetNeighbours_Five()
        {
            var field = new[,]
            {
                {new Cell{Agent = new Hawk()}, new Cell {Agent = new Hawk()}, new Cell{Agent = new Hawk()}},
                {new Cell(), new Cell {Agent = new Hawk()}, new Cell () },
                {new Cell(), new Cell{Agent = new Hawk()}, new Cell{Agent = new Hawk()}}
            };

            Assert.Equal(5, new TestableCellFinder(field).Neighbours(1, 1).Length);
        }

        [Fact]
        public void GetNeighbours_Six()
        {
            var field = new[,]
            {
                {new Cell{Agent = new Hawk()}, new Cell {Agent = new Hawk()}, new Cell{Agent = new Hawk()}},
                {new Cell {Agent = new Dove()}, new Cell {Agent = new Hawk()}, new Cell () },
                {new Cell(), new Cell{Agent = new Hawk()}, new Cell{Agent = new Hawk()}}
            };

            Assert.Equal(6, new TestableCellFinder(field).Neighbours(1, 1).Length);
        }

        [Fact]
        public void GetNeighbours_Seven()
        {
            var field = new[,]
            {
                {new Cell{Agent = new Hawk()}, new Cell {Agent = new Hawk()}, new Cell{Agent = new Hawk()}},
                {new Cell {Agent = new Dove()}, new Cell {Agent = new Hawk()}, new Cell () },
                {new Cell{Agent = new Dove()}, new Cell{Agent = new Hawk()}, new Cell{Agent = new Hawk()}}
            };

            Assert.Equal(7, new TestableCellFinder(field).Neighbours(1, 1).Length);
        }

        [Fact]
        public void GetNeighbours_Eight()
        {
            var field = new[,]
            {
                {new Cell{Agent = new Hawk()}, new Cell {Agent = new Hawk()}, new Cell{Agent = new Hawk()}},
                {new Cell {Agent = new Dove()}, new Cell {Agent = new Hawk()}, new Cell {Agent = new Hawk()} },
                {new Cell{Agent = new Dove()}, new Cell{Agent = new Hawk()}, new Cell{Agent = new Hawk()}}
            };

            Assert.Equal(8, new TestableCellFinder(field).Neighbours(1, 1).Length);
        }
    }
}