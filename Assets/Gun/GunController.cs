using System.Collections.Generic;

namespace Gun
{
    public class GunController
    {
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
            _dictionaryOfGunViews.Add(gun.GetGunType(), gun);
            return gun;
        }

        public void Despawn(GunView gunView)
        {
            _gunViewPool.Despawn(gunView);
        }
    }
}