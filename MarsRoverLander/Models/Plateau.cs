namespace MarsRoverLander.Models
{
    public class Plateau
    {
        public Plateau(int x, int y)
        {
            X = x;
            Y = y;
            TotalGridCount = (X + 1) * (Y + 1);
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int TotalGridCount { get; private set; }
    }
}
