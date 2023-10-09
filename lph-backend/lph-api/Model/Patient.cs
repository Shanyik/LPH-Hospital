namespace lph_api.Model;

public class Patient
{
    public uint Id { get; init;  }
    public string Username { get; init; }
    public string Password { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime CreatedAt  { get; init; }

}