using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HandyWMS_Server.Data.Repository;
using HandyWMS_ClassLibrary.Utils.TextUtils;
using HandyWMS_Server.Models.SystemModels.AreaModels;
using HandyWMS_ClassLibrary.Params.SystemManage;
using HandyWMS_ClassLibrary.Models;
using HandyWMS_ClassLibrary.Extensions;

namespace HandyWMS_Server.Services.SystemServices.AreaServices
{
    public class AreaService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<AreaModel>> GetList(AreaParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<AreaModel>> GetPageList(AreaParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<AreaModel> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<AreaModel>(id);
        }

        public async Task<AreaModel> GetEntityByAreaCode(string areaCode)
        {
            return await this.BaseRepository().FindEntity<AreaModel>(p => p.AreaCode == areaCode);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(AreaModel entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<AreaModel>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<AreaModel>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextUtil.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<AreaModel>(idArr);
        }

        #endregion

        #region 私有方法
        private Expression<Func<AreaModel, bool>> ListFilter(AreaParam param)
        {
            var expression = LinqExtensions.True<AreaModel>();
            if (param != null)
            {
                if (!param.AreaName.IsEmpty())
                {
                    expression = expression.And(t => t.AreaName.Contains(param.AreaName));
                }
            }
            return expression;
        }
        #endregion
    }
}
