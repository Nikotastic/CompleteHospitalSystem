using HospitalCompleteSystem.Services;

namespace HospitalCompleteSystem.Menus;

public class MenuAppointment
{
    public static void ShowMenu()
    {
        // Instantiate the AppointmentService
        var appointmentService = new AppointmentService();
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("\n=== APPOINTMENTS MENU ===");
            Console.WriteLine("1. Schedule Appointment");
            Console.WriteLine("2. List Patient Appointments");
            Console.WriteLine("3. List Doctor Appointments");
            Console.WriteLine("4. Update Appointment Status");
            Console.WriteLine("5. Show Available Hours");
            Console.WriteLine("6. Simulate Email Sending");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nSelect an option: ");

            string? option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        appointmentService.ScheduleAppointment();
                        break;
                    case "2":
                        appointmentService.ListAppointmentsByPatient();
                        break;
                    case "3":
                        appointmentService.ListAppointmentsByDoctor();
                        break;
                    case "4":
                        appointmentService.UpdateAppointmentStatus();
                        break;
                    case "5":
                        Console.Write("Enter Doctor Identification: ");
                        var doctorIdentification = Console.ReadLine();
                        Console.Write("Enter Date (dd/MM/yyyy): ");
                        var dateInput = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(doctorIdentification) && !string.IsNullOrWhiteSpace(dateInput))
                        {
                            appointmentService.ShowAvailableHours(doctorIdentification, dateInput);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please try again.");
                        }
                        break;
                    case "6":
                        Console.WriteLine("Simulating email sending...");
                        Console.WriteLine("This is a placeholder for email simulation.");
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                if (option != "0")
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
