namespace AIGenManager.Core.Domain.Entities;

public class Folder
{
    // Required for SQLite-net
    public Folder() {}
    
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Path { get; set; }
    public int ImageCount { get; set; }
    public DateTime ScannedDate { get; set; }
    public bool Unavailable { get; set; }
    public bool Archived { get; set; }
    public bool Excluded { get; set; }
    public bool IsRoot { get; set; }
    public bool Recursive { get; set; }
    public bool Watched { get; set; }

    public Folder(string path)
    {
        Path = path;
        ParentId = 0;
        ImageCount = 0;
        ScannedDate = DateTime.Now;
    }
}
