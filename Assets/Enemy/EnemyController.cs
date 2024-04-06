using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyController: ITickable
    {
        private readonly TickableManager _tickableManager;
        private readonly EnemyConfig _enemyConfig;
        private readonly EnemyView.Pool _enemyPool;

        private Dictionary<int, EnemyView> _dictEnemyViews = new Dictionary<int, EnemyView>();

        private PlayerView _playerView;
        private bool isFind;
        private bool isReadyToAtack = true;
        public EnemyController(
            TickableManager tickableManager,
            EnemyConfig enemyConfig, 
            EnemyView.Pool enemyPool)
        {
            _tickableManager = tickableManager;
            _enemyConfig = enemyConfig;
            _enemyPool = enemyPool;
            
            _tickableManager.Add(this);
        }

        public EnemyView Spawn(EnemyType type)
        {
            var enemy = _enemyPool.Spawn(_enemyConfig.GetEnemyModelByType(type));
            enemy.transform.position = GetRandomPosition();
            _dictEnemyViews.Add(enemy.GetInstanceID(), enemy);
            enemy.PlayerIsFound += ReadyToMoveForPlayer;

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
            DOVirtual.DelayedCall(2, () => _dictEnemyViews[enemy.GetInstanceID()].IsReadyToAttack = true);
        }
    }
}