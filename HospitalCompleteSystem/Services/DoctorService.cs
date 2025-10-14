using HospitalCompleteSystem.Models;
using HospitalCompleteSystem.Repository;
using HospitalCompleteSystem.Interfaces;
using HospitalCompleteSystem.Data;
using HospitalCompleteSystem;
using System;
using System.Linq;

namespace HospitalCompleteSystem.Services;

public class DoctorService : IDoctorService
{
    private readonly DoctorRepository _repo = new DoctorRepository();

    // Method to add a new doctor with input validation
    public void AddDoctor()
    {
        Console.WriteLine("=== Add New Doctor ===");
        var name = Exceptions.GetStringOrCancel("Name");
        if (name == null || name.Length < 2)
        {
            Console.WriteLine("Error: Name must have at least 2 characters.");
            return;
        }

        var specialty = Exceptions.GetStringOrCancel("Specialty");
        if (specialty == null || specialty.Length < 2)
        {
            Console.WriteLine("Error: Specialty must have at least 2 characters.");
            return;
        }

        var identification = Exceptions.GetStringOrCancel("Identification");
        if (identification == null || identification.Length < 4)
        {
            Console.WriteLine("Error: Identification must have at least 4 characters.");
            return;
        }
        if (_repo.ExistsByIdentification(identification)) {
            Console.WriteLine("There is already a doctor with that document.");
            return;
        }

        var phone = Exceptions.GetStringOrCancel("Phone");
        if (phone == null || !phone.All(char.IsDigit) || phone.Length < 7)
        {
            Console.WriteLine("Error: Phone must contain only digits and at least 7 numbers.");
            return;
        }

        var email = Exceptions.GetStringOrCancel("Email");
        if (email == null || !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            Console.WriteLine("Error: Invalid email format. Example: example@mail.com");
            return;
        }

        try
        {
            var doctor = new Doctor(name, specialty, identification, phone, email);
            _repo.AddDoctor(doctor);
            Console.WriteLine($"Registered doctor with ID: {doctor.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error registering doctor: {ex.Message}");
        }
    }

    // Method to list all doctors
    public void ListDoctors()
    {
        Console.WriteLine("=== List of doctors ===");
        if (!Database.Doctors.Any()) { Console.WriteLine("There are no registered doctors."); return; }
        foreach (var doctor in Database.Doctors)
            Console.WriteLine(doctor);
    }

    // Method to list doctors by specialty
    public void ListDoctorsBySpecialty()
    {
        var specialty = Exceptions.GetStringOrCancel("Specialty to filter");
        if (specialty == null) return;
        var filtered = _repo.GetDoctorsBySpecialty(specialty);
        if (!filtered.Any()) { Console.WriteLine("There are no doctors with that specialty."); return; }
        foreach (var doctor in filtered)
            Console.WriteLine(doctor);
    }

    // Method to search for a doctor by ID
    public void SearchDoctorById()
    {
        var idInput = Exceptions.GetStringOrCancel("Doctor's ID");
        if (idInput == null || !int.TryParse(idInput, out int id)) { Console.WriteLine("Invalid ID."); return; }
        var doctor = _repo.GetDoctor(id);
        if (doctor == null) { Console.WriteLine("Doctor not found."); return; }
        Console.WriteLine(doctor);
    }

    // Method to update a doctor's information
    public void UpdateDoctor()
    {
        Console.Clear();
        Console.WriteLine("=== Update Doctor ===");
        var idInput = Exceptions.GetStringOrCancel("Enter doctor ID");
        if (idInput == null) return;
        if (!int.TryParse(idInput, out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }
        var doctor = _repo.GetDoctor(id);
        if (doctor == null) {
            Console.WriteLine("Doctor not found.");
            return;
        }
        Console.WriteLine($"Current data: {doctor}");
        Console.WriteLine("\nInsert new data (or 'cancel' to exit, press Enter to keep the current value):");

        var newName = Exceptions.GetStringOrCancel($"Name ({doctor.Name})", allowEmpty: true);
        if (newName == null) return;
        if (!string.IsNullOrWhiteSpace(newName) && newName.Length < 2)
        {
            Console.WriteLine("Error: Name must have at least 2 characters.");
            return;
        }

        var newSpecialty = Exceptions.GetStringOrCancel($"New specialty ({doctor.Specialty})", true);
        if (!string.IsNullOrWhiteSpace(newSpecialty) && newSpecialty.Length < 2)
        {
            Console.WriteLine("Error: Specialty must have at least 2 characters.");
            return;
        }

        var newIdentification = Exceptions.GetStringOrCancel($"New document ({doctor.Identification})", true);
        if (newIdentification == null) return;
        if (!string.IsNullOrWhiteSpace(newIdentification))
        {
            if (_repo.ExistsByIdentification(newIdentification) && newIdentification != doctor.Identification)
            {
                Console.WriteLine("There is already a doctor with that document.");
                return;
            }
            if (newIdentification.Length < 4)
            {
                Console.WriteLine("Error: Identification must have at least 4 characters.");
                return;
            }
        }

        var newPhone = Exceptions.GetStringOrCancel($"New phone({doctor.Phone})", true);
        if (!string.IsNullOrWhiteSpace(newPhone))
        {
            if (!newPhone.All(char.IsDigit) || newPhone.Length < 7)
            {
                Console.WriteLine("Error: Phone must contain only digits and at least 7 numbers.");
                return;
            }
        }

        var newEmail = Exceptions.GetStringOrCancel($"New email ({doctor.Email})", true);
        if (!string.IsNullOrWhiteSpace(newEmail))
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(newEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Console.WriteLine("Error: Invalid email format. Example: example@mail.com");
                return;
            }
        }

        try
        {
            // Update the doctor's data using the provided method
            doctor.UpdateDoctorData(
                string.IsNullOrWhiteSpace(newName) ? doctor.Name : newName,
                string.IsNullOrWhiteSpace(newIdentification) ? doctor.Identification : newIdentification,
                string.IsNullOrWhiteSpace(newPhone) ? doctor.Phone : newPhone,
                string.IsNullOrWhiteSpace(newEmail) ? doctor.Email : newEmail
            );
            // Update specialty if provided
            if (!string.IsNullOrWhiteSpace(newSpecialty))
                doctor.UpdateSpecialty(newSpecialty);
            _repo.UpdateDoctor(doctor);
            Console.WriteLine("Doctor updated correctly.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating doctor: {ex.Message}");
        }
    }

    // Method to delete a doctor by ID with confirmation
    public void DeleteDoctor()
    {
        Console.Clear();
        Console.WriteLine("=== Delete doctor ===");
        var idInput = Exceptions.GetStringOrCancel("Enter doctor ID to delete");
        if (idInput == null || !int.TryParse(idInput, out int id)) { Console.WriteLine("ID invalid. It must be a number."); return; }
        var doctor = _repo.GetDoctor(id);
        if (doctor == null) { Console.WriteLine("Doctor not found."); return; }
        
        Console.WriteLine($"Doctor data: {doctor}");
        var confirm = Exceptions.GetStringOrCancel("Are you sure you want to delete this patient? (y/n):");
        if (confirm != null && confirm.Trim().ToLower() == "y")
        {
            if (_repo.DeleteDoctor(id))
                Console.WriteLine("Doctor deleted successfully.");
            else
                Console.WriteLine("Error deleting doctor.");
        }
        else
        {
            Console.WriteLine("Deletion canceled.");
        }
    }
}
