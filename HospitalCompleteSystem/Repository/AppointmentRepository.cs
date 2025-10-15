using HospitalCompleteSystem.Data;
using HospitalCompleteSystem.Interfaces;
using HospitalCompleteSystem.Models;

namespace HospitalCompleteSystem.Repository;

public class AppointmentRepository : IAppointmentRepository
{
    // CRUD operations for Appointment entity
    public void AddAppointment(Appointment appointment)
    {
        appointment.Id = Database.GetNextAppointmentId();
        Database.Appointments.Add(appointment);
    }

    // Method to get an appointment by ID
    public Appointment? GetAppointment(int id)
    {
        return Database.Appointments.FirstOrDefault(a => a.Id == id);
    }
    
    // Method to get an appointment by Patient Identification

    public Appointment? GetAppointmentByIdentification(string identification)
    {
        return Database.Appointments.FirstOrDefault(a => a.Patient.Identification == identification);
    }

    // Method to get all appointments
    public List<Appointment> GetAppointments()
    {
        return Database.Appointments;
    }

    // Method to get appointments by patient ID
    public List<Appointment> GetAppointmentsByPatient(int patientId)
    {
        return Database.Appointments
            .Where(a => a.Patient.Id == patientId)
            .OrderBy(a => a.DateTime)
            .ToList();
    }
    

    // Method to get appointments by doctor ID
    public List<Appointment> GetAppointmentsByDoctor(int doctorId)
    {
        return Database.Appointments
            .Where(a => a.Doctor.Id == doctorId)
            .OrderBy(a => a.DateTime)
            .ToList();
    }

    // Method to get appointments by doctor Identification
    public List<Appointment> GetAppointmentsByDoctorIdentification(string doctorIdentification)
    {
        return Database.Appointments
            .Where(a => a.Doctor.Identification == doctorIdentification)
            .OrderBy(a => a.DateTime)
            .ToList();
    }

    // Method to update an existing appointment
    public void UpdateAppointment(Appointment appointment)
    {
      
    }

    public bool HasConflictingAppointment(int doctorId, int patientId, DateTime dateTime)
    {
        return Database.HasConflictingAppointment(doctorId, patientId, dateTime);
    }

    // Methods for EmailLog entity
    public void AddEmailLog(EmailLog emailLog)
    {
        emailLog.Id = Database.GetNextEmailLogId();
        Database.EmailLogs.Add(emailLog);
    }
    // Method to get email logs by appointment ID
    public List<EmailLog> GetEmailLogs(int appointmentId)
    {
        return Database.EmailLogs
            .Where(e => e.AppointmentId == appointmentId)
            .ToList();
    }
    
    
}
