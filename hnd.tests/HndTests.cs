using model;
using Xunit;
using Xunit.Abstractions;

namespace hnd.tests
{
    public class HndTests
    {
        private static readonly int[,] PayoffMatrix = { { -25, 50 }, { 0, 15 } };

        [Fact]
        public void WhenAgentsFight_BothChangesFitness()
        {
            var field = new[,] { { new Cell(new Hawk()), new Cell(new Hawk()) } };
            var hnd = CreateEvolution(field);

            hnd.Tick();

            Assert.IsType<Hawk>(hnd.World.Field[0, 0].Agent);
            Assert.Equal(-25, hnd.World.Field[0, 0].Agent.Fitness);

            Assert.IsType<Hawk>(hnd.World.Field[0, 1].Agent);
            Assert.Equal(-25, hnd.World.Field[0, 1].Agent.Fitness);
        }

        private static Evolution CreateEvolution(Cell[,] field, int breedThreshold = 100)
        {
            return new Evolution(field, new RandomCellFinder(field), PayoffMatrix, breedThreshold, initialFitness: 0);
        }

        [Fact]
        public void WhenAgentGoesToEmptyCell_AgentMovesToCell()
        {
            var field = new[,] { { new Cell(new Hawk()), new Cell() } };
            var hnd = CreateEvolution(field);

            hnd.Tick();

            Assert.Null(hnd.World.Field[0, 0].Agent);
            Assert.IsType<Hawk>(hnd.World.Field[0, 1].Agent);
        }

        [Fact]
        public void WhenAgentFitnessBelowZero_AgentDies()
        {
            var field = new[,] { { new Cell(new Hawk()), new Cell(new Hawk()) } };

            var hnd = CreateEvolution(field);

            hnd.Tick();
            Assert.True(hnd.World.Field[0, 1].Agent.Fitness < 0);

            hnd.Tick();
            Assert.Null(hnd.World.Field[0, 1].Agent);
        }

        [Fact]
        public void WhenAgentFitnessAboveBreedThreshold_AgentBreeds()
        {
            var field = new[,] { { new Cell { Agent = new Hawk { Fitness = 100 } }, new Cell() } };

            var hnd = CreateEvolution(field);

            hnd.Tick();

            Assert.IsType<Hawk>(hnd.World.Field[0, 0].Agent);
            Assert.IsType<Hawk>(hnd.World.Field[0, 1].Agent);
        }

        [Fact]
        public void WhenAgentBreedsItsFitnessIsSplitEqualBetweenParentAndChild()
        {
            var field = new[,] { { new Cell { Agent = new Hawk { Fitness = 100 } }, new Cell() } };

            var hnd = CreateEvolution(field);

            hnd.Tick();

            var parent = hnd.World.Field[0, 0].Agent;
            var child = hnd.World.Field[0, 1].Agent;
            Assert.IsType<Hawk>(parent);
            Assert.Equal(50, parent.Fitness);
            Assert.IsType<Hawk>(child);
            Assert.Equal(50, child.Fitness);
        }

        [Fact]
        public void WhenAgentShouldBreedButDontHaveAvailableCellAgentDoesntBreed()
        {
            var field = new[,] { { new Cell { Agent = new Hawk { Fitness = 100 } }, new Cell(new Dove()) } };

            var hnd = CreateEvolution(field);

            hnd.Tick();

            Assert.IsType<Hawk>(hnd.World.Field[0, 0].Agent);
            Assert.IsType<Dove>(hnd.World.Field[0, 1].Agent);
        }

        [Fact]
        public void ReturnsInfo()
        {
            var field = new[,] { { new Cell(new Hawk()), new Cell(new Dove()) } };

            var hnd = CreateEvolution(field);

            decimal hawks, doves, fitness, density;
            hnd.GetInfo(out hawks, out doves, out fitness, out density);

            Assert.Equal(0.5m, hawks);
            Assert.Equal(0.5m, doves);
            Assert.Equal(0.0m, fitness);
            Assert.Equal(0.0m, density);
        }

        [Fact]
        public void ReturnsInfo2()
        {
            var field = new[,]
            {
                {new Cell {Agent = new Hawk()}, new Cell {Agent = new Dove()}},
                {new Cell {Agent = new Dove()}, new Cell {Agent = new Dove()}},
                {new Cell {Agent = new Hawk()}, new Cell {Agent = new Dove()}}
            };

            var hnd = CreateEvolution(field);
            
            decimal hawks, doves, fitness, density;
            hnd.GetInfo(out hawks, out doves, out fitness, out density);

            Assert.Equal(0.333m, hawks);
            Assert.Equal(0.667m, doves);
        }
    }
}