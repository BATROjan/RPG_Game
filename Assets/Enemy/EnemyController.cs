using System.Collections.Generic;
using Bullet;
using DG.Tweening;
using Player;
using UnityEngine;
using XMLSystem;
using Zenject;

namespace Enemy
{
    public class EnemyController: ITickable
    {
        public List<EnemyView> EnemyViews = new List<EnemyView>();
        private readonly BulletController _bulletController;
        private readonly IXMLSystem _xmlSystem;
        private readonly TickableManager _tickableManager;
        private readonly EnemyConfig _enemyConfig;
        private readonly EnemyView.Pool _enemyPool;

        private Dictionary<int, EnemyView> _dictEnemyViews = new Dictionary<int, EnemyView>();

        private PlayerView _playerView;
        private bool isFind;
        private bool isReadyToAtack = true;
        public EnemyController(
            BulletController bulletController,
            IXMLSystem xmlSystem,
            TickableManager tickableManager,
            EnemyConfig enemyConfig, 
            EnemyView.Pool enemyPool)
        {
            _bulletController = bulletController;
            _xmlSystem = xmlSystem;
            _tickableManager = tickableManager;
            _enemyConfig = enemyConfig;
            _enemyPool = enemyPool;
            
            _tickableManager.Add(this);
        }

        public EnemyView Spawn(EnemyType type, int id)
        {
            var enemy = _enemyPool.Spawn(_enemyConfig.GetEnemyModelByType(type));
            enemy.transform.position = GetRandomPosition();
            _dictEnemyViews.Add(enemy.GetInstanceID(), enemy);
            EnemyViews.Add(enemy);
            enemy.PlayerIsFound += ReadyToMoveForPlayer;
            enemy.OnDead += DeadLogic;
            if (_xmlSystem.LoadFromXML(enemy.EnemyType.ToString()+ id.ToString(), "health") != null)
            {
                enemy.Health = int.Parse(_xmlSystem.LoadFromXML(enemy.EnemyType.ToString()+ id.ToString(), "health"));
                float percent = (float)enemy.Health / enemy.MaxHealth;
                enemy.HealthImage.fillAmount = percent;
            }
            return enemy;
        }

        private Vector3 GetRandomPosition( )
        {
            Vector3 position = new Vector3(Random.Range(-11, 20), Random.Range(0, 12), 0);
            return position;
        }

        private void ReadyToMoveForPlayer(PlayerView playerView)
        {
            isFind = !isFind;
            _playerView = playerView;
        }

        public EnemyView GetEnemyViewByID(int id)
        {
            EnemyView enemy= new EnemyView();
            foreach (var item in _dictEnemyViews)
            {
                if (item.Key == id)
                { 
                    enemy = item.Value;
                }
            }
            return enemy;
        }

        private void DeadLogic(EnemyView enemyView)
        {
            var buff = _bulletController.SpawnBullet(5);
            buff.transform.position = enemyView.transform.position;
            _dictEnemyViews.Remove(enemyView.GetInstanceID());
            EnemyViews.Remove(enemyView);
            _enemyPool.Despawn(enemyView);
        }

        public void Tick()
        {
            foreach (var enemy in _dictEnemyViews)
            {
                if (enemy.Value.IsFound)
                {
                    if (Vector3.Distance(enemy.Value.transform.position, _playerView.transform.position) > 0.57f)
                    {
                        enemy.Value.transform.position = Vector3.MoveTowards(enemy.Value.transform.position, _playerView.transform.position, enemy.Value.Speed);
                    }
                    else
                    {
                        if (enemy.Value.IsReadyToAttack)
                        {
                            Attack(enemy.Value);
                        }
                    }
                }
            }
        }

        private void Attack(EnemyView enemy)
        {
            _dictEnemyViews[enemy.GetInstanceID()].IsReadyToAttack = false;
            _playerView.healthImage.fillAmount -= enemy.Damage*0.01f;
            _playerView.Heath -= enemy.Damage;
            if (_playerView.Heath <= 0)
            {
                _playerView.OnDead?.Invoke(_playerView);
            }
            DOVirtual.DelayedCall(2, () => _dictEnemyViews[enemy.GetInstanceID()].IsReadyToAttack = true);
        }
    }
}