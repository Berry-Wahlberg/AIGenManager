using System;
using System.IO;
using AIGenManager.Core.Domain.Entities;
using SQLite;


namespace AIGenManager.Infrastructure.Data;

public class DatabaseContext : IDisposable
{
    private readonly SQLiteConnection _connection;
    public string DatabasePath { get; }

    public DatabaseContext(string databasePath)
    {
        DatabasePath = databasePath;
        _connection = new SQLiteConnection(databasePath);
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        var databaseDir = Path.GetDirectoryName(DatabasePath);
        if (!Directory.Exists(databaseDir))
        {
            Directory.CreateDirectory(databaseDir);
        }

        CreateTables();
        CreateIndexes();
    }

    private void CreateTables()
    {
        _connection.CreateTable<Image>();
        _connection.CreateTable<Folder>();
        _connection.CreateTable<Tag>();
        _connection.CreateTable<ImageTag>();
        _connection.CreateTable<Album>();
        
        // Create AlbumImage table with foreign keys
        var sql = @"
        CREATE TABLE IF NOT EXISTS ""AlbumImage""(
            ""AlbumId""   integer,
            ""ImageId""   integer,
            CONSTRAINT ""FK_AlbumImage_AlbumId"" FOREIGN KEY(""AlbumId"") REFERENCES Album(""Id""),
            CONSTRAINT ""FK_AlbumImage_ImageId"" FOREIGN KEY(""ImageId"") REFERENCES Image(""Id"")
        );
        ";
        _connection.Execute(sql);
    }

    private void CreateIndexes()
    {
        // Image indexes
        _connection.CreateIndex<Image>(image => image.RootFolderId);
        _connection.CreateIndex<Image>(image => image.FolderId);
        _connection.CreateIndex<Image>(image => image.Path, true);
        _connection.CreateIndex<Image>(image => image.FileName);
        _connection.CreateIndex<Image>(image => image.ModelHash);
        _connection.CreateIndex<Image>(image => image.Model);
        _connection.CreateIndex<Image>(image => image.Seed);
        _connection.CreateIndex<Image>(image => image.Sampler);
        _connection.CreateIndex<Image>(image => image.Height);
        _connection.CreateIndex<Image>(image => image.Width);
        _connection.CreateIndex<Image>(image => image.CFGScale);
        _connection.CreateIndex<Image>(image => image.Steps);
        _connection.CreateIndex<Image>(image => image.AestheticScore);
        _connection.CreateIndex<Image>(image => image.Favorite);
        _connection.CreateIndex<Image>(image => image.Rating);
        _connection.CreateIndex<Image>(image => image.ForDeletion);
        _connection.CreateIndex<Image>(image => image.NSFW);
        _connection.CreateIndex<Image>(image => image.Unavailable);
        _connection.CreateIndex<Image>(image => image.CreatedDate);
        _connection.CreateIndex<Image>(image => image.Type);

        // Folder indexes
        _connection.CreateIndex<Folder>(folder => folder.ParentId);
        _connection.CreateIndex<Folder>(folder => folder.Path, true);
        _connection.CreateIndex<Folder>(folder => folder.Archived);
        _connection.CreateIndex<Folder>(folder => folder.Unavailable);
        _connection.CreateIndex<Folder>(folder => folder.Excluded);

        // Tag indexes
        _connection.CreateIndex<Tag>(tag => tag.Id);
        
        // ImageTag indexes - using raw SQL for composite index
        _connection.Execute("CREATE INDEX IF NOT EXISTS 'IX_ImageTag_ImageId_TagId' ON 'ImageTag' ('ImageId', 'TagId')");
    }

    public SQLiteConnection GetConnection()
    {
        return _connection;
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}
