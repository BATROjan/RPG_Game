using Zenject;

namespace DefaultNamespace.UI
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            Container.InstantiatePrefabResourceForComponent<UIRoot>("UIRoot");
        }
    }
}