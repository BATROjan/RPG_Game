using System;
using System.Collections.Generic;
using DefaultNamespace.UI;
using Gun;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace BackPack
{
    public class BackPackController
    {
        public Action<GunView> OnChangeGun;
        private readonly GunController _gunController;
        private readonly UIPlayingWindowController _uiPlayingWindowController;

        private bool isActive;
        private UIPlayingWindow _uiPlayingWindow;

        private Dictionary<GunConfig.GunType, GunView> _dictionaryOfGun = new Dictionary<GunConfig.GunType, GunView>();

        private GunView currentGun;
        public BackPackController(
            GunController gunController,
            UIPlayingWindowController uiPlayingWindowController)
        {
            _gunController = gunController;
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
                        cell.GunView = gunView;
                        cell.CellImage.sprite = gunView.GetGunImage();
                        cell.Count.text = " ";
                        cell.IsUsed = true;
                        _gunController.Despawn(gunView);
                        return;
                    }
                }
            }
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

        private void SelectGun(CellView cellView)
        {
            if (currentGun != cellView.GunView)
            {
                currentGun = cellView.GunView;
                OnChangeGun?.Invoke(currentGun);
            }
            _uiPlayingWindow.BackPackView.Selectpanel.gameObject.transform.localPosition = cellView.transform.localPosition;
        }

        private void ControllBackPack()
        {
            _uiPlayingWindow.Buttons[0].gameObject.SetActive(isActive);
            _uiPlayingWindow.BackPackView.gameObject.SetActive(!isActive);
            _uiPlayingWindow.FixedJoystick.gameObject.SetActive(isActive);
            if (!isActive)
            {
                foreach (var cell in _uiPlayingWindow.BackPackView.CellViews)
                {
                    if (cell.CellImage.sprite)
                    {
                        cell.OnSelect += SelectGun;
                    }
                }
            }
            else
            {
                foreach (var cell in _uiPlayingWindow.BackPackView.CellViews)
                {
                    cell.OnSelect -= SelectGun;
                }
            }
            isActive = !isActive;
        }
    }
}