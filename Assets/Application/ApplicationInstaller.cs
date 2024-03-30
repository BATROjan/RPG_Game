using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.InstantiatePrefabResource("CameraView");
        GridInstaller.Install(Container);
    }
}
