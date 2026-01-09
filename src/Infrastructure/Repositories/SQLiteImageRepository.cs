using AIGenManager.Core.Application.Ports;
using AIGenManager.Core.Domain.Entities;
using SQLite;

namespace AIGenManager.Infrastructure.Repositories;

public class SQLiteImageRepository : IImageRepository
{
    private readonly SQLiteConnection _connection;

    public SQLiteImageRepository(SQLiteConnection connection)
    {
        _connection = connection;
    }

    public Task<IEnumerable<Image>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Image>>(_connection.Table<Image>().ToList());
    }

    public Task<Image?> GetByIdAsync(int id)
    {
        return Task.FromResult(_connection.Table<Image>().Where(i => i.Id == id).FirstOrDefault());
    }

    public Task<Image> AddAsync(Image image)
    {
        _connection.Insert(image);
        return Task.FromResult(image);
    }

    public Task<Image> UpdateAsync(Image image)
    {
        _connection.Update(image);
        return Task.FromResult(image);
    }

    public Task DeleteAsync(int id)
    {
        _connection.Delete<Image>(id);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Image>> GetByFolderIdAsync(int folderId)
    {
        return Task.FromResult<IEnumerable<Image>>(_connection.Table<Image>().Where(i => i.FolderId == folderId).ToList());
    }
}
