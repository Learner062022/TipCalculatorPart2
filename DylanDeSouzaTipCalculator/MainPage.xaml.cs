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

            model.LoadMainPagePreferences();
            InitializeModelAudio();
            BindingContext = model;
            if (model.CurrentPageOn == "prefs") Navigation.PushAsync(prefs);
        }

        async void InitializeModelAudio() =>
            await model.InitializeAudioPlayerAsync();

        void Reset() => model.Reset();

        void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (model.PlaySound) model.PlayButtonSound();

            switch (button.Text)
            {
                case "C":
                    Reset();
                    break;
                case ".":
                    model.HandleDecimalInput();
                    break;
                case "0":
                    if (model.BillAmount != "0") model.HandleNumericInput(button.Text);
                    break;
                case "Prefs":
                    model.CurrentPageOn = "prefs";
                    Navigation.PushAsync(prefs);
                    break;
                default:
                    model.HandleNumericInput(button.Text);
                    break;
            }
        }
    }
}