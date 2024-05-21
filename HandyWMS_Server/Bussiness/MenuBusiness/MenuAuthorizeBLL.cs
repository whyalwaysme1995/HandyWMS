using HandyWMS_ClassLibrary.Enum;
using HandyWMS_ClassLibrary.Enum.SystemManage;
using HandyWMS_ClassLibrary.Extensions;
using HandyWMS_ClassLibrary.Models;
using HandyWMS_ClassLibrary.Results.SystemManage;
using HandyWMS_Server.Cache;
using HandyWMS_Server.Models;
using HandyWMS_Server.Models.SystemModels.MenusModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWMS_Server.Bussiness.MenuBusiness
{
    public class MenuAuthorizeBLL
    {
        private MenuAuthorizeCache menuAuthorizeCache = new MenuAuthorizeCache();
        private MenuCache menuCache = new MenuCache();

        #region 获取数据
        public async Task<TData<List<MenuAuthorizeInfo>>> GetAuthorizeList(OperatorInfo user)
        {
            TData<List<MenuAuthorizeInfo>> obj = new TData<List<MenuAuthorizeInfo>>();
            obj.Data = new List<MenuAuthorizeInfo>();

            List<MenuAuthorizeModel> authorizeList = new List<MenuAuthorizeModel>();
            List<MenuAuthorizeModel> userAuthorizeList = null;
            List<MenuAuthorizeModel> roleAuthorizeList = null;

            var menuAuthorizeCacheList = await menuAuthorizeCache.GetList();
            var menuList = await menuCache.GetList();
            var enableMenuIdList = menuList.Where(p => p.MenuStatus == (int)StatusEnum.Yes).Select(p => p.Id).ToList();

            menuAuthorizeCacheList = menuAuthorizeCacheList.Where(p => enableMenuIdList.Contains(p.MenuId)).ToList();

            // 用户
            userAuthorizeList = menuAuthorizeCacheList.Where(p => p.AuthorizeId == user.UserId && p.AuthorizeType == AuthorizeTypeEnum.User.ParseToInt()).ToList();

            // 角色
            if (!string.IsNullOrEmpty(user.RoleIds))
            {
                List<long> roleIdList = user.RoleIds.Split(',').Select(p => long.Parse(p)).ToList();
                roleAuthorizeList = menuAuthorizeCacheList.Where(p => roleIdList.Contains(p.AuthorizeId.Value) && p.AuthorizeType == AuthorizeTypeEnum.Role.ParseToInt()).ToList();
            }

            // 排除重复的记录
            if (userAuthorizeList.Count > 0)
            {
                authorizeList.AddRange(userAuthorizeList);
                roleAuthorizeList = roleAuthorizeList.Where(p => !userAuthorizeList.Select(u => u.AuthorizeId).Contains(p.AuthorizeId)).ToList();
            }
            if (roleAuthorizeList != null && roleAuthorizeList.Count > 0)
            {
                authorizeList.AddRange(roleAuthorizeList);
            }

            foreach (MenuAuthorizeModel authorize in authorizeList)
            {
                obj.Data.Add(new MenuAuthorizeInfo
                {
                    MenuId = authorize.MenuId,
                    AuthorizeId = authorize.AuthorizeId,
                    AuthorizeType = authorize.AuthorizeType,
                    Authorize = menuList.Where(t => t.Id == authorize.MenuId).Select(t => t.Authorize).FirstOrDefault()
                });
            }
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
