using HospitalCompleteSystem.Models;

namespace HospitalCompleteSystem.Data;

public static class Database
{
    public static List<Patient> Patients = new List<Patient>();
    public static List<Doctor> Doctors = new List<Doctor>();
    public static List<Appointment> Appointments = new List<Appointment>();
    public static List<EmailLog> EmailLogs = new List<EmailLog>();
    
    // Simulated auto-incrementing IDs
    private static int _nextPatientId = 1;
    private static int _nextDoctorId = 1;
    private static int _nextAppointmentId = 1;
    private static int _nextEmailLogId = 1;

    public static int GetNextPatientId() => _nextPatientId++;
    public static int GetNextDoctorId() => _nextDoctorId++;
    public static int GetNextAppointmentId() => _nextAppointmentId++;
    public static int GetNextEmailLogId() => _nextEmailLogId++;

    // Check if identification is unique across patients and doctors
    public static bool IdentificationUnique(string identification, int? excludePatientId = null, int? excludeDoctorId = null)
    {
        bool existsInPatients = Patients.Any(p => p.Identification == identification && (!excludePatientId.HasValue || p.Id != excludePatientId.Value));
        bool existsInDoctors = Doctors.Any(d => d.Identification == identification && (!excludeDoctorId.HasValue || d.Id != excludeDoctorId.Value));
        return !(existsInPatients || existsInDoctors);
    }

    // Check if doctor or patient has appointment at given time
    public static bool HasConflictingAppointment(int doctorId, int patientId, DateTime dateTime)
    {
        // Check 1-hour slot
        var start = dateTime.AddMinutes(-59);
        var end = dateTime.AddMinutes(59);

        return Appointments.Any(a => 
            a.Status == AppointmentStatus.Scheduled &&
            a.DateTime >= start && 
            a.DateTime <= end &&
            (a.Doctor.Id == doctorId || a.Patient.Id == patientId)
        );
    }
    
}