using Microsoft.AspNetCore.Http;

namespace sky.recovery.DTOs.RequestDTO
{
    public class UploadExcelDTO
    {
        public string userid { get; set; }

        public IFormFile? File { get; set; }

    }
}
