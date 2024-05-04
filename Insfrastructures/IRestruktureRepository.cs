using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Insfrastructures
{
    public interface IRestruktureRepository
    {
        public Task<List<dynamic>> GetRestukture(int Type, string SPName, string FilterStatus,string UserId);
        public Task<List<dynamic>> GetTaskList(string consstring, string spname, string FilterStatus, string UserId);
        public Task<List<dynamic>> GetMonitoring(string consstring, string spname,string UserId);



    }
}
