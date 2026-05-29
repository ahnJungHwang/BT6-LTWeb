namespace BTVN6.Helpers;

public static class ImageHelper
{
    public static async Task<string?> SaveImageAsync(IFormFile? image, IWebHostEnvironment env)
    {
        if (image == null || image.Length == 0)
            return null;

        var uploadsFolder = Path.Combine(env.WebRootPath, "images");
        Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await image.CopyToAsync(stream);

        return $"/images/{fileName}";
    }
}
