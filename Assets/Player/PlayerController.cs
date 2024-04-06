using BackPack;
using DefaultNamespace.UI;
using DG.Tweening;
using Gun;
using UnityEngine;

namespace Player
{
    public class PlayerController
    {
        private readonly BackPackController _backPackController;
        private readonly UIPlayingWindowController _uiPlayingWindowController;
        private readonly PlayerView.Pool _playerPool;
        private readonly PlayerConfig _playerConfig;

        private PlayerView _playerView;
        private UIPlayingWindow _uiPlayingWindow;
        public PlayerController(
            BackPackController backPackController,
            UIPlayingWindowController uiPlayingWindowController,
            PlayerView.Pool playerPool,
            PlayerConfig playerConfig)
        {
            _backPackController = backPackController;
            _uiPlayingWindowController = uiPlayingWindowController;
            _playerPool = playerPool;
            _playerConfig = playerConfig;
        }

        public PlayerView SpawnPlayer()
        {
            _playerView = _playerPool.Spawn(_playerConfig.GetPlayerModel());
            _uiPlayingWindowController.GetWindow().Buttons[0].OnClick += Fire;
            _playerView.OnTakeGun += TakeGun;
            return _playerView;
        }

        private void TakeGun(GunView gunView)
        {
            _backPackController.TakeGunInBackPack(gunView);
            if (!_playerView.GunSprite.sprite)
            {
                _playerView.GunSprite.sprite = _backPackController.GetCurrentSprite();
            }
        }

        public PlayerView GetPlayerView()
        {
            return _playerView;
        }

        public void Fire()
        {
            Debug.Log("Fire");
        }
    }
}