using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IUserService
    {
        public Task<(bool? Status, GenericResponses<UserDetailDTO> Returns)> GetDataUser(string userid);
        public Task<(bool? Status, GenericResponses<RoleDTO> Returns)> GetRoles(int userlevel);
        public Task<(bool status, string message, bool Result)> GetValidationPermission(string userid, string url);

    }
}
