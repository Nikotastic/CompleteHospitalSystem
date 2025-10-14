using HospitalCompleteSystem.Models;

namespace HospitalCompleteSystem.Data;

public static class Database
{
        public static List<Patient> Patients = new List<Patient>();
        public static List<Doctor> Doctors = new List<Doctor>();
        //public static List<Models.Appointment> Appointments { get; } = new List<Appointment>();
        
        // Simulated auto-incrementing IDs
        private static int _nextPatientId = 1;
        public static int GetNextPatientId() => _nextPatientId++;

        private static int _nextDoctorId = 1;
        public static int GetNextDoctorId() => _nextDoctorId++;

        private static int _nextAppointmentId = 1;
        public static int GetNextAppointmentId() => _nextAppointmentId++;

        public static bool IdentificationUnique(string identification, int? excludePatientId = null, int? excludeDoctorId = null)
        {
            bool existsInPatients = Patients.Any(p => p.Identification == identification && (!excludePatientId.HasValue || p.Id != excludePatientId.Value));
            bool existsInDoctors = Doctors.Any(d => d.Identification == identification && (!excludeDoctorId.HasValue || d.Id != excludeDoctorId.Value));
            return !(existsInPatients || existsInDoctors);
        }

}