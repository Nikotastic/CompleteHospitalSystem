namespace HospitalCompleteSystem.Interfaces;

public interface IPatientService
{
    // Methods for managing patients
    void AddPatient();
    void ListPatients();
    void SearchPatientById();
    void UpdatePatient();
    void DeletePatient();
}

