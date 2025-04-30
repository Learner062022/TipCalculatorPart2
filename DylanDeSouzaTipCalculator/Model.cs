using Plugin.Maui.Audio;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DylanDeSouzaTipCalculator
{
    public partial class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        string billAmount = "0";
        int amountDiners = 1;
        bool decimalButtonPressed;
        int amountDecimalNumbers;
        double tipPercentage = 0.0;
        bool themeIsToggled;
        string? colorChoice;
        bool playSound;
        double fontSize = 14.0;
        string? currentPageOn;
        IAudioPlayer? buttonSound;
        Color? selectedBackgroundColor;
        static readonly List<string> colorChoices = ["Red", "Green", "Blue"];

        public string BillAmount
        {
            get => billAmount;
            set
            {
                if (UpdateAndPersist(nameof(BillAmount), ref billAmount, value, nameof(BillAmount), nameof(TipAmount), nameof(Total), nameof(SplitAmount)))
                    UpdateDecimalState();
            }
        }

        public int AmountDiners
        {
            get => amountDiners;
            set => UpdateAndPersist(nameof(AmountDiners), ref amountDiners, value, nameof(AmountDiners), nameof(SplitAmount));
        }

        public double TipPercentage
        {
            get => tipPercentage;
            set => UpdateAndPersist(nameof(TipPercentage), ref tipPercentage, value, nameof(TipPercentage), nameof(TipAmount), nameof(Total), nameof(SplitAmount));
        }

        public double TipAmount
        {
            get
            {
                if (TryParseBill(out var parsedAmount))
                    return TipPercentage * parsedAmount;
                return 0;
            }
        }

        public double Total
        {
            get
            {
                if (TryParseBill(out var parsedAmount))
                    return TipAmount + parsedAmount;
                return TipAmount;
            }
        }

        public double SplitAmount => amountDiners > 0 ? Total / amountDiners : 0;

        public bool ThemeIsToggled
        {
            get => themeIsToggled;
            set
            {
                if (UpdateAndPersist(nameof(ThemeIsToggled), ref themeIsToggled, value, nameof(ThemeIsToggled)))
                    Application.Current.UserAppTheme = themeIsToggled ? AppTheme.Dark : AppTheme.Light;
            }
        }

        public string? ColorChoice
        {
            get => colorChoice;
            set
            {
                if (UpdateAndPersist(nameof(ColorChoice), ref colorChoice, value, nameof(ColorChoice), nameof(SelectedBackgroundColor)))
                    SelectedBackgroundColor = Color.Parse(value);
            }
        }

        public Color? SelectedBackgroundColor
        {
            get => selectedBackgroundColor;
            set => UpdateField(ref selectedBackgroundColor, value, nameof(SelectedBackgroundColor));
        }

        public bool PlaySound
        {
            get => playSound;
            set => UpdateAndPersist(nameof(PlaySound), ref playSound, value, nameof(PlaySound));
        }

        public double FontSize
        {
            get => fontSize;
            set => UpdateAndPersist(nameof(FontSize), ref fontSize, value, nameof(FontSize));
        }

        public string? CurrentPageOn
        {
            get => currentPageOn;
            set => UpdateAndPersist(nameof(CurrentPageOn), ref currentPageOn, value, nameof(CurrentPageOn));
        }

        public List<string> ColorChoices => colorChoices;
        
        public void PlayButtonSound() => buttonSound?.Play();

        public void Reset()
        {
            BillAmount = "0";
            AmountDiners = 1;
            decimalButtonPressed = false;
            amountDecimalNumbers = 0;

            NotifyMultiplePropertiesChanged(nameof(BillAmount), nameof(TipAmount), nameof(Total), nameof(SplitAmount));
        }

        public void HandleNumericInput(string value)
        {
            if (decimalButtonPressed && amountDecimalNumbers >= 2) return;

            if (decimalButtonPressed) amountDecimalNumbers++;
            BillAmount = BillAmount == "0" ? value : BillAmount + value;
        }

        public void HandleDecimalInput()
        {
            if (decimalButtonPressed) return;

            decimalButtonPressed = true;
            BillAmount += ".";
        }

        public async void InitializeAudioPlayerAsync() =>
            buttonSound ??= AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("tap.wav"));

        public void LoadMainPagePreferences()
        {
            FontSize = LoadAndNotify(nameof(FontSize), 14.0, nameof(FontSize));
            BillAmount = LoadAndNotify(nameof(BillAmount), "0", nameof(BillAmount), nameof(decimalButtonPressed), nameof(amountDecimalNumbers), nameof(TipAmount), nameof(Total), nameof(SplitAmount));
            AmountDiners = LoadAndNotify(nameof(AmountDiners), 1, nameof(AmountDiners), nameof(SplitAmount));
            TipPercentage = LoadPreference(nameof(TipPercentage), 0.0);

            UpdateDecimalState(); 
            ColorChoice = LoadAndNotify(nameof(ColorChoice), "Red", nameof(ColorChoice), nameof(SelectedBackgroundColor));
            SelectedBackgroundColor = Color.Parse(ColorChoice);
        }

        public void LoadPreferencesPageSettings()
        {
            Application.Current.UserAppTheme = ThemeIsToggled ? AppTheme.Dark : AppTheme.Light;
            LoadPreferenceValues();
            NotifyMultiplePropertiesChanged(nameof(PlaySound), nameof(ThemeIsToggled), nameof(FontSize), nameof(ColorChoice), nameof(SelectedBackgroundColor));
        }

        void UpdateDecimalState()
        {
            decimalButtonPressed = BillAmount.Contains('.');
            amountDecimalNumbers = decimalButtonPressed ? BillAmount.Length - BillAmount.IndexOf('.') - 1 : 0;
        }

        bool TryParseBill(out double parsedAmount) => double.TryParse(BillAmount, out parsedAmount);

        void LoadPreferenceValues()
        {
            PlaySound = LoadAndNotify(nameof(PlaySound), false, nameof(PlaySound));
            ThemeIsToggled = LoadAndNotify(nameof(ThemeIsToggled), true, nameof(ThemeIsToggled));
            FontSize = LoadAndNotify(nameof(FontSize), 14.0, nameof(FontSize));
            ColorChoice = LoadAndNotify(nameof(ColorChoice), "Red", nameof(ColorChoice), nameof(SelectedBackgroundColor));
            SelectedBackgroundColor = Color.Parse(ColorChoice);
        }

        T LoadAndNotify<T>(string key, T defaultValue, params string[] dependentProperties)
        {
            T value = LoadPreference(key, defaultValue);
            NotifyMultiplePropertiesChanged(dependentProperties);
            return value;
        }

        T LoadPreference<T>(string key, T defaultValue) =>
            Microsoft.Maui.Storage.Preferences.Default.Get(key, defaultValue);

        bool UpdateAndPersist<T>(string key, ref T field, T value, params string[] dependentProperties)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;

            field = value;
            Microsoft.Maui.Storage.Preferences.Default.Set(key, field); 
            NotifyMultiplePropertiesChanged(dependentProperties);
            return true;
        }

        bool UpdateField<T>(ref T field, T value, params string[] dependentProperties)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;

            field = value;
            NotifyMultiplePropertiesChanged(dependentProperties);
            return true;
        }

        void NotifyMultiplePropertiesChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
                NotifyPropertyChanged(propertyName);
        }

        protected void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}