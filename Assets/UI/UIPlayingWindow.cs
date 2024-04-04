using System;
using BackPack;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.UI
{
    public class UIPlayingWindow : MonoBehaviour
    {
        public BackPackView BackPackView;
        public FixedJoystick FixedJoystick;
        
        public UIButton[] Buttons;

        private void Start()
        {
            FixedJoystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        }

        public class Pool : MonoMemoryPool<UIPlayingWindow>
        {
            
        }
    }
}