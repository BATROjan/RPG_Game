using Zenject;

namespace BackPack
{
    public class BackPackInstaller : Installer<BackPackInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<BackPackController>()
                .AsSingle().NonLazy();
        }
    }
}