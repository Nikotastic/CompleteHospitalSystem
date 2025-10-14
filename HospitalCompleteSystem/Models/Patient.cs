namespace HospitalCompleteSystem.Models;

public class Patient : Person
{
    public int Id { get; internal set; }
    public int Age { get; set; }

    public Patient(string name, int age, string identification, string phone, string email)
        : base(name, identification, phone, email)
    {
        Age = age;
    }
    // Method to set the ID (used by repository)
    internal void SetId(int id)
    {
        Id = id;
    }
    
    // Update methods for each property
    
    public void UpdatePatientData(string newName, string newIdentification, int? newAge, string newPhone, string newEmail)
    {
        if (!string.IsNullOrWhiteSpace(newName)) Name = newName;
        if (!string.IsNullOrWhiteSpace(newIdentification)) Identification = newIdentification;
        if (newAge.HasValue && newAge.Value > 0) Age = newAge.Value;
        if (!string.IsNullOrWhiteSpace(newPhone)) Phone = newPhone;
        if (!string.IsNullOrWhiteSpace(newEmail)) Email = newEmail;
    }

    public override string ToString()
    {
        return $"Patient ID: {Id}, Name: {Name}, Age: {Age}, Identification: {Identification}, Phone: {Phone}, Email: {Email}";
    }
}