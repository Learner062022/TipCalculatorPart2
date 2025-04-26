using System.Diagnostics;

namespace DylanDeSouzaTipCalculator
{
    public partial class Preferences : ContentPage
    {
        readonly Model model;

        public Preferences(Model model)
        {
            InitializeComponent();
            this.model = model;
            BindingContext = this.model; 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            model.LoadPreferencesPageSettings();
        }

        protected override bool OnBackButtonPressed()
        {
            model.CurrentPageOn = "main";
            Microsoft.Maui.Storage.Preferences.Default.Set(nameof(model.CurrentPageOn), "main");
            return base.OnBackButtonPressed();
        }
    }
}