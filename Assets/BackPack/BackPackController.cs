using System.Collections.Generic;
using DefaultNamespace.UI;
using Gun;
using Player;
using UnityEngine;

namespace BackPack
{
    public class BackPackController
    {
        private readonly UIPlayingWindowController _uiPlayingWindowController;

        private bool isActive;
        private UIPlayingWindow _uiPlayingWindow;

        private Dictionary<GunConfig.GunType, GunView> _dictionaryOfGun = new Dictionary<GunConfig.GunType, GunView>();

        private GunView currentGun;
        public BackPackController(
            UIPlayingWindowController uiPlayingWindowController)
        {
            _uiPlayingWindowController = uiPlayingWindowController;
        }

        public void TakeGunInBackPack(GunView gunView)
        {
            if (!_dictionaryOfGun.ContainsKey(gunView.GetGunType()))
            {
                if (_dictionaryOfGun.Count == 0)
                {
                    currentGun = gunView;
                }
                _dictionaryOfGun.Add(gunView.GetGunType(), gunView);
                
                foreach (var cell in _uiPlayingWindow.BackPackView.CellViews)
                {
                    if (!cell.IsUsed)
                    {
                        cell.CellImage.sprite = gunView.GetGunImage();
                        cell.Count.text = " ";
                        cell.IsUsed = true;
                        return;
                    }
                }
            }
            
            GameObject.Destroy(gunView.gameObject);
        }

        public Sprite GetCurrentSprite()
        {
           
            if(_dictionaryOfGun.Count == 1)
            {
                return currentGun.GetGunImage();
            }
            
            return null;
        }

        public void Init()
        {
            _uiPlayingWindow = _uiPlayingWindowController.GetWindow();
            _uiPlayingWindow.Buttons[1].OnClick += ControllBackPack;
        }

        private void ControllBackPack()
        {
            _uiPlayingWindow.Buttons[0].gameObject.SetActive(isActive);
            _uiPlayingWindow.BackPackView.gameObject.SetActive(!isActive);
            _uiPlayingWindow.FixedJoystick.gameObject.SetActive(isActive);
            isActive = !isActive;
        }
    }
}