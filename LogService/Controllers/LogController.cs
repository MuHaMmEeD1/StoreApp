using Microsoft.AspNetCore.Mvc;

namespace LogService.Controllers
{
    public class LogController : ControllerBase
    {

        private readonly string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");

        [HttpPost]
        public IActionResult AddLog(string log)
        {
            try
            {
                // Log mesajını dosyaya ekle
                System.IO.File.AppendAllText(logFilePath, log + Environment.NewLine);
                return Ok("Log kaydedildi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata: {ex.Message}");
            }
        }




    }
}
