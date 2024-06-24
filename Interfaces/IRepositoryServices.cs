using sky.recovery.DTOs.RepositoryDTO;
using sky.recovery.DTOs.RequestDTO.CommonDTO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IRepositoryServices
    {
        public Task<(bool status, string message)> DocumentNotes(List<DocNotesDTO> Entity);
        public Task<(bool status, string message, string path, string name)> RetrieveFilePath(int id, int fiturid);

        public Task<(bool Status, string Message)> UploadToLocalServices(string userid, RepoReqDTO Entity);
        public Task<(bool Status, string Message)> UploadToFTPServices(string userid, RepoReqDTO Entity);

        public Task<(bool Status, string Message)> RemoveDoc(int id);
        //public Task<(bool Status, string Message)> UploadServices(string userid, RepoReqDTO Entity);
        public Task<(bool status, string message, MemoryStream x)> DownloadFromFTP(string url);

    }
}
