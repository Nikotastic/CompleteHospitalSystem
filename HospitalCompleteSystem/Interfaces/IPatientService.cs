namespace HospitalCompleteSystem.Interfaces;

public interface IPatientService
{
    void AddPatient();
    void ListPatients();
    void SearchPatientById();
    void UpdatePatient();
    void DeletePatient();
}

