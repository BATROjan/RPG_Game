using DefaultNamespace.UI;
using DG.Tweening;
using Enemy;
using Player;

namespace GameController
{
    public class GameController
    {
        private readonly UIPlayingWindowController _uiPlayingWindowController;
        private readonly EnemyController _enemyController;
        private readonly PlayerController _playerController;

        public GameController(
            UIPlayingWindowController uiPlayingWindowController,
            EnemyController enemyController,
            PlayerController playerController)
        {
            _uiPlayingWindowController = uiPlayingWindowController;
            _enemyController = enemyController;
            _playerController = playerController;
            
            StartGame();
        }

        public void StartGame()
        {  
            _uiPlayingWindowController.Spawn();
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