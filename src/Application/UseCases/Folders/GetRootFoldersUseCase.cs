using AIGenManager.Core.Application.Ports;
using AIGenManager.Core.Domain.Entities;

namespace AIGenManager.Application.UseCases.Folders;

public class GetRootFoldersUseCase : UseCase<IEnumerable<Folder>>
{
    private readonly IFolderRepository _folderRepository;

    public GetRootFoldersUseCase(IFolderRepository folderRepository)
    {
        _folderRepository = folderRepository;
    }

    public override async Task<IEnumerable<Folder>> ExecuteAsync()
    {
        return await _folderRepository.GetRootFoldersAsync();
    }
}
