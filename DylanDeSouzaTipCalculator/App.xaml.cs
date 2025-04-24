using Syncfusion.Licensing;

namespace DylanDeSouzaTipCalculator
{
    public partial class App : Application
    {
        readonly Model model = new();

        public App() => InitializeComponent();

        protected override Window CreateWindow(IActivationState? activationState)
            => new(new AppShell());

        protected async override void OnStart()
        {
            SyncfusionLicenseProvider.RegisterLicense(
                "Ngo9BigBOggjHTQxAR8/V1NMaF1cXmhKYVFzWmFZfVtgdVdMZFxbRXZPMyBoS35Rc0VhW3xed3VSRWheV0F+");
            model.LoadPreferencesPageSettings();
            model.LoadMainPagePreferences();
            Current.UserAppTheme = model.ThemeIsToggled ? AppTheme.Dark : AppTheme.Light;
            await InitializeAudioPlayer();
        }

        async Task InitializeAudioPlayer() =>
            await model.InitializeAudioPlayerAsync();
    }
}