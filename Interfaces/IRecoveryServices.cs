using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IRecoveryServices
    {
        public Task<(bool Error, GeneralResponses Returns)> ListCollection(string userid);

    }
}
