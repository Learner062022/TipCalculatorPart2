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
        double tipPercentage;
        bool themeIsToggled;
        string? colorChoice;
        readonly List<string> colorChoices = ["None", "Red", "Green", "Blue"];
        Color? selectedBackgroundColor;
        IAudioPlayer? buttonSound;
        bool decimalButtonPressed;
        int amountDecimalNumbers;
        bool playSound;
        double fontSize;
        string? currentPageOn;

        public string BillAmount
        {
            get => billAmount;
            set
            {
                if (billAmount != value)
                {
                    billAmount = value;
                    OnPropertyChanged();
                    NotifyDependentProperties(nameof(TipAmount), nameof(Total), nameof(SplitAmount));
                }
            }
        }

        public int AmountDiners
        {
            get => amountDiners;
            set
            {
                if (amountDiners != value)
                {
                    amountDiners = value;
                    OnPropertyChanged();
                    NotifyDependentProperties(nameof(SplitAmount));
                }
            }
        }

        public double TipPercentage
        {
            get => tipPercentage;
            set
            {
                tipPercentage = value;
                OnPropertyChanged();
                NotifyDependentProperties(nameof(TipAmount), nameof(Total), nameof(SplitAmount));
            }
        }

        public double TipAmount => TipPercentage * double.Parse(BillAmount);

        public double Total => TipAmount + double.Parse(BillAmount);

        public double SplitAmount => AmountDiners > 0 ? Total / AmountDiners : 0;

        public bool ThemeIsToggled
        {
            get => themeIsToggled;
            set
            {
                if (themeIsToggled != value)
                {
                    themeIsToggled = value;
                    SetUserAppTheme(themeIsToggled);
                    OnPropertyChanged();
                }
            }
        }

        public string? ColorChoice
        {
            get => colorChoice;
            set
            {
                if (colorChoice != value)
                {
                    colorChoice = value;
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(ColorChoice), colorChoice);
                    OnPropertyChanged();
                    UpdateSelectedColor(colorChoice);
                }
            }
        }

        void UpdateSelectedColor(string colorChoice)
        {
            SelectedBackgroundColor = colorChoice switch
            {
                "Red" => Colors.Red,
                "Green" => Colors.Green,
                "Blue" => Colors.Blue,
                _ => null
            };
        }

        public List<string> ColorChoices => colorChoices;

        public Color? SelectedBackgroundColor
        {
            get => selectedBackgroundColor;
            set
            {
                if (selectedBackgroundColor != value)
                {
                    selectedBackgroundColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? CurrentPageOn
        {
            get => currentPageOn;
            set
            {
                if (currentPageOn != value)
                {
                    currentPageOn = value;
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(CurrentPageOn), currentPageOn);
                    OnPropertyChanged();
                }
            }
        }

        public bool DecimalButtonPressed
        {
            get => decimalButtonPressed;
            set
            {
                if (decimalButtonPressed != value)
                {
                    decimalButtonPressed = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AmountDecimalNumbers
        {
            get => amountDecimalNumbers;
            set
            {
                if (amountDecimalNumbers != value)
                {
                    amountDecimalNumbers = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool PlaySound
        {
            get => playSound;
            set
            {
                if (playSound != value)
                {
                    playSound = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        void SetUserAppTheme(bool isDarkModeEnabled)
        {
            Application.Current.UserAppTheme = isDarkModeEnabled ? AppTheme.Dark : AppTheme.Light;
            OnPropertyChanged(nameof(ThemeIsToggled));
        }

        void NotifyDependentProperties(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
                OnPropertyChanged(propertyName);
        }

        public void Reset()
        {
            BillAmount = "0";
            AmountDiners = 1;
            AmountDecimalNumbers = 0;
            DecimalButtonPressed = false;
            NotifyDependentProperties(nameof(BillAmount), nameof(TipAmount), nameof(Total), nameof(SplitAmount));
        }

        public void HandleNumericInput(string value)
        {
            if (DecimalButtonPressed && AmountDecimalNumbers >= 2) return;

            if (DecimalButtonPressed) AmountDecimalNumbers++;

            BillAmount = BillAmount == "0" ? value : BillAmount + value;
        }

        public void HandleDecimalInput()
        {
            if (DecimalButtonPressed) return;

            DecimalButtonPressed = true;
            BillAmount += ".";
        }

        public async Task InitializeAudioPlayerAsync() =>
            buttonSound ??= AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("tap.wav"));

        public void PlayButtonSound() => buttonSound?.Play();

        public void LoadMainPagePreferences()
        {
            BillAmount = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(BillAmount), "0");
            AmountDiners = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(AmountDiners), 1);
            TipPercentage = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(TipPercentage), 0.0);
            DecimalButtonPressed = BillAmount.Contains('.');
            AmountDecimalNumbers = DecimalButtonPressed
                ? BillAmount.Length - (BillAmount.IndexOf('.') + 1)
                : 0;
        }

        public void LoadPreferencesPageSettings()
        {
            PlaySound = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(PlaySound), false);
            ThemeIsToggled = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(ThemeIsToggled), true);
            FontSize = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(FontSize), 0.0);
            CurrentPageOn = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(CurrentPageOn), "main");
            ColorChoice = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(ColorChoice), "Red");
            UpdateSelectedColor(ColorChoice);
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}