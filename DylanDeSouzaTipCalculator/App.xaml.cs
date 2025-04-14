using Syncfusion.Licensing;

namespace DylanDeSouzaTipCalculator
{
    public partial class App : Application
    {
        public App()
        {
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF1cXmhKYVFzWmFZfVtgdVdMZFxbRXZPMyBoS35Rc0VhW3xed3VSRWheV0F+");
            InitializeComponent();

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}