using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Reflection.Metadata;

namespace MtuSetsAPIs.Global
{
    public class CloudinaryService
    {
        public static async Task<string> UploadImageAsync(IFormFile file)
        {
            Cloudinary cloudinary;
            var acc = new Account(CloudinarySettings.CloudName, CloudinarySettings.ApiKey, CloudinarySettings.ApiSecret);
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
            var publicId = "";
            // Assuming the URL is something like https://res.cloudinary.com/demo/image/upload/v1/sample.jpg
            try
            {
                var uri = new Uri(url);
                var segments = uri.Segments;
                var publicIdWithExtension = segments.Last();  // e.g., "sample.jpg"
                publicId = publicIdWithExtension.Substring(0, publicIdWithExtension.LastIndexOf('.'));
            }
            catch { }
            return publicId;
        }
        public static async Task<bool> DeleteImageAsync(string imageUrl)
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
            for (int i = 0; i < imageUrl.Length; i++)
            {
                if (!await DeleteImageAsync(imageUrl[i]))
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



        public static async Task<string> UploadPdfAsync(IFormFile file)
        {
            Cloudinary cloudinary;
            var acc = new Account(CloudinarySettings.CloudName, CloudinarySettings.ApiKey, CloudinarySettings.ApiSecret);
            cloudinary = new Cloudinary(acc);

            var uploadResult = new RawUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new RawUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream)
                    };

                    uploadResult = await cloudinary.UploadAsync(uploadParams);
                }
            }

            return uploadResult.Url.ToString();
        }
        public static async Task<List<string>> UploadPdfsAsync(List<IFormFile> files)
        {
            Cloudinary cloudinary;
            var acc = new Account(CloudinarySettings.CloudName, CloudinarySettings.ApiKey, CloudinarySettings.ApiSecret);
            cloudinary = new Cloudinary(acc);

            var uploadTasks = files.Select(file =>
            {
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };
                return cloudinary.UploadAsync(uploadParams);
            });

            var uploadResults = await Task.WhenAll(uploadTasks);

            return uploadResults.Select(result => result.Url.ToString()).ToList();
        }
        public static async Task<string> ReplacePdfAsync(IFormFile file, string existingPdfUrl)
        {
            Cloudinary cloudinary;
            var acc = new Account(CloudinarySettings.CloudName, CloudinarySettings.ApiKey, CloudinarySettings.ApiSecret);
            cloudinary = new Cloudinary(acc);

            // Extract the public ID from the existing URL
            var publicId = GetPublicIdFromUrl(existingPdfUrl);

            // Delete the existing PDF
            var deletionParams = new DeletionParams(publicId);
            await cloudinary.DestroyAsync(deletionParams);

            // Upload the new PDF
            return await UploadPdfAsync(file);
        }
        public static async Task<bool> DeletePdfAsync(string pdfUrl)
        {
            Cloudinary cloudinary;
            var acc = new Account(CloudinarySettings.CloudName, CloudinarySettings.ApiKey, CloudinarySettings.ApiSecret);
            cloudinary = new Cloudinary(acc);

            var publicId = GetPublicIdFromUrl(pdfUrl);
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await cloudinary.DestroyAsync(deletionParams);

            return deletionResult.Result == "ok";
        }


        public static async Task<List<string>> ReplacePdfsAsync(List<string> existingPdfUrls, List<IFormFile> newFiles)
        {
            var cloudinary = new Cloudinary(new Account(
                CloudinarySettings.CloudName,
                CloudinarySettings.ApiKey,
                CloudinarySettings.ApiSecret
            ));

            // Delete old PDFs in bulk
            //var publicIds = existingPdfUrls.Select(GetPublicIdFromUrl).ToList();
            var deleteParams = new DelResParams
            {
                //PublicIds = publicIds,
                PublicIds = existingPdfUrls,
                Type = "upload",
                ResourceType = ResourceType.Raw
            };
            var deleteResult = await cloudinary.DeleteResourcesAsync(deleteParams);
            //Console.WriteLine(deleteResult.JsonObj); // Optional: log delete result for debugging

            // Upload new PDFs
            var uploadTasks = newFiles.Select(async file =>
            {
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                return uploadResult;
            });

            var uploadResults = await Task.WhenAll(uploadTasks);
            return uploadResults
                .Where(result => result != null)
                .Select(result => result.Url.ToString())
                .ToList();
        }



    }
}
