# TipCalculatorPart2

## Project Overview

## Motivation

## Goal

## Features

### New Functionalities  
- Preferences screen added for customization:  
  - Toggle between light and dark mode.  
  - Choose button background color.  
  - Enable or disable button clicking sound.  
  - Resumes where the user last left off.  

### Enhanced User Experience

## Architecture

### Updates
Encapsulated UI property change notifications via the INotifyPropertyChanged interface to ensure synchronization of dependent properties across the app. Streamlined audio setup, playback logic and preference handling within the model. Established dynamic navigation using the CurrentPageOn property for seamless resumption of the last visited page. Achieved modularity through consolidated state management and centralized cascading changes. Enhanced user customization by integrating preferences, adding properties for themes, colors and navigation.

### New Components
Needs to exclusively focus on how the newly introduced elemengts integrated into the overall architecture.

## Tech Stack

### Frameworks and Libraries  
- **Syncfusion.Maui.Sliders (29.1.38)**: Slider component for tip percentage selection.  
- **Microsoft.Maui.Controls (9.0.60)**: UI framework for building user interfaces in the .NET MAUI application.  
- **Microsoft.Extensions.Logging.Debug (9.0.4)**: Enhanced logging for debugging during development.  
- **Microsoft.Maui.Essentials (9.0.60)**: Provides cross-platform APIs for device features such as preferences.  
- **Plugin.Maui.Audio (3.1.1)**: Audio plugin for playing and managing sound in the application.  

### Tools  
- **Visual Studio Code (1.99.0)**: For writing and refining the README file.  
- **Microsoft Visual Studio Community 2022 (17.13.5)**: IDE used for creating and debugging the application.  
- **Git (2.45.1.windows.1)**: Version control system for managing code changes and tracking project history.  
- **GitHub**: Remote repository platform for storing the project and enabling centralized access.

## Installation and Usage

### Pre-requisites
1. Ensure the following are installed on your system:  
   - [.NET SDK](https://dotnet.microsoft.com/download/dotnet) (version 6.0 or later).  
   - Visual Studio 2022 Community Edition or higher with the .NET MAUI workload installed.  
   - Git (version 2.45 or later) for version control.

2. Clone the repository from GitHub:  
   ```bash
   git clone https://github.com/Learner062022/TipCalculator.git
   ```

### Step-by-step Instructions
- **Open the Project**  
  - Launch Visual Studio 2022 Community Edition.  
  - Open the cloned project by navigating to the folder containing the `.csproj` file.  

- **Build the Solution**  
  - In Visual Studio, navigate to *Build* > *Build Solution* (or use the shortcut `Ctrl + Shift + B`).  

- **Run the Application**  
  - Click the *Start* button or press `F5` to debug and run the application.  
  - The app will launch in the specified simulator/emulator or on your connected physical device.  

### Troubleshooting
- **Common Errors**  
  - **Missing Dependencies**  
    Ensure all required NuGet packages are restored by running:  
    ```bash
    dotnet restore
    ```

  - **Build Errors**  
    Confirm that all project references and bindings are correct in the `.csproj` file.  

- **Simulator Issues**  
  - Restart your development environment if the emulator/simulator fails to load.  
  - For physical devices, ensure developer mode is enabled.  

## Troubleshooting

## Testing and Validation

### Automated Testing

### Edge Cases

## Limitations

## Future Improvements

## Contributing

### Guidelines
- Adhere to [Google's Style Guides](https://google.github.io/styleguide/) or [Microsoft's C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) for clean and maintainable code.
- Provide detailed descriptions of changes and link related issues in all pull requests.

### Branching Strategy
- Follow [Conventional Commits](https://www.conventionalcommits.org/) for commit message formatting. Use branch naming conventions such as:
  - `feature/<description>`: For new features.
  - `fix/<description>`: For bug fixes.
  - `hotfix/<description>`: For critical fixes.
- Refer to [Microsoft's Branching Guidance](https://learn.microsoft.com/en-us/azure/devops/repos/git/git-branching-guidance?view=azure-devops) or [GitKraken's Git Branching Strategies](https://www.gitkraken.com/learn/git/best-practices/git-branch-strategy) for workflows:
  - Branch off from the latest `main`.
  - Regularly sync branches with `main` and ensure thorough testing prior to pull requests.

### Pull Requests
- Submit all pull requests to the `main` branch following [GitHub's Pull Request Guide](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/getting-started/about-pull-requests).
- Ensure new features or updates are well-documented. Refer to [Atlassian's Pull Request Guide](https://www.atlassian.com/blog/git/written-unwritten-guide-pull-requests) for best practices.

## Known Issues
- Equal bill splitting only; uneven splits or additional charges per diner are not supported.
- Certain inputs, such as excessively large numbers, may not be handled gracefully.
- Limited testing across unique device configurations may result in UI inconsistencies.

## License
The project is licensed under the [GPL-3.0 License](https://www.gnu.org/licenses/gpl-3.0.en.html).