namespace HospitalCompleteSystem.Models;

// Abstract class representing a person
public abstract class Person
{
    // Properties
    public string Name { get; protected set; }
    public string Identification { get; protected set; }
    public string Phone { get; protected set; }
    public string Email { get; protected set; }
    
    public Person(string name, string identification, string phone, string email)
    {
        Name = name;
        Identification = identification;
        Phone = phone;
        Email = email;
    }
}