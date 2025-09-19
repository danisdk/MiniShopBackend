using System.Xml;
using AutoMapper;
using MiniShop.Models;
using MiniShop.Models.Responses;
using MiniShop.Services.Structures;
using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName;

namespace MiniShop.Services;

public class ImageService : FrozenService<Image>
{
    public ImageService(
        ApplicationContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger<ImageService> logger
    ) : base(context, httpContextAccessor, mapper, logger){}
    
    public override async Task AddAsync(Image entity)
    {
        IFormFile? file = PopContextValue<IFormFile>("Image");
        if (file == null)
        {
            throw new FileNotFoundException("Файл не передан");
        }
        if (file.Length > Constants.MaxFileSize)
        {
            throw new FileTooLargeException(
                $"Файл слишком большой. Максимальный размер: {FileStructure.FormatFileSize(Constants.MaxFileSize)}"
                );
        }
        FileStructure fileStructure = await FileStructure.UploadFile(file);
        Image image = Mapper.Map<FileStructure, Image>(fileStructure);
        image.Name = entity.Name;
        image.FilePath = fileStructure.FullPath;
        await base.AddAsync(image);
    }

    public override async Task UpdateAsync(Image entity, Image entityToUpdate)
    {
        IFormFile? file = PopContextValue<IFormFile>("Image");
        if (file == null)
            throw new FileNotFoundException("Файл не передан");
        if (file.Length > Constants.MaxFileSize)
        {
            throw new FileTooLargeException(
                $"Файл слишком большой. Максимальный размер: {FileStructure.FormatFileSize(Constants.MaxFileSize)}"
            );
        }
        string fullPath = FileStructure.GetFilePathFromRelativePath(entity.FilePath);
        Logger.LogInformation($"fullPath: {fullPath}");
        if(File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        FileStructure fileStructure = await FileStructure.UploadFile(file);
        Image imageForUpdate = Mapper.Map<FileStructure, Image>(fileStructure);
        imageForUpdate.Name = entityToUpdate.Name;
        imageForUpdate.FilePath = fileStructure.FullPath;
        await base.UpdateAsync(entity, imageForUpdate);
    }

    public override async Task DeleteAsync(Image entity)
    {
        string fullPath = FileStructure.GetFilePathFromRelativePath(entity.FilePath);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        await base.DeleteAsync(entity);
    }

    public override async Task AfterDeleteAsync(Image entity)
    {
        await base.AfterDeleteAsync(entity);
        string fullPath = FileStructure.GetFilePathFromRelativePath(entity.FilePath);
        if(File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}

public class FileNotFoundException : Exception
{
    public FileNotFoundException(string message) : base(message) { }
}

public class FileTooLargeException : Exception
{
    public FileTooLargeException(string message) : base(message) { }
}