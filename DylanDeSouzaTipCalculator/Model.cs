using Plugin.Maui.Audio;
using System.ComponentModel;
using System.Diagnostics;
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
        bool playSound;
        double fontSize;
        string? currentPageOn;
        IAudioPlayer? buttonSound;
        Color? selectedBackgroundColor;

        static readonly List<string> colorChoices = ["Red", "Green", "Blue"];

        public string BillAmount
        {
            get => billAmount;
            set => UpdateField(ref billAmount, value, nameof(TipAmount), nameof(Total), nameof(SplitAmount));
        }

        public int AmountDiners
        {
            get => amountDiners;
            set
            {
                UpdateField(ref amountDiners, value, nameof(SplitAmount));
                NotifyPropertyChanged(nameof(AmountDiners));
                Microsoft.Maui.Storage.Preferences.Default.Set(nameof(AmountDiners), amountDiners); 
            } 
        }

        public void PlayButtonSound() => buttonSound?.Play();

        public double TipPercentage
        {
            get => tipPercentage;
            set
            {
                if (tipPercentage != value)
                {
                    tipPercentage = value;
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(TipPercentage), tipPercentage);
                    NotifyMultiplePropertiesChanged(nameof(TipAmount), nameof(Total), nameof(SplitAmount));
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

        public string? ColorChoice
        {
            get => colorChoice;
            set
            {
                if (colorChoice != value)
                {
                    colorChoice = value;
                    Microsoft.Maui.Storage.Preferences.Default.Set(nameof(ColorChoice), value);
                    NotifyPropertyChanged(nameof(ColorChoice)); 
                    NotifyPropertyChanged(nameof(SelectedBackgroundColor)); 
                }
            }
        }

        public Color? SelectedBackgroundColor
        {
            get => selectedBackgroundColor;
            set
            {
                if (selectedBackgroundColor != value)
                {
                    selectedBackgroundColor = value;
                    NotifyPropertyChanged();
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

        public string? CurrentPageOn
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
            NotifyMultiplePropertiesChanged(nameof(BillAmount), nameof(TipAmount), nameof(Total), nameof(SplitAmount));
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

        public async Task InitializeAudioPlayerAsync() =>
            buttonSound ??= AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("tap.wav"));

        public void LoadMainPagePreferences()
        {
            billAmount = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(BillAmount), "0");
            amountDiners = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(AmountDiners), 1);
            tipPercentage = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(TipPercentage), 0.0);
            decimalButtonPressed = billAmount.Contains('.');
            amountDecimalNumbers = decimalButtonPressed ? billAmount.Length - (billAmount.IndexOf('.') + 1) : 0;
            ColorChoice = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(ColorChoice), "Red");
            SelectedBackgroundColor = Color.Parse(ColorChoice);
            NotifyMultiplePropertiesChanged(nameof(BillAmount), nameof(AmountDiners), nameof(TipPercentage), nameof(decimalButtonPressed), nameof(amountDecimalNumbers));
        }

        public void LoadPreferencesPageSettings()
        {
            LoadPreferenceValues();
            NotifyMultiplePropertiesChanged(nameof(PlaySound), nameof(ThemeIsToggled), nameof(FontSize), nameof(ColorChoice), nameof(SelectedBackgroundColor));
        }

        void LoadPreferenceValues()
        {
            PlaySound = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(PlaySound), false);
            ThemeIsToggled = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(ThemeIsToggled), true);
            FontSize = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(FontSize), 14.0);
            ColorChoice = Microsoft.Maui.Storage.Preferences.Default.Get(nameof(ColorChoice), "Red");
            SelectedBackgroundColor = Color.Parse(ColorChoice);
        }            

        void UpdateField<T>(ref T field, T value, params string[] dependentProperties)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                NotifyPropertyChanged();
                NotifyMultiplePropertiesChanged(dependentProperties);
            }
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