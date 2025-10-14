namespace HospitalCompleteSystem.Models;

public class Patient : Person
{
    public int Id { get; internal set; } // Setter interno para mayor seguridad
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
    public void UpdateName(string newName)
    {
        Name = newName;
    }
    public void UpdateIdentification(string newIdentification)
    {
        Identification = newIdentification;
    }
    public void UpdateAge(int newAge)
    {
        Age = newAge;
    }
    public void UpdatePhone(string newPhone)
    {
        Phone = newPhone;
    }
    public void UpdateEmail(string newEmail)
    {
        Email = newEmail;
    }

    public override string ToString()
    {
        return $"Patient ID: {Id}, Name: {Name}, Age: {Age}, Identification: {Identification}, Phone: {Phone}, Email: {Email}";
    }
}