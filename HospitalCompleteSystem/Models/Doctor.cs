using HospitalCompleteSystem.Utils;

namespace HospitalCompleteSystem.Models;

public class Doctor : Person
{
    public int Id { get; internal set; }
    public string Specialty { get; private set; }
    
    public Doctor(string name, string specialty, string identification, string phone, string email)
        : base(name, identification, phone, email)
    {
        ValidationUtils.ValidateSpecialty(specialty);
        Specialty = specialty;
    }

    public void UpdateDoctorData(string newName, string newIdentification, string newPhone, string newEmail)
    {
        if (!string.IsNullOrWhiteSpace(newName))
            ValidationUtils.ValidateName(newName);
            
        if (!string.IsNullOrWhiteSpace(newIdentification))
            ValidationUtils.ValidateIdentification(newIdentification);
            
        if (!string.IsNullOrWhiteSpace(newPhone))
            ValidationUtils.ValidatePhone(newPhone);
            
        if (!string.IsNullOrWhiteSpace(newEmail))
            ValidationUtils.ValidateEmail(newEmail);

        if (!string.IsNullOrWhiteSpace(newName)) Name = newName;
        if (!string.IsNullOrWhiteSpace(newIdentification)) Identification = newIdentification;
        if (!string.IsNullOrWhiteSpace(newPhone)) Phone = newPhone;
        if (!string.IsNullOrWhiteSpace(newEmail)) Email = newEmail;
    }

    public void UpdateSpecialty(string newSpecialty)
    {
        ValidationUtils.ValidateSpecialty(newSpecialty);
        Specialty = newSpecialty;
    }

    public override string ToString()
    {
        return $"Doctor ID: {Id}, Name: {Name}, Specialty: {Specialty}, Identification: {Identification}, Phone: {Phone}, Email: {Email}";
    }
}