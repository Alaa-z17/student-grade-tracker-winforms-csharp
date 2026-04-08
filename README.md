# 📚 Student Grade Tracker

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Windows Forms](https://img.shields.io/badge/Windows_Forms-WinForms-0078D4?logo=windows)](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Status](https://img.shields.io/badge/status-stable-brightgreen)]()

A **Windows Forms desktop application** for managing student grades, subjects, and generating reports. Features encrypted JSON storage, CSV import/export, auto‑save, and a rich UI with grade color coding.

> 🎥 **Demo Video**  
> [▶️ Watch on YouTube](https://www.youtube.com/watch?v=your-video-id)  

---

## 📸 Screenshot

![Main Window](assets/Main.png)

---

## ✨ Key Features

| Category | Features |
|----------|----------|
| **Student Management** | Add, edit, delete students and their grades per subject |
| **Grade Calculation** | Automatic average + letter grade (A–F) based on 0–100 scale |
| **CSV Import/Export** | Bulk add students via CSV (Student Name, Subject, Grade) |
| **Encrypted Storage** | AES‑256 encrypted `students.json` (keys from `appsettings.json`) |
| **Auto‑Save** | Saves every 30 sec (toggle on/off from View menu) |
| **Filter & Sort** | Search by name, sort by ID, name, average, or letter grade |
| **Subject Breakdown** | Tree view with subjects and individual grades per student |
| **Reports** | Tabular report with customizable font and copy‑able text |
| **Grade Colors** | Customize background colors for each letter grade |
| **System Tray** | Auto‑save notifications |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/en-us/download) (or later)
- Windows 10/11 (WinForms)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/student-grade-tracker.git
   cd student-grade-tracker
   ```

2. **Build the project**
   ```bash
   dotnet build
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```
   > Alternatively, open the `.csproj` in Visual Studio 2022+ and press **F5**.

---

## ⚙️ Configuration

Encryption settings are stored in `appsettings.json` (copied to output directory).  
⚠️ **Do not share your keys publicly** – they are for local use only.

```json
{
  "Encryption": {
    "Key": "layOAUX3n+sGq7VUD9y0KP4YuvvrdZLlQaWKFnqeisY=",
    "IV": "KfXqa2/eD92Z18KB1Cm3Og=="
  },
  "AppSettings": {
    "AutoSaveIntervalMs": 30000,
    "DataFile": "students.json"
  }
}
```

### Generating New Secure Keys

Run this snippet in a C# console app to generate a new key and IV:

```csharp
using System.Security.Cryptography;
using System;

var aes = Aes.Create();
aes.GenerateKey();
aes.GenerateIV();

Console.WriteLine($"\"Key\": \"{Convert.ToBase64String(aes.Key)}\",");
Console.WriteLine($"\"IV\": \"{Convert.ToBase64String(aes.IV)}\"");
```

---

## 📁 CSV Format (Import/Export)

The CSV must have the following **case‑insensitive** headers:

| Student Name | Subject | Grade |
|--------------|---------|-------|
| Emma Johnson | Math    | 85    |
| Liam Smith   | English | 92    |

- Grades must be numbers between **0 and 100**.
- Strings containing commas are automatically quoted.
- UTF‑8 encoding with or without BOM is fully supported.

### Example import file (`students.csv`)

```csv
Student Name,Subject,Grade
Emma Johnson,Math,85
Liam Smith,English,92
Olivia Williams,Science,78
```

---

## 🛠️ Technologies Used

- **.NET 10.0** – Windows Forms
- **C#** – Language
- **AES‑256** – Encryption (`System.Security.Cryptography`)
- **Microsoft.Extensions.Configuration.Json** – App settings
- **System.Text.Json** – JSON serialization

---

## 🧪 Usage Tips

- **Right‑click** a student in the list to edit grades or delete.
- **Double‑click** to quickly add/edit grades.
- **Search** box filters students by name in real‑time.
- **Auto‑save** can be toggled from `View → Auto‑Save`.
- **Grade colors** can be changed via `Edit → Grade Colors`.

---

## 📈 Future Improvements

- [ ] Student ID auto‑increment persistence across sessions
- [ ] Export reports as PDF
- [ ] Grade weighting per subject
- [ ] Backup / restore functionality
- [ ] Dark mode theme

---

## 🤝 Contributing

Contributions are welcome!  
1. Fork the project  
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)  
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)  
4. Push to the branch (`git push origin feature/AmazingFeature`)  
5. Open a Pull Request  

For major changes, please open an issue first to discuss what you would like to change.

---

## 📄 License

Distributed under the **MIT License**. See `LICENSE` file for more information.

---

## 📧 Contact

Your Name – [alaaalkatshah.email@example.com](mailto:alaaalkatshah@gmail.com)  
Project Link: [https://github.com/yourusername/student-grade-tracker](https://github.com/Alaa-z17/student-grade-tracker)
