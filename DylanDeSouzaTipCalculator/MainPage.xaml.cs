using System.Diagnostics;

namespace DylanDeSouzaTipCalculator
{
    public partial class MainPage : ContentPage
    {
        readonly Model model;
        readonly Preferences prefs;

        public MainPage()
        {
            InitializeComponent();
            model = new Model();
            prefs = new Preferences(model);
            model.CurrentPageOn = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(model.CurrentPageOn), "main");
            if (model.CurrentPageOn == "prefs") Navigation.PushAsync(prefs);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitializeModelAudio();
            model.LoadMainPagePreferences();
            BindingContext = model;
        }

        async void InitializeModelAudio() =>
            await model.InitializeAudioPlayerAsync();

        void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (model.PlaySound) model.PlayButtonSound();

            switch (button.Text)
            {
                case "C":
                    model.Reset();
                    break;
                case ".":
                    model.HandleDecimalInput();
                    break;
                case "0":
                    if (model.BillAmount != "0") model.HandleNumericInput(button.Text);
                    break;
                case "Prefs":
                    model.CurrentPageOn = "prefs";
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(model.CurrentPageOn), model.CurrentPageOn);
                    Navigation.PushAsync(prefs);
                    break;
                default:
                    model.HandleNumericInput(button.Text);
                    break;
            }
        }
    }
}