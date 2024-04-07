using System.Collections.Generic;
using BackPack;
using Bullet;
using DefaultNamespace.UI;
using DG.Tweening;
using Enemy;
using Gun;
using UnityEngine;
using XMLSystem;

namespace Player
{
    public class PlayerController
    {
        private readonly IXMLSystem _xmlSystem;
        private readonly BackPackController _backPackController;
        private readonly UIPlayingWindowController _uiPlayingWindowController;
        private readonly PlayerView.Pool _playerPool;
        private readonly PlayerConfig _playerConfig;

        private PlayerView _playerView;
        private GunView _gunView;
        private EnemyView currentEnemy;
        private bool _isReadyToFire;
        
        private List<EnemyView> _enemyViews = new List<EnemyView>();
        
        private UIPlayingWindow _uiPlayingWindow;
        public PlayerController(
            IXMLSystem xmlSystem,
            BackPackController backPackController,
            UIPlayingWindowController uiPlayingWindowController,
            PlayerView.Pool playerPool,
            PlayerConfig playerConfig)
        {
            _xmlSystem = xmlSystem;
            _backPackController = backPackController;
            _uiPlayingWindowController = uiPlayingWindowController;
            _playerPool = playerPool;
            _playerConfig = playerConfig;
        }

        public PlayerView SpawnPlayer()
        {
            _playerView = _playerPool.Spawn(_playerConfig.GetPlayerModel());
            _playerView.transform.position = new Vector3(-15, -8, 0);
            _uiPlayingWindowController.GetWindow().Buttons[0].OnClick += Fire;
            _playerView.OnTakeGun += TakeGun;
            _playerView.OnTakeBullet += TakeBullet;
            _playerView.OnFindEnemy += AddEnemy;
            _playerView.OnLoseEnemy += LoseEnemy;
            _playerView.OnDead += DeadLogic;
            _backPackController.OnChangeGun += ChangeGun;
            if (_xmlSystem.LoadFromXML("PlayerHealth", "value") != null)
            {
                _playerView.Heath = int.Parse(_xmlSystem.LoadFromXML("PlayerHealth", "value"));
                _playerView.healthImage.fillAmount = _playerView.Heath / 100;
            }

            return _playerView;
        }
        private void DeadLogic(PlayerView playerView)
        {
            _playerView.OnTakeGun -= TakeGun;
            _playerView.OnTakeBullet -= TakeBullet;
            _playerView.OnFindEnemy -= AddEnemy;
            _playerView.OnLoseEnemy -= LoseEnemy;
            _playerView.OnDead -= DeadLogic;
            _backPackController.OnChangeGun -= ChangeGun;
            _playerView = null;
            _playerPool.Despawn(playerView);
        }

        private void LoseEnemy(EnemyView enemy)
        {
            _enemyViews.Remove(enemy);
            if (_enemyViews.Count == 0)
            {
                currentEnemy = null;
            }
        }

        private void AddEnemy(EnemyView enemy)
        {
            if (_enemyViews.Count == 0)
            {
                currentEnemy = enemy;
            }
            _enemyViews.Add(enemy);
        }

        private void ChangeGun(GunView gun)
        {
            _gunView = gun;
            if (gun)
            {
                _playerView.GunSprite.sprite = _gunView.GetGunImage();
                _isReadyToFire = true;
            }
            else
            {
                _playerView.GunSprite.sprite = null;
                _isReadyToFire = false;
            }
        }

        private void TakeGun(GunView gunView)
        {
            _backPackController.TakeGunInBackPack(gunView);
            if (!_playerView.GunSprite.sprite)
            {
                _gunView = gunView;
                _isReadyToFire = true;
                _playerView.GunSprite.sprite = _gunView.GetGunImage();
            }
        }

        private void TakeBullet(BulletView bulletView)
        {
            _backPackController.TakeBulletInBackPack(bulletView);
        }

        public PlayerView GetPlayerView()
        {
            return _playerView;
        }

        public void Fire()
        {
            if (_isReadyToFire)
            {
                if (currentEnemy)
                {
                    if (_backPackController.SpendBullet())
                    {
                        _isReadyToFire = false;
                        currentEnemy.Health -= _gunView.GetGunDamage();
                        float percent = (float)currentEnemy.Health / currentEnemy.MaxHealth;
                        currentEnemy.HealthImage.fillAmount = percent;
                        if (currentEnemy.Health<=0)
                        {
                            currentEnemy.OnDead?.Invoke(currentEnemy);
                        }
                        _isReadyToFire = true;
                    }
                }
            }
            
        }
    }
}