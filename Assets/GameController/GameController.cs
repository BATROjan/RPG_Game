using BackPack;
using DefaultNamespace.UI;
using DG.Tweening;
using Enemy;
using Gun;
using Player;

namespace GameController
{
    public class GameController
    {
        private readonly BackPackController _backPackController;
        private readonly UIPlayingWindowController _uiPlayingWindowController;
        private readonly EnemyController _enemyController;
        private readonly GunController _gunController;
        private readonly PlayerController _playerController;

        public GameController(
            BackPackController backPackController,
            UIPlayingWindowController uiPlayingWindowController,
            EnemyController enemyController,
            GunController gunController,
            PlayerController playerController)
        {
            _backPackController = backPackController;
            _uiPlayingWindowController = uiPlayingWindowController;
            _enemyController = enemyController;
            _gunController = gunController;
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

            _gunController.Spawn();
            _backPackController.Init();
        }

        public void StopGame()
        {
            
        }
    }
}