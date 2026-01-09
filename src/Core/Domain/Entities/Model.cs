namespace AIGenManager.Core.Domain.Entities;

public class Model
{
    public string Path { get; set; }
    public string Filename { get; set; }
    public string Hash { get; set; }
    public string? SHA256 { get; set; }
    public bool IsLocal { get; set; }

    public Model(string path, string filename, string hash)
    {
        Path = path;
        Filename = filename;
        Hash = hash;
        IsLocal = true;
    }
}
