using Microsoft.AspNetCore.Http;

namespace SP.Simple.Contracts.Requests.Images
{
    public class ImagesUploadRequest
    {
        public IFormFile Image { get; set; }
        public string? AltText { get; set; }
    }
}
