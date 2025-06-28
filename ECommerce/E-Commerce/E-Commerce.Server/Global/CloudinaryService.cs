using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using E_Commerce.Server.Global;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
public class CloudinaryService
{
    public static async Task<string> UploadImageAsync(IFormFile file)
    {
        Cloudinary cloudinary;
        var acc = new Account(CloudinarySettings.CloudName,CloudinarySettings.ApiKey,CloudinarySettings.ApiSecret);
        cloudinary = new Cloudinary(acc);


        var uploadResult = new ImageUploadResult();

        if (file.Length > 0)
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };

                uploadResult = await cloudinary.UploadAsync(uploadParams);
            }
        }

        return uploadResult.Url.ToString();
    }

    public static async Task<string> ReplaceImageAsync(IFormFile file, string existingImageUrl)
    {
        Cloudinary cloudinary;
        var acc = new Account(CloudinarySettings.CloudName, CloudinarySettings.ApiKey, CloudinarySettings.ApiSecret);
        cloudinary = new Cloudinary(acc);

        // Extract the public ID from the existing URL
        var publicId = GetPublicIdFromUrl(existingImageUrl);

        // Delete the existing image
        var deletionParams = new DeletionParams(publicId);
        await cloudinary.DestroyAsync(deletionParams);

        // Upload the new image
        return await UploadImageAsync(file);
    }
    private static string GetPublicIdFromUrl(string url)
    {
        // Assuming the URL is something like https://res.cloudinary.com/demo/image/upload/v1/sample.jpg
        var uri = new Uri(url);
        var segments = uri.Segments;
        var publicIdWithExtension = segments.Last();  // e.g., "sample.jpg"
        var publicId = publicIdWithExtension.Substring(0, publicIdWithExtension.LastIndexOf('.'));
        return publicId;
    }
    public static  async Task<bool> DeleteImageAsync(string imageUrl)
    {
        Cloudinary cloudinary;
        var acc = new Account(CloudinarySettings.CloudName, CloudinarySettings.ApiKey, CloudinarySettings.ApiSecret);
        cloudinary = new Cloudinary(acc);

        // Extract the public ID from the existing URL
        var publicId = GetPublicIdFromUrl(imageUrl);

        // Delete the image from Cloudinary
        var deletionParams = new DeletionParams(publicId);
        var deletionResult = await cloudinary.DestroyAsync(deletionParams);

        return deletionResult.Result == "ok";
    }

    public static async Task<bool> DeleteImagesAsync(string[] imageUrl)
    {
        for(int i = 0; i < imageUrl.Length; i++)
        {
            if(!await DeleteImageAsync(imageUrl[i]))
                return false;
        }

        return true;
    }

    public static async Task<List<string>> ReplaceImagesAsync(List<string> existingImageUrls, List<IFormFile> newFiles)
    {
        Cloudinary cloudinary;
        var acc = new Account(CloudinarySettings.CloudName, CloudinarySettings.ApiKey, CloudinarySettings.ApiSecret);
        cloudinary = new Cloudinary(acc);


        // Silme işlemleri
        var deleteTasks = existingImageUrls.Select(url =>
        {
            var publicId = GetPublicIdFromUrl(url);
            var deletionParams = new DeletionParams(publicId);
            return cloudinary.DestroyAsync(deletionParams);
        });

        // Silme işlemlerini bekleme
        await Task.WhenAll(deleteTasks);

        // Yükleme işlemleri
        var uploadTasks = newFiles.Select(file =>
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };
            return cloudinary.UploadAsync(uploadParams);
        });

        // Yükleme işlemlerini bekleme ve yeni URL'leri toplama
        var uploadResults = await Task.WhenAll(uploadTasks);

        return uploadResults.Select(result => result.Url.ToString()).ToList();
    }
    public static async Task<List<string>> UploadImagesAsync(List<IFormFile> files)
    {
        Cloudinary cloudinary;
        var acc = new Account(CloudinarySettings.CloudName, CloudinarySettings.ApiKey, CloudinarySettings.ApiSecret);
        cloudinary = new Cloudinary(acc);


        var uploadTasks = files.Select(file =>
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };
            return cloudinary.UploadAsync(uploadParams);
        });

        var uploadResults = await Task.WhenAll(uploadTasks);

        return uploadResults.Select(result => result.Url.ToString()).ToList();
    }
}
