using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IRecoveryServices
    {
        public Task<(bool Error, GeneralResponses Returns)> ListCollection(string userid);
        public Task<(bool Error, GeneralResponses Returns)> ListRestructure();
        public Task<(bool Error, GeneralResponses Returns)> MonitoringListDetail(string userid);
        public Task<(bool Error, GeneralResponses Returns)> GetRestrukturDetail(string cucif);

    }
}
