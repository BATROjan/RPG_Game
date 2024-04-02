using System.Collections.Generic;
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
            enemy.transform.position = GetRandomPosition(enemy);
            _dictEnemyViews.Add(enemy.GetInstanceID(), enemy);
            enemy.PlayerIsFound += ReadyToMoveForPlayer;

            return enemy;
        }

        private Vector3 GetRandomPosition(EnemyView enemyView)
        {
            Vector3 position = new Vector3(Random.Range(-10, 14), Random.Range(-5, 7), 0);
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
                }
            }
        }
    }
}