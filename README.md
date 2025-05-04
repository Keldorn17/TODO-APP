
# <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#e3e3e3"><path d="M200-200v-560 454-85 191Zm0 80q-33 0-56.5-23.5T120-200v-560q0-33 23.5-56.5T200-840h560q33 0 56.5 23.5T840-760v320h-80v-320H200v560h280v80H200Zm494 40L552-222l57-56 85 85 170-170 56 57L694-80ZM320-440q17 0 28.5-11.5T360-480q0-17-11.5-28.5T320-520q-17 0-28.5 11.5T280-480q0 17 11.5 28.5T320-440Zm0-160q17 0 28.5-11.5T360-640q0-17-11.5-28.5T320-680q-17 0-28.5 11.5T280-640q0 17 11.5 28.5T320-600Zm120 160h240v-80H440v80Zm0-160h240v-80H440v80Z"/></svg> TODO-APP

A clean and modern TODO application developed in C# using WPF and the MVVM design pattern. Built as a university project, this application helps users manage their tasks efficiently with an intuitive UI and essential task management features.

## 🧩 Table of Contents

- [Features](#-features)
- [Technologies](#-technologies)
- [Getting Started](#-getting-started)
- [Project Structure](#-project-structure)
- [Backend](#-backend)
- [Release](#-release)
- [Contributing](#-contributing)
- [Credits](#-credits)

---

## ✨ Features

- ✅ Create, edit, and delete tasks
- 📅 Due date selection
- 🟢 Priority tagging (e.g., High, Medium, Low)
- 🔄 Mark tasks as complete/incomplete
- 💾 Persistent storage (tasks saved across sessions)
- 🖥️ Responsive and clean UI using WPF
- 🔁 MVVM architecture with separation of concerns

---

## 🛠️ Technologies

- **Language**: C#
- **Framework**: .NET 8 
- **UI**: WPF (Windows Presentation Foundation)
- **Architecture**: MVVM (Model-View-ViewModel)
- **IDE**: Visual Studio 2022

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (8.0)
- Visual Studio 2022 with WPF support

### Installation

1. **Open the Project**  
Open the `.sln` file in Visual Studio.


2. **Build & Run**
    - Restore NuGet packages if prompted.
    - Build the solution.
    - Press `F5` or click **Start**.

---

## 📁 Project Structure

```
TODO-APP/
├── .github/           # GitHub workflows and configurations
├── Client/            # Client-side resources and assets
├── Converter/         # Value converters for data binding
├── DTO/               # Data Transfer Objects
├── Domain/            # Core business logic and domain models
├── Exceptions/        # Custom exception classes
├── Mapper/            # Mapping configurations between models and DTOs
├── Model/             # Data models representing application entities
├── Provider/          # Data providers for accessing and managing data
├── Service/           # Services containing business logic
├── Themes/            # UI themes and styles
├── Utils/             # Utility classes and helper functions
├── View/              # XAML views for the UI
├── ViewModel/         # ViewModels for data binding and UI logic
├── App.xaml           # Application definition and resources
├── App.xaml.cs        # Application startup logic
├── AssemblyInfo.cs    # Assembly metadata
├── TODO.csproj        # Project file
├── TODO.sln           # Solution file
├── appsettings.json   # Application configuration settings
├── redirect.html      # Redirect page for deployment (if applicable)
└── README.md          # Project documentation

```

---

## 👨‍💻 Backend

- The backend of this application is powered by a microservice developed by [robotTX](https://github.com/robotTX1).
- You can check the code here: [MicroSpringTodoService](https://github.com/robotTX1/MicroSpringTodoService)

---

## 🚨 Release

### To download a released version of the app you can:
- Click on the `Releases` tab and download the latest working version in a zip.
- Then you can extract the files in a directory.
- Now you can search for the `Todo.exe` file and run it.

---

## 🤝 Contributing

We welcome suggestions, issues, and contributions! Here's how you can help:

1. Fork the repository
2. Create a new branch: `git checkout -b feature/yourFeature`
3. Commit your changes: `git commit -m "Added your feature"`
4. Push to the branch: `git push origin feature/yourFeature`
5. Open a Pull Request

---

## 🙌 Credits

Developed by [Keldorn17](https://github.com/Keldorn17), [robotTX](https://github.com/robotTX1) and [Turboblider55](https://github.com/Turboblider55) as part of a university coursework project.
