using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.UI;
using Enemy;
using Grid;
using MainCamera;
using Player;
using UnityEngine;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        CameraInstaller.Install(Container);
        GridInstaller.Install(Container);
        PlayerInstaller.Install(Container);
        UIInstaller.Install(Container);
        InputInstaller.Install(Container);
        EnemyInstaller.Install(Container);
        
        Container.Bind<GameController.GameController>().AsSingle().NonLazy();
    }
}
