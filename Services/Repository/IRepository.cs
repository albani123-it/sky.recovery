using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Services.Repository
{
    public interface IRepository
    {
        public Task<(bool status, string message, List<dynamic> Data)> GetDetailUser(string userid);

    }
}
