using System.Runtime.CompilerServices;

namespace DylanDeSouzaTipCalculator
{
    public partial class Preferences : ContentPage
    {
        Model model;

        public Preferences(Model model)
        {
            InitializeComponent();
            this.model = model;
            model.LoadPreferencesPageSettings();
            BindingContext = this.model;

        }

        protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            base.OnPropertyChanged(propertyName);

        public bool ThemeIsToggled
        {
            get => model.ThemeIsToggled;
            set { if (model.ThemeIsToggled != value) model.ThemeIsToggled = value; }
        }

        public double FontSize
        {
            get => model.FontSize;
            set 
            { 
                if (model.FontSize != value)
                {
                    model.FontSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool PlaySound
        {
            get => model.PlaySound;
            set { if (model.PlaySound != value) model.PlaySound = value; }
        }

        public List<string> Colors { get => model.Colors; }

        public string? ColorScheme
        {
            get => model.ColorScheme;
            set { if (model.ColorScheme != value) model.ColorScheme = value; }
        }

        protected override bool OnBackButtonPressed()
        {
            model.CurrentPageOn = "main";
            Microsoft.Maui.Storage.Preferences.Default.Set(nameof(model.CurrentPageOn), "main");
            return base.OnBackButtonPressed();
        }
    }
}