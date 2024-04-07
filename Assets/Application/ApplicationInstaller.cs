using System.Collections;
using System.Collections.Generic;
using BackPack;
using Bullet;
using DefaultNamespace;
using DefaultNamespace.UI;
using Enemy;
using Grid;
using Gun;
using MainCamera;
using Player;
using UnityEngine;
using XMLSystem;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        CameraInstaller.Install(Container);
        UIInstaller.Install(Container);
        XMLSystemInstaller.Install(Container);
        BackPackInstaller.Install(Container);
        GridInstaller.Install(Container);
        PlayerInstaller.Install(Container);
        GunInstaller.Install(Container);
        BulletInstaller.Install(Container);
        InputInstaller.Install(Container);
        EnemyInstaller.Install(Container);
        
        Container.Bind<GameController.GameController>().AsSingle().NonLazy();
    }
}
