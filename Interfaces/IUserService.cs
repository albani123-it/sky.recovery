using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IUserService
    {
        public Task<(bool Error, GeneralResponses Returns)> GetDataUser(string userid);

    }
}
