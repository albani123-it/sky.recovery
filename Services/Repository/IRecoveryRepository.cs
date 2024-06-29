using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Services.Repository
{
    public interface IRecoveryRepository
    {
        public Task<(bool status, string message, List<dynamic?> Data)> GetMonitor(string userid, string services);

    }
}
