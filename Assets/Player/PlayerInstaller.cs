using Zenject;

namespace Player
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerConfig>()
                .FromScriptableObjectResource("PlayerConfig")
                .AsSingle()
                .NonLazy();
            
            Container
                .BindMemoryPool<PlayerView, PlayerView.Pool>()
                .FromComponentInNewPrefabResource("PlayerView");

            Container
                .Bind<PlayerController>()
                .AsSingle()
                .NonLazy();
        }
    }
}