using System.Collections.Generic;

namespace sky.recovery.Insfrastructures
{
    public interface IAydaRepository
    {
        public List<dynamic> GetAyda(int Type, string SPName, string FilterStatus);
        public List<dynamic> GetTaskList(string consstring, string spname, string FilterStatus);
        public List<dynamic> GetMonitoring(string consstring, string spname);
    }
}
