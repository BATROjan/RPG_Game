using Zenject;

namespace Grid
{
    public class GridInstaller : Installer<GridInstaller>
    {
        public override void InstallBindings()
        {
            Container.InstantiatePrefabResourceForComponent<GridView>("Grid");
        }
    }
}