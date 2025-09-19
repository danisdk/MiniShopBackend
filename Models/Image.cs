using System.ComponentModel.DataAnnotations;

namespace MiniShop.Models;

public class Image : Frozen
{
    [MaxLength(255)]
    public string Name { get; set; }
    [MaxLength(255)]
    public string StoredName { get; set; }
    [MaxLength(512)] 
    public string FilePath { get; set; }
    public long Size { get; set; }
}