using DefaultNamespace.UI;
using Player;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class InputController : ITickable
    {
        private readonly PlayerController _playerController;
        private readonly TickableManager _tickableManager;

        private FixedJoystick _fixedJoystick;
        private Rigidbody2D _rigidbody2D;

        private PlayerView _playerView;
        
        public InputController(
            PlayerController playerController,
            TickableManager tickableManager)
        {
            _playerController = playerController;
            _tickableManager = tickableManager;
            _tickableManager.Add(this);
        }
        public void Tick()
        {
            if (_rigidbody2D && _fixedJoystick)
            {
                _rigidbody2D.velocity = new Vector2(_fixedJoystick.Horizontal * _playerView.Speed, _fixedJoystick.Vertical * _playerView.Speed);
            }
            else
            {
                _fixedJoystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();

                _playerView = _playerController.GetPlayerView();
                
                _rigidbody2D = _playerView.GetComponent<Rigidbody2D>();
            }
            
        }
    }
}