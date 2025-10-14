using HospitalCompleteSystem.Data;
using HospitalCompleteSystem.Interfaces;
using HospitalCompleteSystem.Models;
using HospitalCompleteSystem.Repository;

namespace HospitalCompleteSystem.Services;

public class PatientService : IPatientService
{
    private readonly PatientRepository _repo = new PatientRepository();
    
    public void AddPatient()
    {
        Console.Clear();
        Console.WriteLine("=== Add New Patient ===");
        try
        {
            var name = Exceptions.GetStringOrCancel("Name");
            if (name == null) return;

            var ageInput = Exceptions.GetStringOrCancel("Age");
            if (ageInput == null) return;
            if (!int.TryParse(ageInput, out int age) || age <= 0)
            {
                Console.WriteLine("Error: Age must be a positive number.");
                return;
            }

            var identification = Exceptions.GetStringOrCancel("Identification");
            if (identification == null) return;

            if (!Database.IdentificationUnique(identification))
            {
                Console.WriteLine("Error: That identification already exists in the system.");
                return;
            }

            var phone = Exceptions.GetStringOrCancel("Phone");
            if (phone == null) return;

            var email = Exceptions.GetStringOrCancel("Email");
            if (email == null) return;

            try
            {
                var patient = new Patient(name, age, identification, phone, email);
                _repo.AddPatient(patient);
                Console.WriteLine($"\nPatient added successfully with ID: {patient.Id}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    public static Patient? GetPatientById(int id)
    {
        return Database.Patients.FirstOrDefault(p => p.Id == id);
    }
    
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

    public void UpdatePatient()
    {
        Console.Clear();
        Console.WriteLine("=== Update Patient ===");
        try
        {
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
            Console.WriteLine($"\nCurrent data: {patient}");
            Console.WriteLine("\nInsert new data (or 'cancel' to exit, press Enter to keep current value):");

            var newName = Exceptions.GetStringOrCancel($"Name ({patient.Name})", allowEmpty: true);
            if (newName == null) return;

            var newAgeInput = Exceptions.GetStringOrCancel($"Age ({patient.Age})", allowEmpty: true);
            if (newAgeInput == null) return;
            int? newAge = null;
            if (!string.IsNullOrWhiteSpace(newAgeInput))
            {
                if (!int.TryParse(newAgeInput, out int parsedAge) || parsedAge <= 0)
                {
                    Console.WriteLine("Error: Age must be a positive number.");
                    return;
                }
                newAge = parsedAge;
            }

            var newIdentification = Exceptions.GetStringOrCancel($"Identification ({patient.Identification})", allowEmpty: true);
            if (newIdentification == null) return;
            if (!string.IsNullOrWhiteSpace(newIdentification) && newIdentification != patient.Identification)
            {
                if (!Database.IdentificationUnique(newIdentification, excludePatientId: patient.Id))
                {
                    Console.WriteLine("Error: That identification already exists in the system.");
                    return;
                }
            }

            var newPhone = Exceptions.GetStringOrCancel($"Phone ({patient.Phone})", allowEmpty: true);
            if (newPhone == null) return;

            var newEmail = Exceptions.GetStringOrCancel($"Email ({patient.Email})", allowEmpty: true);
            if (newEmail == null) return;

            try
            {
                patient.UpdatePatientData(
                    string.IsNullOrWhiteSpace(newName) ? patient.Name : newName,
                    string.IsNullOrWhiteSpace(newIdentification) ? patient.Identification : newIdentification,
                    newAge,
                    string.IsNullOrWhiteSpace(newPhone) ? patient.Phone : newPhone,
                    string.IsNullOrWhiteSpace(newEmail) ? patient.Email : newEmail
                );
                Console.WriteLine("\nData updated correctly.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
    
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
        
        var confirm = Exceptions.GetStringOrCancel("Are you sure you want to delete this patient? (y/n):");
        if (confirm != null && confirm.Trim().ToLower() == "y")
        {
            if (_repo.DeletePatient(id))
                Console.WriteLine("\nPatient deleted successfully.");
            else
                Console.WriteLine("\nError deleting patient.");
        }
        else
        {
            Console.WriteLine("\nDeletion cancelled.");
        }
    }
}
