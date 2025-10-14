using HospitalCompleteSystem.Models;
using HospitalCompleteSystem.Services;

namespace HospitalCompleteSystem.Menus;

public class MenuPatient
{
    // Display the patient menu and handle user input
    public static void ShowMenu()
    {
        // Menu loop
        bool exit = false;
        var patientService = new PatientService(); // Instance available for the entire menu

        while (!exit)
        {
            Console.WriteLine("\n=== MENU PATIENT ===");
            Console.WriteLine("1. Register patient");
            Console.WriteLine("2. List patient");
            Console.WriteLine("3. Search patient by ID");
            Console.WriteLine("4. Update patient");
            Console.WriteLine("5. Delete patient");
            Console.WriteLine("0. Back to main menu");
            Console.Write("\nSelect an option: ");


            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    // I make a catch for each option so that if one fails the entire menu doesn't crash
                    try { patientService.AddPatient(); } catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                    break;
                case "2":
                    try { patientService.ListPatients(); } catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                    break;
                case "3":
                    try { patientService.SearchPatientById(); } catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                    break;
                case "4":
                    try { patientService.UpdatePatient(); } catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                    break;
                case "5":
                    try { patientService.DeletePatient(); } catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("\nOption invalid. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }


        }
    }
}