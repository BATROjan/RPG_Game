using System.Net.Mime;
using BackPack;
using Bullet;
using DefaultNamespace.UI;
using DG.Tweening;
using Enemy;
using Gun;
using Player;
using UnityEngine;
using XMLSystem;

namespace GameController
{
    public class GameController
    {
        private readonly IXMLSystem _xmlSystem;
        private readonly BulletController _bulletController;
        private readonly BackPackController _backPackController;
        private readonly UIPlayingWindowController _uiPlayingWindowController;
        private readonly EnemyController _enemyController;
        private readonly GunController _gunController;
        private readonly PlayerController _playerController;

        public GameController(
            IXMLSystem xmlSystem,
            BulletController bulletController,
            BackPackController backPackController,
            UIPlayingWindowController uiPlayingWindowController,
            EnemyController enemyController,
            GunController gunController,
            PlayerController playerController)
        {
            _xmlSystem = xmlSystem;
            _bulletController = bulletController;
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
                _enemyController.Spawn(EnemyType.Zombie, i);
            }

            _gunController.Spawn(GunConfig.GunType.Makarov);
            _gunController.Spawn(GunConfig.GunType.AK74);
            _bulletController.SpawnBullet(10);
            _bulletController.SpawnBullet(5);
            _backPackController.Init();
            _uiPlayingWindowController.GetWindow().Buttons[2].OnClick += ExitGame;
        }

        public void ExitGame()
        {
            _xmlSystem.CreatXMLFile();
            if (_playerController.GetPlayerView())
            {
                _xmlSystem.SaveHealthToXML(_playerController.GetPlayerView().Heath.ToString());
            }
            _xmlSystem.SaveEnemyCountToXML(_enemyController.EnemyViews);
            _xmlSystem.SaveGunInBackPackToXML(_backPackController._listGuns);
            Application.Quit();
        }
    }
}