namespace HospitalCompleteSystem.Menus;

public class MenuMain
{
    // Displays the main menu and handles user input
    public static void ShowMenu()
    {
        int option;
        do
        {
            Console.WriteLine("\n--- MAIN MENU ---");
            Console.WriteLine("1. Manage Patients");
            Console.WriteLine("2. Manage Doctors");
            Console.WriteLine("3. Manage Appointments");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");
            
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (option)
            {
                case 1:
                    MenuPatient.ShowMenu();
                    break;
                case 2:
                    MenuDoctor.ShowMenu();
                    break;
                case 3:
                    MenuAppointment.ShowMenu();
                    break;
                case 0:
                    Console.WriteLine("Exiting the application. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        } while (option != 0);
    }
}