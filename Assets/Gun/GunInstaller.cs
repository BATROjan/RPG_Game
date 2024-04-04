using Zenject;

namespace Gun
{
    public class GunInstaller : Installer<GunInstaller>
    {
        public override void InstallBindings()
        {
            
            Container
                .Bind<GunConfig>()
                .FromScriptableObjectResource("GunConfig")
                .AsSingle()
                .NonLazy();
            
            Container
                .BindMemoryPool<GunView, GunView.Pool>()
                .FromComponentInNewPrefabResource("GunView");

            Container
                .Bind<GunController>()
                .AsSingle()
                .NonLazy();
        }
    }
}