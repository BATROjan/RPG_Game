using Zenject;

namespace Tilemap
{
    public class TilemapInstaller : Installer<TilemapInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<TilemapController>().AsSingle().NonLazy();
        }
    }
}