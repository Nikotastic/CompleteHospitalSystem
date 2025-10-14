using HospitalCompleteSystem.Models;
using System.Collections.Generic;

namespace HospitalCompleteSystem.Interfaces;

public interface IPatientRepository
{
    void AddPatient(Patient patient);
    Patient? GetPatient(int id);
    List<Patient> GetPatients();
    void UpdatePatient(Patient patient);
    bool DeletePatient(int id);
}

