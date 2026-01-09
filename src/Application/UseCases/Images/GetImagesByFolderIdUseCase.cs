using AIGenManager.Core.Application.Ports;
using AIGenManager.Core.Domain.Entities;

namespace AIGenManager.Application.UseCases.Images;

public record GetImagesByFolderIdRequest(int FolderId);

public class GetImagesByFolderIdUseCase : UseCase<GetImagesByFolderIdRequest, IEnumerable<Image>>
{
    private readonly IImageRepository _imageRepository;

    public GetImagesByFolderIdUseCase(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public override async Task<IEnumerable<Image>> ExecuteAsync(GetImagesByFolderIdRequest request)
    {
        return await _imageRepository.GetByFolderIdAsync(request.FolderId);
    }
}
