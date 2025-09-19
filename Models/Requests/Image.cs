namespace MiniShop.Models.Requests;

public class ImageRequest
{
    public string Name { get; set; }
    public IFormFile File { get; set; }
}