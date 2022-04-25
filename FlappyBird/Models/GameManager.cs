namespace FlappyBird.Models
{
    public class GameManager
    {
        private const int Gravity = 1;

        public event EventHandler MainLoopCompleted;

        public BirdModel Bird { get; private set; }
        public List<PipeModel> Pipes { get; private set; }
        public bool IsRunning { get; private set; } = false;

        public GameManager()
        {
            Bird = new BirdModel();
            Pipes = new List<PipeModel>();
        }

        public async void MainLoop()
        {
            IsRunning = true;
            while (IsRunning)
            {
                MoveObjects();
                CheckForCollisions();
                ManagePipes();

                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20); //This loop takes approx. 20 milliseconds
            }
        }



        public void StartGame()
        {

            if (!IsRunning)
            {
                Bird = new BirdModel();
                Pipes = new List<PipeModel>();
                MainLoop();
            }

        }

        public void Jump()
        {
            if (IsRunning)
            {
                Bird.Jump();
            }
        }

        void MoveObjects()
        {
            Bird.Fall(Gravity);

            foreach (var pipe in Pipes)
            {
                pipe.Move();
            }
        }

        void CheckForCollisions()
        {
            if (Bird.IsOnGround())
            {
                GameOver();
            }
        }

        private void ManagePipes()
        {
            if(!Pipes.Any() || Pipes.Last().DistanceFromLeft <= 250)
            {
                Pipes.Add(new PipeModel());
            }

            if (Pipes.First().IsOffScreen())
            {
                Pipes.Remove(Pipes.First());
            }
        }

        public void GameOver()
        {
            IsRunning = false;
        }
    }
}
