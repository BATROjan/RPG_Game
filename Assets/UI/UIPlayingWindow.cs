using UnityEngine;
using Zenject;

namespace DefaultNamespace.UI
{
    public class UIPlayingWindow : MonoBehaviour
    {
        public UIButton[] Buttons;
        
        public class Pool : MonoMemoryPool<UIPlayingWindow>
        {
            
        }
    }
}