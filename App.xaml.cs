namespace SFI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell(); // or your main page
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
#if WINDOWS
            // Initial size
            window.Width = 540;
            window.Height = 1200;

            // Limits (desktop only: Windows)
            window.MaximumWidth = 540;
            window.MaximumHeight = 1200;
            window.MinimumWidth = 540;
            window.MinimumHeight = 1200;
#endif
            return window;
        }
    }
}