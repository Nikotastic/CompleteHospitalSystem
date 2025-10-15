# HospitalCompleteSystem

Hospital management system developed in C# (.NET 8).

## Description
This project allows the management of patients, doctors, and medical appointments in a console environment. It includes robust validations for data such as email and phone, and simulates the sending of appointment confirmation emails.

## Project Structure
```
HospitalCompleteSystem/
├── Data/
├── Docs/
├── Interfaces/
├── Menus/
├── Models/
├── Repository/
├── Services/
├── Utils/
├── Program.cs
├── HospitalCompleteSystem.csproj
```
- **Data/**: Simulated database.
- **Docs/**: Class and use case diagrams.
- **Interfaces/**: Interfaces for repositories and services.
- **Menus/**: Console interaction menus.
- **Models/**: Main entities (Patient, Doctor, Appointment, etc.).
- **Repository/**: Data access repositories.
- **Services/**: Business logic and email simulation.
- **Utils/**: Utilities and validations.

## Installation
1. Requires .NET 8 SDK.
2. Clone the repository and open the folder in your IDE.
3. Run:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project HospitalCompleteSystem/HospitalCompleteSystem.csproj
   ```

## Usage
- Run the program and navigate through the menus to manage patients, doctors, and appointments.
- The system validates emails and phones in all forms.
- Email sending is simulated in the console.

## Requirements
- .NET 8 SDK
- Compatible operating system (Windows, Linux, Mac)

## Diagrams
- Class and use case diagrams are located in the `Docs/` folder.

## License
This project is licensed under the MIT License.

## Author
Developed Nikol Velasquez.
