namespace lph_api.Model;

public class Patient
{
    public uint Id { get; }
    public string Username { get; }
    public string Password { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public DateTime CreatedAt  { get; }

    public Patient(uint id, string username, string password, string email, string phoneNumber, string firstName, string lastName)
    {
        Id = id;
        Username = username;
        Password = password;
        Email = email;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        CreatedAt = DateTime.Now;
    }
}