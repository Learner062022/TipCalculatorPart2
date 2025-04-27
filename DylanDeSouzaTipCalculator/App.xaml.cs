using Syncfusion.Licensing;

namespace DylanDeSouzaTipCalculator
{
    public partial class App : Application
    {
        readonly Model model = new();

        public App() => InitializeComponent();

        protected override Window CreateWindow(IActivationState? activationState)
            => new(new AppShell());

        protected override void OnStart()
        {
            SyncfusionLicenseProvider.RegisterLicense(
                "Ngo9BigBOggjHTQxAR8/V1NMaF1cXmhKYVFzWmFZfVtgdVdMZFxbRXZPMyBoS35Rc0VhW3xed3VSRWheV0F+");
            model.LoadPreferencesPageSettings();
        }
    }
}