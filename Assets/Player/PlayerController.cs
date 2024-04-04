using DefaultNamespace.UI;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerController
    {
        private readonly UIPlayingWindowController _uiPlayingWindowController;
        private readonly PlayerView.Pool _playerPool;
        private readonly PlayerConfig _playerConfig;

        private PlayerView _playerView;
        private UIPlayingWindow _uiPlayingWindow;
        public PlayerController(
            UIPlayingWindowController uiPlayingWindowController,
            PlayerView.Pool playerPool,
            PlayerConfig playerConfig)
        {
            _uiPlayingWindowController = uiPlayingWindowController;
            _playerPool = playerPool;
            _playerConfig = playerConfig;
        }

        public PlayerView SpawnPlayer()
        {
            _playerView = _playerPool.Spawn(_playerConfig.GetPlayerModel());
            _uiPlayingWindowController.GetWindow().Buttons[0].OnClick += Fire;
            return _playerView;
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