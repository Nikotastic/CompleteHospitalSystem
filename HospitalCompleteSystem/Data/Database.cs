using HospitalCompleteSystem.Models;

namespace HospitalCompleteSystem.Data;

public static class Database
{
        public static List<Patient> Patients = new List<Patient>();
        //public static List<Models.Doctor> Doctors { get; } = new List<Models.Doctor>();
        //public static List<Models.Appointment> Appointments { get; } = new List<Appointment>();
        
        // Simulated auto-incrementing IDs
        private static int _nextPatientId = 1;
        public static int GetNextPatientId() => _nextPatientId++;

        private static int _nextDoctorId = 1;
        public static int GetNextDoctorId() => _nextDoctorId++;

        private static int _nextAppointmentId = 1;
        public static int GetNextAppointmentId() => _nextAppointmentId++;

}