using AIGenManager.Core.Domain.Entities;

namespace AIGenManager.Core.Application.Ports;

public interface IImageRepository
{
    Task<IEnumerable<Image>> GetAllAsync();
    Task<Image?> GetByIdAsync(int id);
    Task<Image> AddAsync(Image image);
    Task<Image> UpdateAsync(Image image);
    Task DeleteAsync(int id);
    Task<IEnumerable<Image>> GetByFolderIdAsync(int folderId);
}
