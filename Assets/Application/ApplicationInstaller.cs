using System.Collections;
using System.Collections.Generic;
using BackPack;
using DefaultNamespace;
using DefaultNamespace.UI;
using Enemy;
using Grid;
using Gun;
using MainCamera;
using Player;
using UnityEngine;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        CameraInstaller.Install(Container);
        UIInstaller.Install(Container);
        BackPackInstaller.Install(Container);
        GridInstaller.Install(Container);
        PlayerInstaller.Install(Container);
        GunInstaller.Install(Container);
        InputInstaller.Install(Container);
        EnemyInstaller.Install(Container);
        
        Container.Bind<GameController.GameController>().AsSingle().NonLazy();
    }
}
