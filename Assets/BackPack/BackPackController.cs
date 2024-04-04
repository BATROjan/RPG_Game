using DefaultNamespace.UI;

namespace BackPack
{
    public class BackPackController
    {
        private readonly UIPlayingWindowController _uiPlayingWindowController;

        private bool isActive;
        private UIPlayingWindow _uiPlayingWindow;
        
        public BackPackController(
            UIPlayingWindowController uiPlayingWindowController)
        {
            _uiPlayingWindowController = uiPlayingWindowController;
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