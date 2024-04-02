using Player;
using UnityEngine;
using Zenject;

namespace MainCamera
{
    public class CameraController: ITickable
    {
        private readonly PlayerController _playerController;
        private readonly TickableManager _tickableManager;
       
        private CameraView _cameraView;
        private Vector3 offset;
        private PlayerView _playerView;
        
        private CameraController(
            PlayerController playerController,
            TickableManager tickableManager,
            CameraView cameraView)
        {
            _playerController = playerController;
            _tickableManager = tickableManager;
            _cameraView = cameraView;
            _tickableManager.Add(this);
        }
        
        public CameraView GetCameraView()
        {
            return _cameraView;
        }

        public void Tick()
        {
            if (_playerView)
            {
                Vector3 desiredPosition = _playerView.transform.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(_cameraView.transform.position, desiredPosition, Time.deltaTime * 5f);
                _cameraView.transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, _cameraView.transform.position.z); 
            }
            else
            {
                _playerView = _playerController.GetPlayerView();
            }
            
        }
    }
}