using Zenject;

namespace Enemy
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<EnemyConfig>()
                .FromScriptableObjectResource("EnemyConfig")
                .AsSingle()
                .NonLazy();
            
            Container
                .BindMemoryPool<EnemyView, EnemyView.Pool>()
                .FromComponentInNewPrefabResource("ZombieView");

            Container
                .Bind<EnemyController>()
                .AsSingle()
                .NonLazy();
        }
    }
}