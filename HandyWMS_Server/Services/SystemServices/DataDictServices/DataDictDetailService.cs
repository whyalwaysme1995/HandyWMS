using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using HandyWMS_Server.Data.Repository;
using HandyWMS_Server.Models.SystemModels.DataDictModels;
using HandyWMS_ClassLibrary.Params.SystemManage;
using HandyWMS_ClassLibrary.Models;
using HandyWMS_ClassLibrary.Extensions;
using HandyWMS_ClassLibrary.Utils.TextUtils;

namespace HandyWMS_Server.Services.SystemServices.DataDictServices
{
    public class DataDictDetailService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<DataDictDetailModel>> GetList(DataDictParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.OrderBy(p => p.DictSort).ToList();
        }

        public async Task<List<DataDictDetailModel>> GetPageList(DataDictParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<DataDictDetailModel> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<DataDictDetailModel>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(DictSort) FROM SysDataDictDetail");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }

        public bool ExistDictKeyValue(DataDictDetailModel entity)
        {
            var expression = LinqExtensions.True<DataDictDetailModel>();
            expression = expression.And(t => t.IsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.DictType == entity.DictType && (t.DictKey == entity.DictKey || t.DictValue == entity.DictValue));
            }
            else
            {
                expression = expression.And(t => t.DictType == entity.DictType && (t.DictKey == entity.DictKey || t.DictValue == entity.DictValue) && t.Id != entity.Id);
            }
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(DataDictDetailModel entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<DataDictDetailModel>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<DataDictDetailModel>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextUtil.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<DataDictDetailModel>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<DataDictDetailModel, bool>> ListFilter(DataDictParam param)
        {
            var expression = LinqExtensions.True<DataDictDetailModel>();
            if (param != null)
            {
                if (param.DictKey.ParseToInt() > 0)
                {
                    expression = expression.And(t => t.DictKey == param.DictKey);
                }

                if (!string.IsNullOrEmpty(param.DictValue))
                {
                    expression = expression.And(t => t.DictValue.Contains(param.DictValue));
                }

                if (!string.IsNullOrEmpty(param.DictType))
                {
                    expression = expression.And(t => t.DictType.Contains(param.DictType));
                }
            }
            return expression;
        }
        #endregion
    }
}
