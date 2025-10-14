namespace HospitalCompleteSystem.Utils;

public static class ValidationUtils
{
    public static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
            throw new ArgumentException("Name must have at least 2 characters.");
    }

    public static void ValidateIdentification(string identification)
    {
        if (string.IsNullOrWhiteSpace(identification) || identification.Length < 4)
            throw new ArgumentException("Identification must have at least 4 characters.");
    }

    public static void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) || !phone.All(char.IsDigit) || phone.Length < 7)
            throw new ArgumentException("Phone must contain only digits and at least 7 numbers.");
    }

    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || 
            !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid email format.");
    }

    public static void ValidateSpecialty(string specialty)
    {
        if (string.IsNullOrWhiteSpace(specialty) || specialty.Length < 2)
            throw new ArgumentException("Specialty must have at least 2 characters.");
    }
}
