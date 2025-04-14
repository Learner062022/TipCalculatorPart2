using System.Runtime.CompilerServices;

namespace DylanDeSouzaTipCalculator
{
    public partial class MainPage : ContentPage
    {
        readonly Model model = new();
        Preferences prefs;

        public MainPage()
        {
            InitializeComponent();
            prefs = new(model);
            model.LoadMainPagePreferences();
            InitializeModelAudio();
            BindingContext = model;

            if (model.CurrentPageOn == "prefs") Navigation.PushAsync(prefs);
        }

        protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            base.OnPropertyChanged(propertyName);

        async void InitializeModelAudio() => await model.InitializeAudioPlayerAsync();

        public string BillAmount
        {
            get => model.BillAmount;
            set
            {
                if (model.BillAmount != value)
                {
                    model.BillAmount = value;
                    NotifyDependentProperties();
                }
            }
        }

        public int AmountDiners
        {
            get => model.AmountDiners;
            set
            {
                if (model.AmountDiners != value)
                {
                    model.AmountDiners = value;
                    NotifyDependentProperties();
                }
            }
        }

        public double TipPercentage
        {
            get => model.TipPercentage;
            set
            {
                model.TipPercentage = value;
                NotifyDependentProperties();
            }
        }

        void Reset() => model.Reset();

        void NotifyDependentProperties(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames) OnPropertyChanged(propertyName);
        }

        void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
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
                    if (BillAmount != "0") model.HandleNumericInput(button.Text);
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