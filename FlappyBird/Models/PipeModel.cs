namespace FlappyBird.Models
{
    public class PipeModel
    {
        public int DistanceFromLeft { get; private set; } = 500;
        public int DistanceFromBottom { get; private set; } = new Random().Next(0, 60);
        public int Speed { get; set; } = 2;

        public void Move()
        {
            DistanceFromLeft -= Speed;
        }

        public bool IsOffScreen()
        {
            return DistanceFromLeft <= -60;
        }
    }
}
