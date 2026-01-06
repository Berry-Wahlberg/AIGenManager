using BerryAIGen.IO;
using BerryAIGen.Common;

namespace BerryAIGen.Toolkit.Services;

public class RecordJob
{
    public bool Skip { get; set; }
    public FileParameters FileParameters { get; set; }
    public bool StoreMetadata { get; set; }
    public bool StoreWorkflow { get; set; }
}
 






