namespace Player
{
    public class PlayerController
    {
        private readonly PlayerView.Pool _playerPool;
        private readonly PlayerConfig _playerConfig;

        private PlayerView _playerView;
        
        public PlayerController(
            PlayerView.Pool playerPool,
            PlayerConfig playerConfig)
        {
            _playerPool = playerPool;
            _playerConfig = playerConfig;
        }

        public PlayerView SpawnPlayer()
        {
            _playerView = _playerPool.Spawn(_playerConfig.GetPlayerModel());
            return _playerView;
        }

        public PlayerView GetPlayerView()
        {
            return _playerView;
        }
    }
}