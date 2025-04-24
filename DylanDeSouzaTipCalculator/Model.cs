using Plugin.Maui.Audio;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DylanDeSouzaTipCalculator
{
    public partial class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        string billAmount = "0";
        int amountDiners;
        bool decimalButtonPressed;
        int amountDecimalNumbers;
        double tipPercentage;
        bool themeIsToggled;
        string? colorChoice;
        Color selectedBackgroundColor = Colors.Red;
        bool playSound;
        double fontSize;
        string? currentPageOn;
        IAudioPlayer? buttonSound;

        static readonly List<string> colorChoices = ["Red", "Green", "Blue"];

        public string BillAmount
        {
            get => billAmount;
            set => UpdateField(ref billAmount, value, nameof(TipAmount), nameof(Total), nameof(SplitAmount));
        }

        public int AmountDiners
        {
            // issue isn't with label, as hardcoded value is displayed, so why isn't 1 showing?. Total amount is changing in response to amount diners when amountDiners is returned, so amount diners is being increemnted and decremented.
            get => amountDiners;
            set => UpdateField(ref amountDiners, value, nameof(SplitAmount));
        }

        public bool DecimalButtonPressed
        {
            get => decimalButtonPressed;
            set => UpdateField(ref decimalButtonPressed, value);
        }

        public void PlayButtonSound() => buttonSound?.Play();

        public int AmountDecimalNumbers
        {
            get => amountDecimalNumbers;
            set => UpdateField(ref amountDecimalNumbers, value);
        }

        public double TipPercentage
        {
            get => tipPercentage;
            set
            {
                if (tipPercentage != value)
                {
                    tipPercentage = value;
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(TipPercentage), tipPercentage);
                    NotifyDependentProperties(nameof(TipAmount), nameof(Total), nameof(SplitAmount));
                }
            }
        }

        public double TipAmount => TipPercentage * double.Parse(BillAmount);

        public double Total => TipAmount + double.Parse(BillAmount);

        public double SplitAmount => amountDiners > 0 ? Total / amountDiners : 0;

        public bool ThemeIsToggled
        {
            get => themeIsToggled;
            set
            {
                if (themeIsToggled != value)
                {
                    themeIsToggled = value;
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(ThemeIsToggled), themeIsToggled);
                    Application.Current.UserAppTheme = themeIsToggled ? AppTheme.Dark : AppTheme.Light;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ColorChoice
        {
            get => colorChoice ?? "Red";
            set
            {
                if (string.IsNullOrEmpty(value)) value = "Red";

                if (colorChoice != value)
                {
                    colorChoice = value;
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(ColorChoice), value);
                    UpdateSelectedColor(value);
                    NotifyPropertyChanged(nameof(ColorChoice), nameof(SelectedBackgroundColor));
                }
            }
        }

        public Color SelectedBackgroundColor
        {
            get => selectedBackgroundColor;
            set => UpdateField(ref selectedBackgroundColor, value);
        }

        public bool PlaySound
        {
            get => playSound;
            set
            {
                if (playSound != value)
                {
                    playSound = value;
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(PlaySound), playSound);
                    NotifyPropertyChanged();
                }
            }
        }

        public double FontSize
        {
            get => fontSize;
            set
            {
                if (fontSize != value)
                {
                    fontSize = value;
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(FontSize), fontSize);
                    NotifyPropertyChanged();
                }
            }
        }

        public string CurrentPageOn
        {
            get => currentPageOn;
            set
            {
                if (currentPageOn != value)
                {
                    currentPageOn = value;
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(CurrentPageOn), currentPageOn);
                    NotifyPropertyChanged();
                }
            }
        }

        public List<string> ColorChoices => colorChoices;

        public void Reset()
        {
            billAmount = "0";
            amountDiners = 1;
            amountDecimalNumbers = 0;
            decimalButtonPressed = false;
            NotifyDependentProperties(nameof(BillAmount), nameof(TipAmount), nameof(Total), nameof(SplitAmount));
        }

        public void HandleNumericInput(string value)
        {
            if (decimalButtonPressed && amountDecimalNumbers >= 2) return;
            if (decimalButtonPressed) amountDecimalNumbers++;
            billAmount = billAmount == "0" ? value : billAmount + value;
            NotifyPropertyChanged(nameof(BillAmount));
        }

        public void HandleDecimalInput()
        {
            if (decimalButtonPressed) return;

            decimalButtonPressed = true;
            billAmount += ".";
            NotifyPropertyChanged(nameof(BillAmount));
        }

        public async Task InitializeAudioPlayerAsync()
        {
            buttonSound ??= AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("tap.wav"));
        }

        public void UpdateSelectedColor(string colorChoice)
        {
            var colorMapping = new Dictionary<string, Color>
            {
                { "Red", Colors.Red },
                { "Green", Colors.Green },
                { "Blue", Colors.Blue }
            };

            if (colorMapping.TryGetValue(colorChoice, out var color))
                SelectedBackgroundColor = color;
        }

        public void LoadMainPagePreferences()
        {
            billAmount = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(BillAmount), "0");
            amountDiners = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(AmountDiners), 1);
            tipPercentage = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(TipPercentage), 0.0);

            decimalButtonPressed = billAmount.Contains('.');
            amountDecimalNumbers = decimalButtonPressed ? billAmount.Length - (billAmount.IndexOf('.') + 1) : 0;

            NotifyDependentProperties(nameof(BillAmount), nameof(AmountDiners), nameof(TipPercentage), nameof(decimalButtonPressed), nameof(amountDecimalNumbers));
        }

        public void LoadPreferencesPageSettings()
        {
            LoadPreferenceValues();
            UpdateUIProperties();
        }

        void LoadPreferenceValues()
        {
            playSound = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(PlaySound), false);
            themeIsToggled = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(ThemeIsToggled), true);
            fontSize = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(FontSize), 14.0);
            colorChoice = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(ColorChoice), "Red");
            UpdateSelectedColor(colorChoice);
        }

        void UpdateUIProperties()
        {
            NotifyPropertyChanged(nameof(PlaySound), nameof(ThemeIsToggled), nameof(FontSize), nameof(ColorChoice), nameof(SelectedBackgroundColor));
        }

        void UpdateField<T>(ref T field, T value, params string[] dependentProperties)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                NotifyPropertyChanged();
                NotifyDependentProperties(dependentProperties);
            }
        }

        void NotifyPropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                NotifyPropertyChanged(propertyName);
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void NotifyDependentProperties(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                NotifyPropertyChanged(propertyName);
            }
        }
    }
}