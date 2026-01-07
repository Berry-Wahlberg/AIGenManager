using SQLite;

namespace BerryAIGen.Database;

public class Snippet {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Text { get; set; }
}

