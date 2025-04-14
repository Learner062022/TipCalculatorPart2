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
        int amountDecimalNumbers;
        IAudioPlayer? buttonSound;
        bool decimalButtonPressed;
        bool playSound;
        double fontSize;
        bool themeIsToggled;
        string? currentPageOn;
        List<string> colors = ["Red", "Green", "Blue"];
        string? colorScheme;

        public string? CurrentPageOn
        {
            get => currentPageOn;
            set
            {
                currentPageOn = value;
                Microsoft.Maui.Storage.Preferences.Default.Set(nameof(CurrentPageOn), currentPageOn);
                OnPropertyChanged();
            }
        }

        public bool ThemeIsToggled
        {
            get => themeIsToggled;
            set
            {
                if (themeIsToggled != value)
                {
                    themeIsToggled = value;
                    OnPropertyChanged();
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(ThemeIsToggled), themeIsToggled);
                }
            }
        }

        public List<string> Colors { get => colors; }

        public string? ColorScheme
        {
            get => colorScheme;
            set
            {
                if (colorScheme != value)
                {
                    colorScheme = value;
                    OnPropertyChanged();
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(ColorScheme), colorScheme);
                }
            }
        }

        public string BillAmount
        {
            get => billAmount;
            set
            {
                if (billAmount != value)
                {
                    billAmount = value;
                    OnPropertyChanged();
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(BillAmount), billAmount);
                    NotifyDependentProperties(nameof(TipAmount), nameof(Total), nameof(SplitAmount));
                }
            }
        }

        public void HandleNumericInput(string value)
        {
            if (DecimalButtonPressed && AmountDecimalNumbers >= 2) return;

            if (DecimalButtonPressed) AmountDecimalNumbers++;

            BillAmount = BillAmount == "0" ? value : BillAmount + value;
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
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(FontSize), fontSize);
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

        public int AmountDiners
        {
            get => amountDiners;
            set
            {
                if (amountDiners != value)
                {
                    amountDiners = value;
                    OnPropertyChanged();
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(AmountDiners), amountDiners);
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
                Microsoft.Maui.Storage.Preferences.Default.Set(nameof(TipPercentage), tipPercentage);
                NotifyDependentProperties(nameof(TipAmount), nameof(Total), nameof(SplitAmount));
            }
        }

        public double TipAmount => TipPercentage * double.Parse(BillAmount);

        public double Total => TipAmount + double.Parse(BillAmount);

        public double SplitAmount => AmountDiners > 0 ? Total / AmountDiners : 0;

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
            ThemeIsToggled = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(ThemeIsToggled), false);
            FontSize = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(FontSize), 0.0);
            CurrentPageOn = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(CurrentPageOn), "main");
            ColorScheme = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(ColorScheme), "Red");
        }

        public async Task InitializeAudioPlayerAsync()
        {
            if (buttonSound == null)
            {
                Stream soundFile = await FileSystem.OpenAppPackageFileAsync("tap.wav");
                buttonSound = AudioManager.Current.CreatePlayer(soundFile);
            }
        }

        public void PlayButtonSound() => buttonSound?.Play();

        void NotifyDependentProperties(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames) OnPropertyChanged(propertyName);
        }

        public void Reset()
        {
            BillAmount = "0";
            AmountDiners = 1;
            AmountDecimalNumbers = 0;
            DecimalButtonPressed = false;

            NotifyDependentProperties(nameof(BillAmount), nameof(TipAmount), nameof(Total), nameof(SplitAmount));
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void HandleDecimalInput()
        {
            if (DecimalButtonPressed) return;

            DecimalButtonPressed = true;
            BillAmount += ".";
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
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(PlaySound), playSound);
                }
            }
        }
    }
}