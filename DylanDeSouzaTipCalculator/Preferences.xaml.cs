namespace DylanDeSouzaTipCalculator
{
    public partial class Preferences : ContentPage
    {
        private readonly Model model;

        public Preferences(Model model)
        {
            InitializeComponent();
            this.model = model;
            model.LoadPreferencesPageSettings();
            BindingContext = this.model;
        }

        protected override bool OnBackButtonPressed()
        {
            model.CurrentPageOn = "main";
            Microsoft.Maui.Storage.Preferences.Default.Set(nameof(model.CurrentPageOn), "main");
            return base.OnBackButtonPressed();
        }
    }
}