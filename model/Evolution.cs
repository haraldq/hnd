using System;

namespace model
{
    public class Evolution
    {
        private readonly int _breedThreshold;
        private readonly int[,] _payoffMatrix;
        private readonly Cellfinder _cellFinder;


        public Evolution(Cell[,] field, Cellfinder cellFinder,
            int[,] hawkAndDovePayoffMatrix, int breedThreshold = 1000, int initialFitness = 0)
        {
            World = new World(field);
            _cellFinder = cellFinder;

            _payoffMatrix = hawkAndDovePayoffMatrix;

            _breedThreshold = breedThreshold;

            SetInitialFitness(initialFitness);
        }

        private void SetInitialFitness(int initialFitness)
        {
            for (var x = 0; x < World.Field.GetLength(0); x++)
                for (var y = 0; y < World.Field.GetLength(1); y++)
                {
                    var agent = World.Field[x, y].Agent;
                    if (agent != null && agent.Fitness == 0)
                        agent.Fitness = initialFitness;
                }
        }

        public World World { get; }

        public void GetInfo(out decimal hawk, out decimal dove, out decimal totalFitnessPerAgent, out decimal density)
        {
            int nHawks = 0, nDoves = 0, nTotal = 0, nTotalFitness = 0;
            for (var x = 0; x < World.Field.GetLength(0); x++)
                for (var y = 0; y < World.Field.GetLength(1); y++)
                {
                    var agent = World.Field[x, y].Agent;
                    if (agent != null)
                    {
                        nTotal++;
                        nTotalFitness += agent.Fitness;
                        if (agent is Hawk)
                            nHawks++;
                        else if (agent is Dove)
                            nDoves++;
                    }
                }
            hawk = GetPercentage(nHawks, nTotal);
            dove = GetPercentage(nDoves, nTotal);
            totalFitnessPerAgent = GetPercentage(nTotalFitness, nTotal);
            density = GetPercentage(nHawks + nDoves, World.Field.Length);
        }

        private static decimal GetPercentage(int n, int tot)
        {
            return decimal.Round((decimal)n / tot, 3, MidpointRounding.AwayFromZero);
        }

        public void Tick()
        {
            ResetHasMoved();

            for (var row = 0; row < World.Field.GetLength(0); row++)
            {
                for (var column = 0; column < World.Field.GetLength(1); column++)
                {
                    var coordinate = new Coordinate(row, column);

                    var agent = World[coordinate].Agent;

                    if (agent == null)
                        continue;

                    if (agent.HasMoved)
                        continue;

                    if (agent.Fitness < 0)
                    {
                        Die(coordinate);
                        continue;
                    }

                    Coordinate newCoordinate;
                    _cellFinder.Pick(World.Field, coordinate, out newCoordinate);

                    if (newCoordinate == Coordinate.Empty)
                        continue;

                    var opponent = World[newCoordinate].Agent;

                    if (opponent != null)
                    {
                        Fight(agent, opponent);
                    }
                    else if (agent.Fitness >= _breedThreshold)
                    {
                        Breed(agent, coordinate);
                    }
                    else
                    {
                        MoveToCell(agent, coordinate, newCoordinate);
                    }
                }
            }
        }

        private void ResetHasMoved()
        {
            for (var x = 0; x < World.Field.GetLength(0); x++)
                for (var y = 0; y < World.Field.GetLength(1); y++)
                    if (World.Field[x, y].Agent != null)
                        World.Field[x, y].Agent.HasMoved = false;
        }

        private void Die(Coordinate coordinate)
        {
            World[coordinate].Agent = null;
        }

        private void Breed(Agent parent, Coordinate coordinate)
        {
            Coordinate newCoordinate;
            _cellFinder.PickWithoutAgent(World.Field, coordinate, out newCoordinate);

            if (newCoordinate == Coordinate.Empty)
                return;

            var child = CreateChild(parent);

            World[newCoordinate].Agent = child;
        }

        private static Agent CreateChild(Agent parent)
        {
            Agent child;

            if (parent is Hawk)
                child = new Hawk { HasMoved = true };
            else
                child = new Dove { HasMoved = true };

            DistributeFitness(parent, child);

            return child;
        }

        private static void DistributeFitness(Agent parent, Agent child)
        {
            var fitness = parent.Fitness / 2;
            parent.Fitness = fitness;
            child.Fitness = fitness;
        }

        private void MoveToCell(Agent agent, Coordinate coordinate, Coordinate newCoordinate)
        {
            World[coordinate].Agent = null;
            World[newCoordinate].Agent = agent;
            agent.HasMoved = true;
        }

        private void Fight(Agent agent, Agent opponent)
        {
            var agentIndex = agent is Hawk ? 0 : 1;
            var opponentIndex = opponent is Hawk ? 0 : 1;

            agent.Fitness += _payoffMatrix[agentIndex, opponentIndex];
            agent.HasMoved = true;
            opponent.Fitness += _payoffMatrix[opponentIndex, agentIndex];
            opponent.HasMoved = true;
        }
    }
}