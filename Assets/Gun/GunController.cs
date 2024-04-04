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

        public GunView Spawn()
        {
            var gun = _gunViewPool.Spawn(_gunConfig.GetGunModelByType(GunConfig.GunType.Makarov));
            _dictionaryOfGunViews.Add(GunConfig.GunType.Makarov, gun);
            return gun;
        }
    }
}