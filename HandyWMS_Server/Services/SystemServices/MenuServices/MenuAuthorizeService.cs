using HandyWMS_ClassLibrary.Extensions;
using HandyWMS_ClassLibrary.Utils.TextUtils;
using HandyWMS_Server.Data.Repository;
using HandyWMS_Server.Models.SystemModels.MenusModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWMS_Server.Services.SystemServices.MenuServices
{
    public class MenuAuthorizeService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<MenuAuthorizeModel>> GetList(MenuAuthorizeModel param)
        {
            var expression = LinqExtensions.True<MenuAuthorizeModel>();
            if (param != null)
            {
                if (param.AuthorizeId.ParseToLong() > 0)
                {
                    expression = expression.And(t => t.AuthorizeId == param.AuthorizeId);
                }
                if (param.AuthorizeType.ParseToInt() > 0)
                {
                    expression = expression.And(t => t.AuthorizeType == param.AuthorizeType);
                }
                if (!param.AuthorizeIds.IsEmpty())
                {
                    long[] authorizeIdArr = TextUtil.SplitToArray<long>(param.AuthorizeIds, ',');
                    expression = expression.And(t => authorizeIdArr.Contains(t.AuthorizeId.Value));
                }
            }
            var list = await this.BaseRepository().FindList<MenuAuthorizeModel>(expression);
            return list.ToList();
        }

        public async Task<MenuAuthorizeModel> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<MenuAuthorizeModel>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(MenuAuthorizeModel entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                await this.BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(long id)
        {
            await this.BaseRepository().Delete<MenuAuthorizeModel>(id);
        }
        #endregion
    }
}
