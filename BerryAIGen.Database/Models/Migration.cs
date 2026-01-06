using SQLite;

namespace BerryAIGen.Database.Models;

public class Migration
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
}
