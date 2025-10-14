namespace HospitalCompleteSystem.Interfaces;

public interface IDoctorService
{
    // Methods for managing doctors
    void AddDoctor();
    void ListDoctors();
    void ListDoctorsBySpecialty();
    void SearchDoctorById();
    void UpdateDoctor();
    void DeleteDoctor();
}

