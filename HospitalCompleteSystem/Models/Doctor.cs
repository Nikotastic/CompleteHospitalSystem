namespace HospitalCompleteSystem.Models;

public class Doctor : Person
{
    // Properties
    public int Id { get; internal set; }
    public string Specialty { get; private set; }
    
    public Doctor(string name, string specialty, string identification, string phone, string email)
        : base(name, identification, phone, email)
    {
        Specialty = specialty;
    }

    // Update methods for each property
    public void UpdateSpecialty(string newSpecialty)
    {
        Specialty = newSpecialty;
    }
    // Method to set the ID 
    internal void SetId(int id)
    {
        Id = id;
    }
    
    public override string ToString()
    {
        return $"Doctor ID: {Id}, Name: {Name}, Specialty: {Specialty}, Identification: {Identification}, Phone: {Phone}, Email: {Email}";
    }

    
}