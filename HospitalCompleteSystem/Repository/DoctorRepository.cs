using HospitalCompleteSystem.Data;
using HospitalCompleteSystem.Models;
using HospitalCompleteSystem.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HospitalCompleteSystem.Repository;

public class DoctorRepository : IDoctorRepository
{
    // Method to add a new doctor to the database
    public void AddDoctor(Doctor doctor)
    {
        if (ExistsByIdentification(doctor.Identification))
            throw new InvalidOperationException("There is already a doctor with that document.");
        doctor.Id = Database.GetNextDoctorId();
        Database.Doctors.Add(doctor);
    }

    // Method to get a doctor by ID
    public Doctor? GetDoctor(int id)
    {
        return Database.Doctors.FirstOrDefault(d => d.Id == id);
    }

    // Method to get all doctors
    public List<Doctor> GetDoctors()
    {
        return Database.Doctors;
    }

    // Method to get a patient by Identification
    public Doctor? GetDoctorByIdentification(string identification)
    {
        return Database.Doctors.FirstOrDefault(p => p.Identification == identification);
    }
    
    // Method to get doctors by specialty
    public List<Doctor> GetDoctorsBySpecialty(string specialty)
    {
        return Database.Doctors.Where(d => d.Specialty.Equals(specialty, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    // Method to update an existing doctor
    public void UpdateDoctor(Doctor doctor)
    {
        var existing = GetDoctor(doctor.Id);
        if (existing != null)
        {
            existing.UpdateDoctorData(doctor.Name, doctor.Identification, doctor.Phone, doctor.Email);
            existing.UpdateSpecialty(doctor.Specialty);
        }
    }

    // Method to delete a doctor by ID
    public bool DeleteDoctor(int id)
    {
        var doctor = GetDoctor(id);
        if (doctor != null)
        {
            Database.Doctors.Remove(doctor);
            return true;
        }
        return false;
    }

    // Method to check if a doctor exists by identification
    public bool ExistsByIdentification(string identification)
    {
        return Database.Doctors.Any(d => d.Identification == identification);
    }
}
