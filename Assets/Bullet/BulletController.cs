using System.Collections.Generic;

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
            _dictionary.Add(bulletView.GetBulletCount(), bulletView);
            return bulletView;
        }
    }
}