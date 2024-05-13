using static sky.recovery.Services.Repository.RestruktureRepoConfig;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IGeneralParam
    {
        public Task<GeneralParamReturn> GetParamDetail(int IdParamHeader);

    }
}
