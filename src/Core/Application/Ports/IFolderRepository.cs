using AIGenManager.Core.Domain.Entities;

namespace AIGenManager.Core.Application.Ports;

public interface IFolderRepository
{
    Task<IEnumerable<Folder>> GetAllAsync();
    Task<Folder?> GetByIdAsync(int id);
    Task<Folder> AddAsync(Folder folder);
    Task<Folder> UpdateAsync(Folder folder);
    Task DeleteAsync(int id);
    Task<IEnumerable<Folder>> GetRootFoldersAsync();
    Task<IEnumerable<Folder>> GetChildrenAsync(int parentId);
}
