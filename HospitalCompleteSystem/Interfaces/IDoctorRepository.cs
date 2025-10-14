using HospitalCompleteSystem.Models;
using System.Collections.Generic;

namespace HospitalCompleteSystem.Interfaces;

public interface IDoctorRepository
{
    // CRUD operations for Doctor entity
    void AddDoctor(Doctor doctor);
    Doctor? GetDoctor(int id);
    List<Doctor> GetDoctors();
    List<Doctor> GetDoctorsBySpecialty(string specialty);
    void UpdateDoctor(Doctor doctor);
    bool DeleteDoctor(int id);
    bool ExistsByIdentification(string identification);
}

