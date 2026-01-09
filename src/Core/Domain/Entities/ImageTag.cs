namespace AIGenManager.Core.Domain.Entities;

public class ImageTag
{
    public int ImageId { get; set; }
    public int TagId { get; set; }

    public ImageTag() { }

    public ImageTag(int imageId, int tagId)
    {
        ImageId = imageId;
        TagId = tagId;
    }
}
