using ImageServiceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace ImageServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly HttpClient _httpClient;

        public ImageController(IPhotoService photoService)
        {
            _photoService = photoService;
            _httpClient = new HttpClient();
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var file = Request.Form.Files["file"];
            if (file != null && file.Length > 0)
            {
                // Resmi yükle
                string result = await _photoService.UploadImageAsync(new Dtos.PhotoCreationDto { File = file });

                // Log kaydet
                var logMessage = $"Uploaded file: {file.FileName} at {DateTime.UtcNow}";
                var logResponse = await _httpClient.PostAsJsonAsync("https://localhost:10608/api/log", new { message = logMessage });

                if (!logResponse.IsSuccessStatusCode)
                {
                    return StatusCode((int)logResponse.StatusCode, "Log kaydedilemedi.");
                }

                return Ok(result);
            }
            return BadRequest(new { message = "No File Received" });
        }
    }
}
