using System;
using System.Collections.Generic;
using Bullet;
using DefaultNamespace.UI;
using Gun;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using XMLSystem;

namespace BackPack
{
    public class BackPackController
    {
        public int BulletCount;
        
        public Action<GunView> OnChangeGun;
        
        public List<GunView> _listGuns = new List<GunView>();

        private readonly IXMLSystem _xmlSystem;
        private readonly BulletController _bulletController;
        private readonly GunController _gunController;
        private readonly UIPlayingWindowController _uiPlayingWindowController;

        private bool isActive;

        private UIPlayingWindow _uiPlayingWindow;

        private Dictionary<GunConfig.GunType, GunView> _dictionaryOfGun = new Dictionary<GunConfig.GunType, GunView>();
        private Dictionary<Sprite, int> _bulletDictionaryCount = new Dictionary<Sprite, int>();
        private Dictionary<Sprite,CellView> _dictionaryOfBulletCells = new Dictionary<Sprite,CellView>();

        private GunView currentGun;
        private CellView currentCell;
        public BackPackController(IXMLSystem xmlSystem,
            BulletController bulletController,
            GunController gunController,
            UIPlayingWindowController uiPlayingWindowController)
        {
            _xmlSystem = xmlSystem;
            _bulletController = bulletController;
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
                _listGuns.Add(gunView);
                foreach (var cell in _uiPlayingWindow.BackPackView.CellViews)
                {
                    if (!cell.IsUsed)
                    {
                        if (!currentCell)
                        {
                            currentCell = cell;
                        }
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
        public void TakeBulletInBackPack(BulletView bulletView)
        {
            if (_bulletDictionaryCount.Count == 0)
            {
                _bulletDictionaryCount.Add(bulletView.GetBulletSprite(), bulletView.GetBulletCount());
                BulletCount = bulletView.GetBulletCount();
                foreach (var cell in _uiPlayingWindow.BackPackView.CellViews)
                {
                    if (!cell.IsUsed)
                    {
                        if (!currentCell)
                        {
                            currentCell = cell;
                        }
                        cell.CellImage.sprite = bulletView.GetBulletSprite();
                        cell.BulletView = bulletView;
                        cell.Count.text = bulletView.GetBulletCount().ToString();
                        cell.IsUsed = true;
                        _bulletController.Despawn(bulletView);
                        _dictionaryOfBulletCells.Add( bulletView.GetBulletSprite(), cell);
                        return;
                    }
                }
            }
            else
            {
                BulletCount = _bulletDictionaryCount[bulletView.GetBulletSprite()];
                _bulletDictionaryCount.Remove(bulletView.GetBulletSprite());
                BulletCount += bulletView.GetBulletCount();
                _bulletDictionaryCount.Add(bulletView.GetBulletSprite(), BulletCount);
                _dictionaryOfBulletCells[bulletView.GetBulletSprite()].Count.text = BulletCount.ToString();
                _bulletController.Despawn(bulletView);
            }
        }

        public bool SpendBullet()
        {
            bool ready = false;
            if (BulletCount>0)
            {
                BulletCount -= 1;
                if (BulletCount == 0)
                {
                    foreach (var cell in _dictionaryOfBulletCells.Values)
                    {
                        currentCell = cell;
                    }

                    _bulletDictionaryCount.Remove(currentCell.CellImage.sprite);
                    _dictionaryOfBulletCells.Remove(currentCell.CellImage.sprite);
                    
                    DeleteItem();

                }
                else
                {
                    foreach (var cell in _dictionaryOfBulletCells.Values)
                    {
                        cell.Count.text = BulletCount.ToString();
                    }
                }
                ready = true;
            }
            return ready;
        }

        public void Init()
        {
            _uiPlayingWindow = _uiPlayingWindowController.GetWindow();
            for (int i = 0; i < _gunController.GunViews.Count; i++)
            {
                if (_xmlSystem.LoadFromXML("gun" + i.ToString(), "type") != null)
                { 
                    GunConfig.GunType type = (GunConfig.GunType)Enum.Parse(typeof(GunConfig.GunType) ,_xmlSystem.LoadFromXML("gun" + i.ToString(), "type"));
                    var gunView = _gunController.GetViewByType(type);
                    TakeGunInBackPack(gunView);
                }
            }
            if (_xmlSystem.LoadFromXML("bullet", "count")!= null)
            {
                int count = int.Parse(_xmlSystem.LoadFromXML("bullet", "count"));
                var bullet = _bulletController.SpawnCustomBullet(count);
                TakeBulletInBackPack(bullet);
            }
            _uiPlayingWindow.Buttons[1].OnClick += ControllBackPack;
            _uiPlayingWindow.BackPackView.DeleteButton.OnClick += DeleteItem;
        }

        public CellView GetCellWithBullet()
        {
            foreach (var cell in _uiPlayingWindow.BackPackView.CellViews)
            {
                if (cell.BulletView)
                {
                    return cell;
                }
            }
            return null;
        }

        private void DeleteItem()
        {
            currentCell.CellImage.sprite = null;
            if (currentCell.GunView)
            {
                if (currentCell.GunView.GetGunType() == currentGun.GetGunType())
                {
                    currentGun = null;
                    OnChangeGun?.Invoke(currentGun);
                }
                _listGuns.Remove(currentCell.GunView);
            }
            currentCell.GunView = null;
            currentCell.BulletView = null;
            currentCell.IsUsed = false;
            currentCell.Count.text = " ";
            currentCell.OnSelect -= SelectGun;
        }

        private void SelectGun(CellView cellView)
        {
            if (cellView.GunView)
            {
                if (currentGun != cellView.GunView)
                {
                    currentGun = cellView.GunView;
                    OnChangeGun?.Invoke(currentGun);
                }
            }

            currentCell = cellView;
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