namespace lphh_api.Model;

public class Exam
{
    public uint Id { get; init; }
    public string Type { get; init; }
    public uint PatientId { get; init; }
    public uint DoctorId { get; init; }
    public string Result { get; init; }
    public DateTime CreatedAt  { get; init; }
}