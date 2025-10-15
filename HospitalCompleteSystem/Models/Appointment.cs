namespace HospitalCompleteSystem.Models;

public class Appointment
{
    // Properties
    public int Id { get; internal set; }
    public Patient Patient { get; private set; }
    public Doctor Doctor { get; private set; }
    public DateTime DateTime { get; private set; }
    public AppointmentStatus Status { get; private set; }

    public Appointment(Patient patient, Doctor doctor, DateTime dateTime)
    {
        ValidateDateTime(dateTime);
        Patient = patient ?? throw new ArgumentNullException(nameof(patient));
        Doctor = doctor ?? throw new ArgumentNullException(nameof(doctor));
        DateTime = dateTime;
        Status = AppointmentStatus.Scheduled;
    }
    // Validate appointment date and time
    private void ValidateDateTime(DateTime dateTime)
    {
        if (dateTime < DateTime.Now)
            throw new ArgumentException("Appointment date cannot be in the past.");

        // Validate business hours (8 AM to 6 PM)
        if (dateTime.Hour < 7 || dateTime.Hour >= 18)
            throw new ArgumentException("Appointments must be scheduled between 7 AM and 6 PM.");

        // Validate weekdays only
        if (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday)
            throw new ArgumentException("Appointments can only be scheduled on weekdays.");
    }

    // Methods to update status
    public void Cancel()
    {
        if (Status != AppointmentStatus.Scheduled)
            throw new InvalidOperationException("Only scheduled appointments can be cancelled.");
        Status = AppointmentStatus.Cancelled;
    }

    // Mark appointment as completed
    public void MarkAsCompleted()
    {
        if (Status != AppointmentStatus.Scheduled)
            throw new InvalidOperationException("Only scheduled appointments can be marked as completed.");
        if (DateTime > DateTime.Now)
            throw new InvalidOperationException("Future appointments cannot be marked as completed.");
        Status = AppointmentStatus.Completed;
    }

    public override string ToString()
    {
        return $"Appointment: Patient={Patient.Name}, Doctor={Doctor.Name}, DateTime={DateTime:g}, Status={Status}";
    }
}
