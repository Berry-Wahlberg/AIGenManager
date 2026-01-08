using SQLite;

namespace BerryAIGen.Database.Models;

public class NodeProperty
{
    public int NodeId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}

