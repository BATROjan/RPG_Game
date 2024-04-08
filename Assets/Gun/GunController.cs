using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class GunController
    {
        public List<GunView> GunViews= new List<GunView>();
        private readonly GunConfig _gunConfig;
        private readonly GunView.Pool _gunViewPool;

        private Dictionary<GunConfig.GunType, GunView> _dictionaryOfGunViews =
            new Dictionary<GunConfig.GunType, GunView>();
        public GunController(GunConfig gunConfig,
            GunView.Pool gunViewPool)
        {
            _gunConfig = gunConfig;
            _gunViewPool = gunViewPool;
        }

        public GunView Spawn(GunConfig.GunType type)
        {
            var gun = _gunViewPool.Spawn(_gunConfig.GetGunModelByType(type));
            gun.transform.position = GetRandomPosition();
            _dictionaryOfGunViews.Add(gun.GetGunType(), gun);
            GunViews.Add(gun);
            return gun;
        }

        public GunView GetViewByType(GunConfig.GunType type)
        {
            return _dictionaryOfGunViews[type];
        }

        private Vector3 GetRandomPosition( )
        {
            Vector3 position = new Vector3(Random.Range(-11, 20), Random.Range(0, 12), 0);
            return position;
        }

        public void Despawn(GunView gunView)
        {
            _gunViewPool.Despawn(gunView);
        }
    }
}