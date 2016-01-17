namespace model
{
    public class Cell
    {
        public Cell()
        {
        }

        public Cell(Agent agent)
        {
            Agent = agent;
        }

        public Agent Agent { get; set; }
    }
}