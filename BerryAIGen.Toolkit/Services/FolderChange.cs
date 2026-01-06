namespace BerryAIGen.Toolkit.Services;
using BerryAIGen.Common;

public class FolderChange
{
    public FolderType FolderType { get; set; }
    public string Path { get; set; }
    public string NewPath { get; set; }
    public ChangeType ChangeType { get; set; }
    public bool Recursive { get; set; }
    public bool Watched { get; set; }
}







