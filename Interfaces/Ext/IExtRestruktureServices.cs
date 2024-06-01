using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces.Ext
{
    public interface IExtRestruktureServices
    {
        public Task<(bool Status, string Message, List<dynamic> Data)> GetMonitoringlist(string userid);

    }
}
