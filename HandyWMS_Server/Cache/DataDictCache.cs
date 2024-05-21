using HandyWMS_Server.Cache.Base;
using HandyWMS_Server.Cache.Base.Factory;
using HandyWMS_Server.Models.SystemModels.DataDictModels;
using HandyWMS_Server.Services.SystemServices.DataDictServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWMS_Server.Cache
{
    public class DataDictCache : BaseBusinessCache<DataDictModel>
    {
        private DataDictService dataDictService = new DataDictService();

        public override string CacheKey => this.GetType().Name;

        public override async Task<List<DataDictModel>> GetList()
        {
            var cacheList = CacheFactory.Cache.GetCache<List<DataDictModel>>(CacheKey);
            if (cacheList == null)
            {
                var list = await dataDictService.GetList(null);
                CacheFactory.Cache.SetCache(CacheKey, list);
                return list;
            }
            else
            {
                return cacheList;
            }
        }
    }
}
