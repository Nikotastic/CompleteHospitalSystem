using HospitalCompleteSystem.Models;

namespace HospitalCompleteSystem.Interfaces;

public interface IAppointmentRepository
{
    // CRUD operations for Appointment entity
    void AddAppointment(Appointment appointment);
    Appointment? GetAppointment(int id);
    List<Appointment> GetAppointments();
    List<Appointment> GetAppointmentsByPatient(int patientId);
    List<Appointment> GetAppointmentsByDoctor(int doctorId);
    void UpdateAppointment(Appointment appointment);
    bool HasConflictingAppointment(int doctorId, int patientId, DateTime dateTime);
}
