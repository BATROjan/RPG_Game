using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.UI;
using Grid;
using Player;
using UnityEngine;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.InstantiatePrefabResource("CameraView");
        
        GridInstaller.Install(Container);
        PlayerInstaller.Install(Container);
        UIInstaller.Install(Container);
        InputInstaller.Install(Container);
        
        Container.Bind<GameController.GameController>().AsSingle().NonLazy();
    }
}
