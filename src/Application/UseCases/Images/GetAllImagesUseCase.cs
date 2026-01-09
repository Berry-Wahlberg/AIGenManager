using AIGenManager.Core.Application.Ports;
using AIGenManager.Core.Domain.Entities;

namespace AIGenManager.Application.UseCases.Images;

public class GetAllImagesUseCase : UseCase<IEnumerable<Image>>
{
    private readonly IImageRepository _imageRepository;

    public GetAllImagesUseCase(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public override async Task<IEnumerable<Image>> ExecuteAsync()
    {
        return await _imageRepository.GetAllAsync();
    }
}
