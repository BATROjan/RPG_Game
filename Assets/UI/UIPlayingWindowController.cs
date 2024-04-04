namespace DefaultNamespace.UI
{
    public class UIPlayingWindowController
    {
        private readonly UIPlayingWindow.Pool _pool;

        private UIPlayingWindow _uiPlayingWindow;
        
        public UIPlayingWindowController(UIPlayingWindow.Pool pool)
        {
            _pool = pool;
        }

        public void Spawn()
        {
           _uiPlayingWindow = _pool.Spawn();
        }

        public UIPlayingWindow GetWindow()
        {
            return _uiPlayingWindow;
        }
    }
}