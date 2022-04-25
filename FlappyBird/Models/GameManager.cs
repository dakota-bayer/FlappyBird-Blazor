using System.ComponentModel;

namespace FlappyBird.Models
{
    public class GameManager : INotifyPropertyChanged
    {
        private const int Gravity = 1;

        public event PropertyChangedEventHandler? PropertyChanged;

        public BirdModel Bird { get; private set; }
        public PipeModel Pipe { get; private set; }
        public bool IsRunning { get; private set; } = false;

        public GameManager()
        {
            Bird = new BirdModel();
            Pipe = new PipeModel();
        }

        public async void MainLoop()
        {
            IsRunning = true;
            while (IsRunning)
            {
                Bird.Fall(Gravity);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bird)));

                if(Bird.DistanceFromGround <= 0)
                {
                    GameOver();
                }

                await Task.Delay(20); //This loop takes approx. 20 milliseconds
            }
        }

        public void StartGame()
        {

            if (!IsRunning)
            {
                Bird = new BirdModel();
                MainLoop();
            }

        }

        public void GameOver()
        {
            IsRunning = false;
        }
    }
}
