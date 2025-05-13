# TipCalculatorPart2  

## Project Overview  
TipCalculator is a user-centric application designed to **simplify tip calculation** while offering **dynamic customization** and **intuitive navigation**. Users can personalize their experience with **light/dark mode toggling, button color customization and adjustable font sizes** to enhance accessibility. The app ensures **seamless page resumption**, providing a **cohesive and adaptable interface**. Built with **modular state management** and **efficient property synchronization**, it maintains **responsiveness and scalability** across devices.  

## Motivation & Goal  
The project aims to enhance user experience through **seamless navigation, intuitive customization and personalized settings**, improving **accessibility, engagement and usability**. The objective is to create a **functional yet adaptable interface** that integrates customization and usability.  

## Features  

### Customization Options  
- **Preferences screen** added for user settings:  
  - Toggle **light/dark mode** for personalized themes.  
  - Customize **button background colors**.  
  - Enable or disable **click sound effects**.  
  - Resume **previous session state** seamlessly.  
  - Adjust **font sizes** for readability.  

## Architecture & Tech Stack  

### Updates  
- Implemented **INotifyPropertyChanged** for UI property synchronization.  
- Streamlined **audio setup**, playback logic and preference handling.  
- Established **dynamic navigation** via `CurrentPageOn` for seamless session resumption.  
- Centralized **state management** for modularity and cascading updates.  

### Frameworks & Libraries  
- **Syncfusion.Maui.Sliders (29.1.38)** – Slider component for tip percentage selection.  
- **Microsoft.Maui.Controls (9.0.60)** – UI framework for building user interfaces in .NET MAUI.  
- **Microsoft.Extensions.Logging.Debug (9.0.4)** – Enhanced debugging tools.  
- **Microsoft.Maui.Essentials (9.0.60)** – Cross-platform APIs for device preferences.  
- **Plugin.Maui.Audio (3.1.1)** – Audio management.  

### Tools  
- **Visual Studio Code (1.99.0)** – Documentation editing.  
- **Visual Studio Community 2022 (17.13.5)** – Development environment.  
- **Git (2.45.1.windows.1)** – Version control.  
- **GitHub** – Remote repository hosting.  

## Installation and Usage

### Pre-requisites
1. Ensure the following are installed on your system:  
   - [.NET SDK](https://dotnet.microsoft.com/download/dotnet) (version 6.0 or later).  
   - Visual Studio 2022 Community Edition or higher with the .NET MAUI workload installed.  
   - Git (version 2.45 or later) for version control.

2. Clone the repository from GitHub:  
   ```bash
   git clone https://github.com/Learner062022/TipCalculatorPart2.git
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

## Troubleshooting & Debugging  
Used manual debugging techniques, including:  
- Setting breakpoints to inspect execution flow.  
- Using `Debug.WriteLine()` statements to monitor variable states.  
- Analyzing the stack trace to resolve issues efficiently.  

## Testing & Validation  
Performed **functional testing** to verify key features, including **preference management, navigation and UI customization**. No formalized automated testing methods were used beyond validating expected behavior through manual execution.  

## Limitations  
- **Restricted background color customization** – Applies only to buttons, with three available colors.  
- **Font size adjustments** – Limited to controls that support the `FontSize` property.  
- **Fixed font size range** – Minimum and maximum values are predefined.  
- **Static layout** – Does not dynamically adapt to screen size or orientation.  

## Future Improvements  
- Optimize **memory allocation** and **resource management** to resolve performance warnings.  
- Implement **automatic preference saving** on app closure.  
- Use a **single instantiated model** across the app instead of separate instances in `App` and `MainPage`.  

## Contributing  

### Guidelines  
- Follow [Google's Style Guides](https://google.github.io/styleguide/) or [Microsoft's C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) for clean and maintainable code.  
- Provide detailed descriptions of changes and link related issues in all pull requests.  

### Branching Strategy  
- Use **Conventional Commits** ([guide](https://www.conventionalcommits.org/)) for commit formatting.  
- Follow structured **branch naming conventions** based on industry best practices, referencing [Microsoft's Git Branching Guidance](https://learn.microsoft.com/en-us/azure/devops/repos/git/git-branching-guidance?view=azure-devops).  
- Always branch off the latest `main`, sync regularly and test thoroughly before submitting pull requests.   

### Pull Requests  
- Submit all pull requests to the `main` branch per [GitHub's Pull Request Guide](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/getting-started/about-pull-requests).  
- Ensure features and updates are documented, following [Atlassian's PR Best Practices](https://www.atlassian.com/blog/git/written-unwritten-guide-pull-requests).  

## Known Issues  
- **Limited bill splitting** – Only supports equal splits, no per-diner adjustments.  
- **Handling of large inputs** – Certain excessively large values may not be processed correctly.  
- **UI inconsistencies** – Limited testing across different device configurations.  

## License  
This project is licensed under the [GPL-3.0 License](https://www.gnu.org/licenses/gpl-3.0.en.html). 