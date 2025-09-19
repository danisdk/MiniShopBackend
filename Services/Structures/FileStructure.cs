namespace MiniShop.Services.Structures;

public class FileStructure
{
    public string StoredName;
    public string FullPath;
    public long Size;
    
    public static async Task<FileStructure> UploadFile(IFormFile file)
    {
        string uploadPath = GetDirectoryForSave();
        string safeFileName = Path.GetFileName(file.FileName);
        string uniqueFileName = $"{Guid.NewGuid()}_{safeFileName}";
        string fullPath = Path.Combine(uploadPath, uniqueFileName);
        using (FileStream fs = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(fs);
        }

        FileStructure fileStructure = new FileStructure()
        {
            StoredName = uniqueFileName,
            FullPath = GetRelativePathFromFilePath(fullPath),
            Size = file.Length
        };
        return fileStructure; 
    }

    public static string GetDirectoryForSave()
    {
        string uploadPath = Constants.UploadPath;
        DateTime dateTime = DateTime.UtcNow;
        string year = dateTime.ToString("yyyy");
        string month = dateTime.ToString("MM");
        string day = dateTime.ToString("dd");
        string hour = dateTime.ToString("HH");
        string directoryForSave = Path.Combine(uploadPath, year, month, day, hour);
        Directory.CreateDirectory(directoryForSave);
        return directoryForSave;
    }

    public static string GetFilePathFromRelativePath(string relativePath)
    {
        string fullPath = Path.Combine(relativePath.Split('/')).Replace(Constants.UploadUrl, "");
        return Path.Combine(Constants.UploadPath, fullPath);
    }

    public static string GetRelativePathFromFilePath(string filePath)
    {
        return filePath.Replace(Constants.UploadPath, Constants.UploadUrl).Replace("\\", "/");
    }
    
    public static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }
    
}