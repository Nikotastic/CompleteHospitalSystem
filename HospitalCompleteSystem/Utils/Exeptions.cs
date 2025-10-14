namespace HospitalCompleteSystem;

public class Exceptions
{
    // Method to get a string or cancel
    public static string GetStringOrCancel(string message, bool allowEmpty = false)
    {
        while (true)
        {
            Console.Write($"{message} (or 'cancel' to exit): ");
            string input = Console.ReadLine()!.Trim();

            if (input.ToLower() == "cancel")
                return null;

            if (!allowEmpty && string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("This field cannot be empty.");
                continue;
            }

            return input; 
        }
    }
    // It uses a generic delegate (Func<int, T?>) that allows you to pass the repository search function (_petRepository.GetById or _veterinarianRepository.GetById) to the method as an argument.
    public static int? GetValidId<T>(string entityName, Func<int, T?> repositoryLookup) where T : class
    {
        int id;
        string? input;

        while (true)
        {
            Console.Write($"Insert ID {entityName} (or 'cancel' to exit): ");
            input = Console.ReadLine()!.Trim();

            if (input.ToLower() == "cancel")
                return null; // Returns null if the user cancels

            if (!int.TryParse(input, out id))
            {
                Console.WriteLine("Invalid ID. Please enter a numeric value.");
                continue;
            }

            // Use the provided search function (the Func<int, T?> delegate)
            var entity = repositoryLookup(id); 
            
            if (entity != null)
                return id; // Returns the ID if the entity exists
            
            Console.WriteLine($"No {entityName.ToLower()} found with ID {id}. Try again.");
        }
    }
}