using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using HandyWMS_Server.Cache.Base;
using HandyWMS_Server.Cache.Base.Factory;
using HandyWMS_Server.Models.SystemModels.MenusModels;
using HandyWMS_Server.Services.SystemServices.MenuServices;

namespace HandyWMS_Server.Cache
{
    public class MenuAuthorizeCache : BaseBusinessCache<MenuAuthorizeModel>
    {
        private MenuAuthorizeService menuAuthorizeService = new MenuAuthorizeService();

        public override string CacheKey => this.GetType().Name;

        public override async Task<List<MenuAuthorizeModel>> GetList()
        {
            var cacheList = CacheFactory.Cache.GetCache<List<MenuAuthorizeModel>>(CacheKey);
            if (cacheList == null)
            {
                var list = await menuAuthorizeService.GetList(null);
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
