# 🚀 PlaywrightBDD.Framework
[![Playwright BDD CI](https://github.com/PathakRitesh21/PlaywrightBDD.Framework/actions/workflows/main.yml/badge.svg)](https://github.com/PathakRitesh21/PlaywrightBDD.Framework/actions/workflows/main.yml)

A modern **Automation Testing Framework** built with:

- ✅ **C# (.NET)**
- ✅ **Playwright** for UI automation
- ✅ **BDD (Reqnroll)** for readable Gherkin scenarios
- ✅ **NUnit** as test runner
- ✅ **Data-Driven Testing** using Excel
- ✅ **Screenshot Utility** for evidence & debugging

---

## 📌 Features

- 🧪 BDD style tests using **Gherkin** (`.feature` files)
- 🌐 UI automation with **Microsoft Playwright**
- 📊 Data-driven tests using **Excel (ClosedXML)**
- 📸 **Reusable Screenshot Utility**
  - Take screenshots from steps or hooks
  - Automatic screenshots on test failure
- 🧱 Clean architecture:
  - Pages (Page Object Model)
  - Step Definitions
  - Hooks
  - Utilities
- ⚙️ Easily extendable for CI/CD (GitHub Actions, Azure DevOps, etc.)

---

## 🛠 Tech Stack

- **.NET** (net10.0)
- **Microsoft.Playwright**
- **Reqnroll** (BDD)
- **NUnit**
- **ClosedXML** (Excel reading)

---

## 📂 Project Structure

PlaywrightBDD.Framework
│
├── Config
│ └── appsettings.json
│
├── Drivers
│ ├── Hooks.cs
│ └── PlaywrightDriver.cs
│
├── Features
│ ├── LoginGtplBank.feature
│ └── LoginWithExcel.feature
│
├── Pages
│ └── GtplLoginPage.cs
│
├── StepDefinitions
│ ├── LoginSteps.cs
│ └── LoginWithExcelSteps.cs
│
├── TestData
│ └── credentials.xlsx
│
├── Utils
│ ├── ExcelUtils.cs
│ └── ScreenshotUtils.cs
│
├── Screenshots
│ └── (Auto-generated screenshots on failure / usage)
│
└── PlaywrightBDD.Framework.csproj

