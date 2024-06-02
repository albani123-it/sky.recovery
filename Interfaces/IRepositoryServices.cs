using sky.recovery.DTOs.RequestDTO.CommonDTO;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IRepositoryServices
    {
        public  Task<(bool Status, string Message)> RemoveDoc(int id);
        public Task<(bool Status, string Message)> UploadServices(RepoReqDTO Entity);

    }
}
