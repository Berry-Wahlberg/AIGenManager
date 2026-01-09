namespace AIGenManager.Core.Domain.Entities;

public class Album
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public DateTime LastUpdated { get; set; }

    public Album() { }

    public Album(string name)
    {
        Name = name;
        Order = 0;
        LastUpdated = DateTime.Now;
    }
}
