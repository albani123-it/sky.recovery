using sky.recovery.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Insfrastructures
{
    public interface IHelperRepository
    {
        public Task<List<GeneralParamHeader>> GetParamConfig(string consstring, string spname);

    }
}
