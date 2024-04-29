using Microsoft.Extensions.Options;
using sky.recovery.Entities;
using sky.recovery.Helper.Config;
using sky.recovery.Insfrastructures;
using sky.recovery.Interfaces;

namespace sky.recovery.Services
{
    public class CollectionServices: SkyCollConfig, ICollectionService
    {
        private ICollectionService _Collection{ get; set; }

        public CollectionServices(ICollectionService Collection, IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _Collection = Collection;


        }


    }
}
