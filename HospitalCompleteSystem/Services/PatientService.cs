using HospitalCompleteSystem.Data;
using HospitalCompleteSystem.Models;
using HospitalCompleteSystem.Repository;

namespace HospitalCompleteSystem.Services;

public class PatientService
{
    // Repository instance for patient operations
    private readonly PatientRepository _repo = new PatientRepository();
    
    // Method to add a new patient with input validation
    public void AddPatient()
    {
        Console.WriteLine("=== Add New Patient ===");
        var name = Exceptions.GetStringOrCancel("Name");
        if (name == null) return;

        int age;
        bool isValid;

        do
        {
            Console.Write("Enter Age (must be a non-negative integer): ");
            string input = Console.ReadLine(); 


            isValid = int.TryParse(input, out age) && age > 0;

            if (!isValid)
            {
                Console.WriteLine("Invalid input. Age must be a non-negative whole number.");
            }
        } while (!isValid); 
        
            
        Console.Write("Enter Identification: ");
        string identification = Console.ReadLine() ?? string.Empty;
        
        // Validate that the ID is unique
        if (Database.Patients.Any(p => p.Identification == identification))
        {
            Console.WriteLine("Error: A patient with that ID already exists. Registration is not possible.");
            return;
        }

        string? phone;
        do
        {
            phone = Exceptions.GetStringOrCancel("Phone");
            if (phone == null) return;

            if (!phone.All(char.IsDigit) || phone.Length < 7)
            {
                Console.WriteLine("Invalid phone number. Must contain only digits and at least 7 numbers.");
                phone = null;
            }
        } while (phone == null);

        string? email;
        do
        {
            email = Exceptions.GetStringOrCancel("Email");
            if (email == null) return;

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Console.WriteLine("Invalid email format. Example: example@mail.com");
                email = null;
            }
        } while (email == null);
        
        try
        {
            var patient = new Patient( name, age, identification, phone, email);
            _repo.AddPatient(patient);

            Console.WriteLine($"\nClient added successfully with ID: {patient.Id}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while registering client: {e.Message}");
        }
    }

    // Static method to get a patient by ID
    public static Patient? GetPatientById(int id)
    {
      return  Database.Patients.FirstOrDefault(p => p.Id == id);
    }
    
    // Method to search for a patient by ID and display their information
    public void SearchPatientById()
    {
        Console.Clear();
        Console.WriteLine("=== Search Patient by ID ===");
        var idInput = Exceptions.GetStringOrCancel("Enter patient ID");
        if (idInput == null) return;
        if (!int.TryParse(idInput, out int id))
        {
            Console.WriteLine("\nID invalid. It must be a number");
            return;
        }
        var patient = GetPatientById(id);
        if (patient == null)
        {
            Console.WriteLine("\nPatient not found.");
            return;
        }
        Console.WriteLine($"\nPatient data: {patient}");
    }
    
    // Method to list all patients
    public void ListPatients()
    {
        Console.Clear();
        Console.WriteLine("=== List of Patients ===");
        if (Database.Patients.Count == 0)
        {
            Console.WriteLine("No patients registered yet.");
            return;
        }
        foreach (var patient in Database.Patients)
        {
            Console.WriteLine(patient);
        }
    }

    // Method to update an existing patient's information
    public void UpdatePatient()
    {
        Console.Clear();
        Console.WriteLine("=== Update Patient ===");
        var idInput = Exceptions.GetStringOrCancel("Enter client ID");
        if (idInput == null) return;
        if (!int.TryParse(idInput, out int id))
        {
            Console.WriteLine("\nID invalid. It must be a number");
            return;
        }
        var patient = GetPatientById(id);
        if (patient == null)
        {
            Console.WriteLine("\nClient not found.");
            return;
        }
        Console.WriteLine($"\nCurrent data: {patient}");
        Console.WriteLine("\nInsert new data (or 'cancel' to exit, presiona Enter para mantener el valor actual):");

        // Name
        var newName = Exceptions.GetStringOrCancel($"Name ({patient.Name})", allowEmpty: true);
        if (newName == null) return;
        if (!string.IsNullOrWhiteSpace(newName))
            patient.UpdateName(newName);

        // Age
        var newAgeInput = Exceptions.GetStringOrCancel($"Age ({patient.Age})", allowEmpty: true);
        if (newAgeInput == null) return;
        if (!string.IsNullOrWhiteSpace(newAgeInput) && int.TryParse(newAgeInput, out int newAge) && newAge > 0)
            patient.UpdateAge(newAge);

        // Identification
        var newIdentification = Exceptions.GetStringOrCancel($"Identification ({patient.Identification})", allowEmpty: true);
        if (newIdentification == null) return;
        if (!string.IsNullOrWhiteSpace(newIdentification) && newIdentification != patient.Identification)
        {
            if (Database.Patients.Any(p => p.Identification == newIdentification && p.Id != patient.Id))
            {
                Console.WriteLine("Error: A patient with that ID already exists. Cannot update.");
                return;
            }
            patient.UpdateIdentification(newIdentification);
        }

        // Phone
        var newPhone = Exceptions.GetStringOrCancel($"Phone ({patient.Phone})", allowEmpty: true);
        if (newPhone == null) return;
        // Validate phone number, must contain only digits and at least 7 numbers, all characters are digits
        if (!string.IsNullOrWhiteSpace(newPhone) && newPhone.All(char.IsDigit) && newPhone.Length >= 7)
            patient.UpdatePhone(newPhone);

        // Email
        var newEmail = Exceptions.GetStringOrCancel($"Email ({patient.Email})", allowEmpty: true);
        // Validate email format, Match the email format using a regular expression
        if (newEmail == null) return;
        if (!string.IsNullOrWhiteSpace(newEmail) && System.Text.RegularExpressions.Regex.IsMatch(newEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            patient.UpdateEmail(newEmail);

        Console.WriteLine("\nData updated correctly.");
    }
    
    // Method to delete a patient by ID with confirmation
    public void DeletePatient()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Patient ===");
        var idInput = Exceptions.GetStringOrCancel("Enter patient ID to delete");
        if (idInput == null) return;
        if (!int.TryParse(idInput, out int id))
        {
            Console.WriteLine("\nID invalid. It must be a number");
            return;
        }
        var patient = GetPatientById(id);
        if (patient == null)
        {
            Console.WriteLine("\nPatient not found.");
            return;
        }
        Console.WriteLine($"\nPatient data: {patient}");
        Console.Write("\nAre you sure you want to delete this patient? (y/n): ");
        string confirmation = Console.ReadLine()!.Trim().ToLower();
        if (confirmation == "y")
        {
            _repo.DeletePatient(id);
            Console.WriteLine("\nPatient deleted successfully.");
        }
        else
        {
            Console.WriteLine("\nDeletion cancelled.");
        }
    }
    
}
