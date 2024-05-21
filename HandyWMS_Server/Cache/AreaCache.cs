using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using HandyWMS_Server.Cache.Base;
using HandyWMS_Server.Models.SystemModels.AreaModels;
using HandyWMS_Server.Services.SystemServices.AreaServices;
using HandyWMS_Server.Cache.Base.Factory;

namespace HandyWMS_Server.Cache
{
    public class AreaCache : BaseBusinessCache<AreaModel>
    {
        private AreaService areaService = new AreaService();

        public override string CacheKey => this.GetType().Name;

        public override async Task<List<AreaModel>> GetList()
        {
            var cacheList = CacheFactory.Cache.GetCache<List<AreaModel>>(CacheKey);
            if (cacheList == null)
            {
                var result = await areaService.GetList(null);
                CacheFactory.Cache.SetCache(CacheKey, result);
                return result;
            }
            else
            {
                return cacheList;
            }
        }
    }
}
