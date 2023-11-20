namespace lphh_api.Model;

public class Admin
{
    public uint Id { get; init; }
    public string IdentityId { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime CreatedAt  { get; init; }
}