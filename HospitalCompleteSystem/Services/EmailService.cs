using HospitalCompleteSystem.Models;

namespace HospitalCompleteSystem.Services;

public class EmailService
{
    public static bool SendAppointmentConfirmation(Appointment appointment, out string errorMessage)
    {
        errorMessage = string.Empty;
        try
        {
            Console.WriteLine("Simulating email sending...");
            Console.WriteLine($"To: {appointment.Patient.Email}");
            Console.WriteLine($"Subject: Appointment Confirmation");
            Console.WriteLine($"Body: Your appointment with Dr. {appointment.Doctor.Name} is confirmed for {appointment.DateTime:dd/MM/yyyy HH:mm}.");
            return true;
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            return false;
        }
    }
}
