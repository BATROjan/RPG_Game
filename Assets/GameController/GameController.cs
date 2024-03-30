using Player;

namespace GameController
{
    public class GameController
    {
        private readonly PlayerController _playerController;

        public GameController(PlayerController playerController)
        {
            _playerController = playerController;
            
            StartGame();
        }

        public void StartGame()
        {  
            _playerController.SpawnPlayer();
        }   
        
        public void StopGame()
        {
            
        }
    }
}