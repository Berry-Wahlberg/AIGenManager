using AIGenManager.Core.Application.Ports;
using AIGenManager.Core.Domain.Entities;
using SQLite;

namespace AIGenManager.Infrastructure.Repositories;

public class SQLiteFolderRepository : IFolderRepository
{
    private readonly SQLiteConnection _connection;

    public SQLiteFolderRepository(SQLiteConnection connection)
    {
        _connection = connection;
    }

    public Task<IEnumerable<Folder>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Folder>>(_connection.Table<Folder>().ToList());
    }

    public Task<Folder?> GetByIdAsync(int id)
    {
        return Task.FromResult(_connection.Table<Folder>().Where(f => f.Id == id).FirstOrDefault());
    }

    public Task<Folder> AddAsync(Folder folder)
    {
        _connection.Insert(folder);
        return Task.FromResult(folder);
    }

    public Task<Folder> UpdateAsync(Folder folder)
    {
        _connection.Update(folder);
        return Task.FromResult(folder);
    }

    public Task DeleteAsync(int id)
    {
        _connection.Delete<Folder>(id);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Folder>> GetRootFoldersAsync()
    {
        return Task.FromResult<IEnumerable<Folder>>(_connection.Table<Folder>().Where(f => f.IsRoot).ToList());
    }

    public Task<IEnumerable<Folder>> GetChildrenAsync(int parentId)
    {
        return Task.FromResult<IEnumerable<Folder>>(_connection.Table<Folder>().Where(f => f.ParentId == parentId).ToList());
    }
}
