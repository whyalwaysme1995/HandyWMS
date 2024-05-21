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
    public class MenuCache : BaseBusinessCache<MenuModel>
    {
        private MenuService menuService = new MenuService();

        public override string CacheKey => this.GetType().Name;

        public override async Task<List<MenuModel>> GetList()
        {
            var cacheList = CacheFactory.Cache.GetCache<List<MenuModel>>(CacheKey);
            if (cacheList == null)
            {
                var list = await menuService.GetList(null);
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
