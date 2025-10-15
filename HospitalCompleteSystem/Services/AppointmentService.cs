using HospitalCompleteSystem.Models;
using HospitalCompleteSystem.Repository;

namespace HospitalCompleteSystem.Services;

public class AppointmentService
{
    // Repositories
    private readonly AppointmentRepository _repo = new AppointmentRepository();
    private readonly PatientRepository _patientRepo = new PatientRepository();
    private readonly DoctorRepository _doctorRepo = new DoctorRepository();

    public void ScheduleAppointment()
    {
        Console.Clear();
        Console.WriteLine("=== Schedule New Appointment ===");
        try
        {
            // Get patient
            var patientIdentification = Exceptions.GetStringOrCancel("Patient Identification");
            if (string.IsNullOrWhiteSpace(patientIdentification))
            {
                Console.WriteLine("Invalid patient identification.");
                return;
            }

            var patient = _patientRepo.GetPatientByIdentification(patientIdentification);
            if (patient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }

            // Get doctor
            var doctorIdentification = Exceptions.GetStringOrCancel("Doctor Identification");
            if (string.IsNullOrWhiteSpace(doctorIdentification))
            {
                Console.WriteLine("Invalid doctor identification.");
                return;
            }

            var doctor = _doctorRepo.GetDoctorByIdentification(doctorIdentification);
            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }

            // Get date and time
            var dateInput = Exceptions.GetStringOrCancel("Date (dd/MM/yyyy)");
            if (string.IsNullOrWhiteSpace(dateInput))
            {
                Console.WriteLine("Invalid date.");
                return;
            }

            var timeInput = Exceptions.GetStringOrCancel("Time (HH:mm)");
            if (string.IsNullOrWhiteSpace(timeInput))
            {
                Console.WriteLine("Invalid time.");
                return;
            }

            if (!DateTime.TryParseExact(dateInput + " " + timeInput, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime appointmentDateTime))
            {
                Console.WriteLine("Invalid date and time format.");
                return;
            }

            // Assuming the appointment is successfully scheduled
            var appointment = new Appointment(patient, doctor, appointmentDateTime);

            _repo.AddAppointment(appointment);
            Console.WriteLine("Appointment successfully scheduled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    // List all appointments
    public void ListAppointmentsByPatient()
    {
        Console.Clear();
        Console.WriteLine("=== List Patient Appointments ===");
    
        var identification = Exceptions.GetStringOrCancel("Patient Identification");
        if (string.IsNullOrWhiteSpace(identification))
        {
            Console.WriteLine("Invalid patient identification.");
            return;
        }

        var patient = _patientRepo.GetPatientByIdentification(identification);
        if (patient == null)
        {
            Console.WriteLine("Patient not found.");
            return;
        }

        var appointments = _repo.GetAppointmentsByPatient(patient.Id);
        if (!appointments.Any())
        {
            Console.WriteLine("No appointments found for this patient.");
            return;
        }

        foreach (var appointment in appointments)
        {
            Console.WriteLine(appointment);
        }
    }


    // List all appointments
    public void ListAppointmentsByDoctor()
    {
        Console.Clear();
        Console.WriteLine("=== List Doctor Appointments ===");
    
        var identification = Exceptions.GetStringOrCancel("Doctor Identification");
        if (string.IsNullOrWhiteSpace(identification))
        {
            Console.WriteLine("Invalid doctor identification.");
            return;
        }

        var doctor = _doctorRepo.GetDoctorByIdentification(identification);
        if (doctor == null)
        {
            Console.WriteLine("Doctor not found.");
            return;
        }

        var appointments = _repo.GetAppointmentsByDoctor(doctor.Id);
        if (!appointments.Any())
        {
            Console.WriteLine("No appointments found for this doctor.");
            return;
        }

        foreach (var appointment in appointments)
        {
            Console.WriteLine(appointment);
        }
    }


    // Update appointment status
    public void UpdateAppointmentStatus()
    {
        Console.Clear();
        Console.WriteLine("=== Update Appointment Status ===");
        
        var idInput = Exceptions.GetStringOrCancel("Appointment ID");
        if (string.IsNullOrWhiteSpace(idInput))
        {
            Console.WriteLine("Invalid appointment ID.");
            return;
        }
        
        if (!int.TryParse(idInput, out int appointmentId))
        {
            Console.WriteLine("Invalid appointment ID.");
            return;
        }

        var appointment = _repo.GetAppointment(appointmentId);
        if (appointment == null)
        {
            Console.WriteLine("Appointment not found.");
            return;
        }

        Console.WriteLine($"Current appointment: {appointment}");
        Console.WriteLine("\n1. Mark as Completed");
        Console.WriteLine("2. Cancel appointment");
        Console.WriteLine("0. Back");

        var option = Exceptions.GetStringOrCancel("Select option");
        if (string.IsNullOrWhiteSpace(option))
        {
            Console.WriteLine("Invalid option.");
            return;
        }

        try
        {
            switch (option)
            {
                case "1":
                    appointment.MarkAsCompleted();
                    Console.WriteLine("Appointment marked as completed.");
                    break;
                case "2":
                    appointment.Cancel();
                    Console.WriteLine("Appointment cancelled.");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    

    // Show available hours for a doctor on a specific date
    public void ShowAvailableHours(string doctorIdentification, string dateInput)
    {
        if (string.IsNullOrWhiteSpace(doctorIdentification))
        {
            Console.WriteLine("Doctor identification cannot be empty.");
            return;
        }

        if (!DateTime.TryParseExact(dateInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            Console.WriteLine("Invalid date format. Please use dd/MM/yyyy.");
            return;
        }

        var doctor = _doctorRepo.GetDoctorByIdentification(doctorIdentification);
        if (doctor == null)
        {
            Console.WriteLine("Doctor not found.");
            return;
        }

        var appointments = _repo.GetAppointmentsByDoctorIdentification(doctorIdentification)
            .Where(a => a.DateTime.Date == date.Date)
            .Select(a => a.DateTime.TimeOfDay)
            .ToHashSet();

        var startHour = new TimeSpan(9, 0, 0); // Clinic opens at 9:00 AM
        var endHour = new TimeSpan(17, 0, 0);  // Clinic closes at 5:00 PM

        Console.WriteLine($"\nAvailable hours for {doctor.Name} on {date:dd/MM/yyyy}:");
        bool hasAvailableHours = false;

        for (var time = startHour; time < endHour; time = time.Add(new TimeSpan(0, 30, 0)))
        {
            if (!appointments.Contains(time))
            {
                Console.WriteLine(time.ToString(@"hh\:mm"));
                hasAvailableHours = true;
            }
        }

        if (!hasAvailableHours)
        {
            Console.WriteLine("No available hours for this date.");
        }
    }
    
}
