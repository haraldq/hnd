namespace model
{
    public abstract class Agent
    {
        public int Fitness { get; set; }
        public bool HasMoved { get; set; }
    }

    public class Dove : Agent
    {
    }
    public class Hawk : Agent
    {
    }
}