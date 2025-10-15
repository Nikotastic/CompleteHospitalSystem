using HospitalCompleteSystem.Models;
using HospitalCompleteSystem.Repository;
using HospitalCompleteSystem.Interfaces;
using HospitalCompleteSystem.Data;

namespace HospitalCompleteSystem.Services;

public class DoctorService : IDoctorService
{
    private readonly DoctorRepository _repo = new DoctorRepository();

    // Method to add a new doctor with input validation
    public void AddDoctor()
    {
        Console.Clear();
        Console.WriteLine("=== Add New Doctor ===");
        try
        {
            var name = Exceptions.GetStringOrCancel("Name");
            if (name == null) return;

            var specialty = Exceptions.GetStringOrCancel("Specialty");
            if (specialty == null) return;

            string identification;
            while (true)
            {
                identification = Exceptions.GetStringOrCancel("Identification");
                if (identification == null) return;

                if (!Database.IdentificationUnique(identification))
                    Console.WriteLine("Error: That identification already exists in the system. Try again.");
                else
                    break;
            }

            // Validate unique identification in the system
            if (!Database.IdentificationUnique(identification))
            {
                Console.WriteLine("Error: That identification already exists in the system.");
                return;
            }

            string phone;
            while (true)
            {
                phone = Exceptions.GetStringOrCancel("Phone");
                if (phone == null) return;
                try
                {
                    HospitalCompleteSystem.Utils.ValidationUtils.ValidatePhone(phone);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            string email;
            while (true)
            {
                email = Exceptions.GetStringOrCancel("Email");
                if (email == null) return;
                try
                {
                    HospitalCompleteSystem.Utils.ValidationUtils.ValidateEmail(email);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            try
            {
                var doctor = new Doctor(name, specialty, identification, phone, email);
                _repo.AddDoctor(doctor);
                Console.WriteLine($"Doctor registered successfully with ID: {doctor.Id}");
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
        try
        {
            var idInput = Exceptions.GetStringOrCancel("Enter doctor ID");
            if (idInput == null) return;
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var doctor = _repo.GetDoctor(id);
            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }

            Console.WriteLine($"Current data: {doctor}");
            Console.WriteLine("\nInsert new data (or 'cancel' to exit, press Enter to keep current value):");

            var newName = Exceptions.GetStringOrCancel($"Name ({doctor.Name})", allowEmpty: true);
            if (newName == null) return;

            var newSpecialty = Exceptions.GetStringOrCancel($"Specialty ({doctor.Specialty})", allowEmpty: true);
            if (newSpecialty == null) return;

            var newIdentification = Exceptions.GetStringOrCancel($"Identification ({doctor.Identification})", allowEmpty: true);
            if (newIdentification == null) return;
            if (!string.IsNullOrWhiteSpace(newIdentification) && newIdentification != doctor.Identification)
            {
                if (!Database.IdentificationUnique(newIdentification, excludeDoctorId: doctor.Id))
                {
                    Console.WriteLine("Error: That identification already exists in the system.");
                    return;
                }
            }

            var newPhone = Exceptions.GetStringOrCancel($"Phone ({doctor.Phone})", allowEmpty: true);
            if (newPhone == null) return;

            var newEmail = Exceptions.GetStringOrCancel($"Email ({doctor.Email})", allowEmpty: true);
            if (newEmail == null) return;

            try
            {
                doctor.UpdateDoctorData(
                    string.IsNullOrWhiteSpace(newName) ? doctor.Name : newName,
                    string.IsNullOrWhiteSpace(newIdentification) ? doctor.Identification : newIdentification,
                    string.IsNullOrWhiteSpace(newPhone) ? doctor.Phone : newPhone,
                    string.IsNullOrWhiteSpace(newEmail) ? doctor.Email : newEmail
                );

                if (!string.IsNullOrWhiteSpace(newSpecialty))
                {
                    doctor.UpdateSpecialty(newSpecialty);
                }

                _repo.UpdateDoctor(doctor);
                Console.WriteLine("Doctor updated successfully.");
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
