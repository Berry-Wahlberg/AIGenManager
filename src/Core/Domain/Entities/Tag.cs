namespace AIGenManager.Core.Domain.Entities;

public class Tag
{
    // Required for SQLite-net
    public Tag() {}
    
    public int Id { get; set; }
    public string Name { get; set; }

    public Tag(string name)
    {
        Name = name;
    }
}
