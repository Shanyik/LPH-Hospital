namespace lphh_api.Model;

public class Prescription
{
    public uint Id { get; init; }
    public uint PatientId { get; init; }
    public uint DoctorId { get; init; }
    public uint ProductId { get; init; }
    public string Description { get; init; }
    public DateTime CreatedAt  { get; init; }
}