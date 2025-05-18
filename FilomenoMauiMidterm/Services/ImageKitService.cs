using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imagekit.Sdk;

namespace FilomenoMauiMidterm.Services
{
    public class ImageKitService
    {

        private readonly ImagekitClient _client;

        public ImageKitService(ImagekitClient client)
        {
            _client = client;
        }

        public async Task<string> UploadImageAsync(byte[] imageData, string fileName)
        {
            var uploadRequest = new FileCreateRequest
            {
                file = imageData,
                fileName = fileName,
                useUniqueFileName = true
            };

            var response = await _client.UploadAsync(uploadRequest);
            return response.url;
        }
    }
}
