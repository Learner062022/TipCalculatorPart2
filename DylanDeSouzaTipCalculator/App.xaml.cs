using Syncfusion.Licensing;

namespace DylanDeSouzaTipCalculator
{
    public partial class App : Application
    {
        Model model = new();

        public App()
        {
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF1cXmhKYVFzWmFZfVtgdVdMZFxbRXZPMyBoS35Rc0VhW3xed3VSRWheV0F+");
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState) => new(new AppShell());

        protected override void OnStart()
        {
            model.ThemeIsToggled = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(model.ThemeIsToggled), false);
            Current.UserAppTheme = model.ThemeIsToggled ? AppTheme.Dark : AppTheme.Light;
        }
    }
}