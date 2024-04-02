using Enemy;
using Player;

namespace GameController
{
    public class GameController
    {
        private readonly EnemyController _enemyController;
        private readonly PlayerController _playerController;

        public GameController(
            EnemyController enemyController,
            PlayerController playerController)
        {
            _enemyController = enemyController;
            _playerController = playerController;
            
            StartGame();
        }

        public void StartGame()
        {  
            _playerController.SpawnPlayer();
            for (int i = 0; i < 3; i++)
            {
                _enemyController.Spawn(EnemyType.Zombie);
            }
        }   
        
        public void StopGame()
        {
            
        }
    }
}