using Zenject;

namespace DefaultNamespace
{
    public class InputInstaller: Installer<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<InputController>().AsSingle().NonLazy();
        }
    }
}