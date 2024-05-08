using Microsoft.AspNetCore.Http;

namespace sky.recovery.DTOs.RequestDTO
{
    public class UploadDocRestrukturDTO
    {
        public int? IdLoan { get; set; }
        public int? IdRestrukture { get; set; }
        public int? IdDocType { get; set; }
        public string jenisdocdesc { get; set; }
        public IFormFile? File { get; set; }

       
        public string UserId { get; set; }
        
    }
}
