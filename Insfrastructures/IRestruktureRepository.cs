using System.Collections.Generic;

namespace sky.recovery.Insfrastructures
{
    public interface IRestruktureRepository
    {
        public List<dynamic> GetRestukture(int Type, string SPName, string FilterStatus);
        public List<dynamic> GetTaskList(string consstring, string spname, string FilterStatus);
        public List<dynamic> GetMonitoring(string consstring, string spname);



    }
}
