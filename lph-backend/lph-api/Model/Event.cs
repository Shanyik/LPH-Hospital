namespace lph_api.Model;

public class Event
{
    public uint Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime Start  { get; init; }
    public DateTime End  { get; init; }
    public DateTime CreatedAt  { get; init; }
}