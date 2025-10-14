using HospitalCompleteSystem.Services;

namespace HospitalCompleteSystem.Menus;

public class MenuDoctor
{
    // Show the doctor menu
    public static void ShowMenu()
    {
        // Menu loop
        bool exit = false;
        var doctorService = new DoctorService();
        while (!exit)
        {
            Console.WriteLine("\n=== MENU DOCTORS ===");
            Console.WriteLine("1. Add doctor");
            Console.WriteLine("2. List all doctors");
            Console.WriteLine("3. List doctor by specialty");
            Console.WriteLine("4. Search doctor por ID");
            Console.WriteLine("5. Update doctor");
            Console.WriteLine("6. Delete doctor");
            Console.WriteLine("0. Back to main menu");
            Console.Write("\nSelect an option: ");
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    // I make a catch for each option so that if one fails the entire menu doesn't crash
                    try { doctorService.AddDoctor(); } catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                    break;
                case "2":
                    try { doctorService.ListDoctors(); } catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                    break;
                case "3":
                    try { doctorService.ListDoctorsBySpecialty(); } catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                    break;
                case "4":
                    try { doctorService.SearchDoctorById(); } catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                    break;
                case "5":
                    try { doctorService.UpdateDoctor(); } catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                    break;
                case "6":
                    try { doctorService.DeleteDoctor(); } catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press a key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}

