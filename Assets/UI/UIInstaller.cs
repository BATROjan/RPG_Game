using Zenject;

namespace DefaultNamespace.UI
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindMemoryPool<UIPlayingWindow, UIPlayingWindow.Pool>()
                .FromComponentInNewPrefabResource("UIPlayerWindowView");

            Container
                .Bind<UIPlayingWindowController>()
                .AsSingle()
                .NonLazy();
        }
    }
}