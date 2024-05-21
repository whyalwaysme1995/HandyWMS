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
    public class DataDictDetailCache : BaseBusinessCache<DataDictDetailModel>
    {
        private DataDictDetailService dataDictDetailService = new DataDictDetailService();

        public override string CacheKey => this.GetType().Name;

        public override async Task<List<DataDictDetailModel>> GetList()
        {
            var cacheList = CacheFactory.Cache.GetCache<List<DataDictDetailModel>>(CacheKey);
            if (cacheList == null)
            {
                var list = await dataDictDetailService.GetList(null);
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
