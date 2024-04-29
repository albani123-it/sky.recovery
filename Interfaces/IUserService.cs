using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IUserService
    {
        public Task<(bool? Error, GenericResponses<UserDetailDTO> Returns)> GetDataUser(string userid);
        public Task<(bool? Error, GenericResponses<RoleDTO> Returns)> GetRoles(int userlevel);

    }
}
