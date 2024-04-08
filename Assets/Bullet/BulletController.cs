using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    public class BulletController
    {
        private readonly BulletConfig _bulletConfig;
        private readonly BulletView.Pool _pool;

        private Dictionary<int, BulletView> _dictionary = new Dictionary<int, BulletView>();
        public BulletController(
            BulletConfig bulletConfig, 
            BulletView.Pool pool)
        {
            _bulletConfig = bulletConfig;
            _pool = pool;
        }

        public BulletView SpawnBullet(int ID)
        {
            var bulletView = _pool.Spawn(_bulletConfig.GetBulletModelByID(ID));
            bulletView.transform.position = GetRandomPosition();
            if (!_dictionary.ContainsKey(bulletView.GetBulletCount()))
            {
                _dictionary.Add(bulletView.GetBulletCount(), bulletView);
            }
            return bulletView;
        }

        public BulletView SpawnCustomBullet(int count)
        {
            var bulletview = _pool.Spawn(_bulletConfig.GetBulletModelByID(1));
            bulletview.CorrectBulletCount(count - 1);
            if (!_dictionary.ContainsKey(bulletview.GetBulletCount()))
            {
                _dictionary.Add(bulletview.GetBulletCount(),bulletview);
            }
            return bulletview;
        }
        
        private Vector3 GetRandomPosition( )
        {
            Vector3 position = new Vector3(Random.Range(-11, 20), Random.Range(0, 12), 0);
            return position;
        }

        public void Despawn(BulletView bulletView)
        {
            _pool.Despawn(bulletView);
        }
    }
}