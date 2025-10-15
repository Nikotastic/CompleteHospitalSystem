using HospitalCompleteSystem.Data;
using HospitalCompleteSystem.Interfaces;
using HospitalCompleteSystem.Models;

namespace HospitalCompleteSystem.Repository;

public class PatientRepository : IPatientRepository
{
    // Method to add a new patient to the database
    public void AddPatient(Patient patient)
    {
        patient.Id = Database.GetNextPatientId(); 
        Database.Patients.Add(patient);
    }

    // Method to get a patient by ID
    public Patient? GetPatient(int id)
    {
        return Database.Patients.FirstOrDefault(p => p.Id == id);
    }
    
    // Method to get a patient by Identification
    public Patient? GetPatientByIdentification(string identification)
    {
        return Database.Patients.FirstOrDefault(p => p.Identification == identification);
    }

    // Method to get all patients
    public List<Patient> GetPatients()
    {
        return Database.Patients;
    }

    // Method to update an existing patient
    public void UpdatePatient(Patient patient)
    {
        var existing = GetPatient(patient.Id);
        if (existing != null)
        {
            existing.UpdatePatientData(
                patient.Name,
                patient.Identification,
                patient.Age,
                patient.Phone,
                patient.Email
            );
        }
    }
    
    // Method to delete a patient by ID
    public bool DeletePatient(int id)
    {
        var patient = GetPatient(id);
        if (patient != null)
        {
            Database.Patients.Remove(patient);
            return true;
        }
        return false;
    }

}