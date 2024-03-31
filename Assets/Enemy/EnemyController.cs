using System.Collections.Generic;

namespace Enemy
{
    public class EnemyController
    {
        private readonly EnemyConfig _enemyConfig;
        private readonly EnemyView.Pool _enemyPool;

        private Dictionary<int, EnemyView> _dictEnemyViews = new Dictionary<int, EnemyView>();

        public EnemyController(
            EnemyConfig enemyConfig, 
            EnemyView.Pool enemyPool)
        {
            _enemyConfig = enemyConfig;
            _enemyPool = enemyPool;
        }

        public EnemyView Spawn(EnemyType type)
        {
            var enemy = _enemyPool.Spawn(_enemyConfig.GetEnemyModelByType(type));
            _dictEnemyViews.Add(enemy.GetInstanceID(), enemy);

            return enemy;
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
    }
}