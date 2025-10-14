using HospitalCompleteSystem.Utils;

namespace HospitalCompleteSystem.Models;

public class Patient : Person
{
    public int Id { get; internal set; }
    public int Age { get; private set; }

    public Patient(string name, int age, string identification, string phone, string email)
        : base(name, identification, phone, email)
    {
        ValidateAge(age);
        Age = age;
    }

    private void ValidateAge(int age)
    {
        if (age <= 0)
            throw new ArgumentException("Age must be a positive number.");
    }

    // Method to set the ID (used by repository)
    internal void SetId(int id)
    {
        Id = id;
    }
    
    // Update methods for each property (controlled method)
    
    public void UpdatePatientData(string newName, string newIdentification, int? newAge, string newPhone, string newEmail)
    {
        // Validate all input data before making any changes
        if (!string.IsNullOrWhiteSpace(newName))
            ValidationUtils.ValidateName(newName);
            
        if (!string.IsNullOrWhiteSpace(newIdentification))
            ValidationUtils.ValidateIdentification(newIdentification);
            
        if (newAge.HasValue)
            ValidateAge(newAge.Value);
            
        if (!string.IsNullOrWhiteSpace(newPhone))
            ValidationUtils.ValidatePhone(newPhone);
            
        if (!string.IsNullOrWhiteSpace(newEmail))
            ValidationUtils.ValidateEmail(newEmail);

        // If all validations pass, update the data
        if (!string.IsNullOrWhiteSpace(newName)) Name = newName;
        if (!string.IsNullOrWhiteSpace(newIdentification)) Identification = newIdentification;
        if (newAge.HasValue) Age = newAge.Value;
        if (!string.IsNullOrWhiteSpace(newPhone)) Phone = newPhone;
        if (!string.IsNullOrWhiteSpace(newEmail)) Email = newEmail;
    }

    public override string ToString()
    {
        return $"Patient ID: {Id}, Name: {Name}, Age: {Age}, Identification: {Identification}, Phone: {Phone}, Email: {Email}";
    }
}