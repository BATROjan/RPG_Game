using Zenject;

namespace Bullet
{
    public class BulletInstaller : Installer<BulletInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<BulletConfig>()
                .FromScriptableObjectResource("BulletConfig")
                .AsSingle()
                .NonLazy();
            
            Container
                .BindMemoryPool<BulletView, BulletView.Pool>()
                .FromComponentInNewPrefabResource("BulletView");

            Container
                .Bind<BulletController>()
                .AsSingle()
                .NonLazy();
        }
    }
}