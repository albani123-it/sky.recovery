using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace sky.recovery.DTOs.RequestDTO.CommonDTO
{
    public class User
    {
        public string UserId { get; set; }
    }
    
    public class RepoDTO
    {
        public int FiturId { get; set; }
        public int RequestId { get; set; }
        public int DocType { get; set; }
        public IFormFile? File { get; set; }

    }

    public class RepoReqDTO

    {
        public string UserId { get; set; }
        public int FiturId { get; set; }
        public int RequestId { get; set; }
        public int DocType { get; set; }
        public IFormFile? File { get; set; }
    }
    public class FiturUploadDTO
    {
        public List<RepoDTO> _Repo { get; set; }

    }
    public class UploadDTO
    {
        public User _User { get; set; }
        public List<RepoDTO> _Repo { get; set; }
    }
}
