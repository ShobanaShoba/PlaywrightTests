# PlaywrightTests

This repository contains **Playwright automation tests** for the Snipe-IT demo application using **C# and NUnit**. The project demonstrates end-to-end UI testing including asset creation, verification, and history checks.

---

## Project Structure
PlaywrightTests/             # Root folder of the project
├─ Pages/                    # Page Object Model (POM) classes
│   ├─ LoginPage.cs
│   ├─ ListPage.cs
│   └─ HistoryPage.cs
│
├─ TestData/                 # Constants and test data
│   └─ TestData.cs
│
├─ Tests/                    # NUnit test classes
│   ├─ LoginTest.cs
│   ├─ HistoryTest.cs
│   └─ AssetTest.cs
│
├─ Utils/                    # Helper classes (e.g., screenshot utils)
│   └─ ScreenshotHelper.cs
│
├─ .gitignore                # To exclude unnecessary files/folders
├─ PlaywrightTests.csproj     # Project file
└─ README.md                 # Project description and instructions






---

## Test Scenarios

- Login to the Snipe-IT demo application
- Create a new MacBook Pro 13" asset
- Verify asset creation in the asset list
- Navigate and check asset history
- Capture screenshots on test failure

>  Note: Some asset creation forms are **dynamic and may change**. Update `TestData` constants before running tests.

---

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code
- [Playwright CLI](https://playwright.dev/dotnet/docs/intro)

### Setup

1. Clone the repository:
```bash
git clone https://github.com/ShobanaShoba/PlaywrightTests.git
cd PlaywrightTests

Install Playwright dependencies:dotnet tool install --global Microsoft.Playwright.CLI
playwright install

Running Tests
Run all tests using the .NET CLI:
dotnet test
ests will automatically capture screenshots if a test fails and save them in the Screenshots/ folder.

Notes
Avoid committing large files in bin/ or .playwright/.
Update TestData values if Snipe-IT demo changes dynamically.
For GitHub, large files are not recommended. Use .gitignore to exclude them.
