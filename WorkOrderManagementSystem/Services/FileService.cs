using Microsoft.AspNetCore.Components.Forms;

namespace WorkOrderManagementSystem.Services;

public class FileService
{
    private readonly IWebHostEnvironment _environment;
    private const long MaxFileSize = 10 * 1024 * 1024; // 10 MB

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> UploadFileAsync(IBrowserFile file)
    {
        var uploadDir = Path.Combine(_environment.WebRootPath, "uploads");
        if (!Directory.Exists(uploadDir))
        {
            Directory.CreateDirectory(uploadDir);
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
        var filePath = Path.Combine(uploadDir, fileName);

        await using var stream = file.OpenReadStream(MaxFileSize);
        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await stream.CopyToAsync(fileStream);

        return $"/uploads/{fileName}";
    }
}
