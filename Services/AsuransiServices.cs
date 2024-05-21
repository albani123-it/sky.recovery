using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using sky.recovery.Helper.Config;
using sky.recovery.Insfrastructures;
using sky.recovery.Interfaces;
using sky.recovery.Responses;

namespace sky.recovery.Services
{
    public class AsuransiServices : SkyCoreConfig, IAsuransiServices
    {

        private IUserService _User { get; set; }
        private IGeneralParam _GeneralParam { get; set; }
        private readonly IWebHostEnvironment _environment;
        private IRestruktureRepository _postgreRepository { get; set; }
        private IHelperRepository _helperRepository { get; set; }

        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        AydaHelper _aydahelper = new AydaHelper();
        public AsuransiServices(IGeneralParam GeneralParam, IWebHostEnvironment environment, IUserService User, IHelperRepository helperRepository, IRestruktureRepository postgreRepository,
      IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _environment = environment;
            _GeneralParam = GeneralParam;
            _User = User;
            _postgreRepository = postgreRepository;
            _helperRepository = helperRepository;

        }







    }
}
