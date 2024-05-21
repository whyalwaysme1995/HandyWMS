using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using HandyWMS_Server.Data.Repository;
using HandyWMS_ClassLibrary.Extensions;
using HandyWMS_Server.Models.SystemModels.DataDictModels;
using HandyWMS_ClassLibrary.Utils.TextUtils;
using HandyWMS_ClassLibrary.Models;
using HandyWMS_ClassLibrary.Params.SystemManage;

namespace HandyWMS_Server.Services.SystemServices.DataDictServices
{
    public class DataDictService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<DataDictModel>> GetList(DataDictListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<DataDictModel>> GetPageList(DataDictListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<DataDictModel> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<DataDictModel>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(DictSort) FROM SysDataDict");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }

        public bool ExistDictType(DataDictModel entity)
        {
            var expression = LinqExtensions.True<DataDictModel>();
            expression = expression.And(t => t.IsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.DictType == entity.DictType);
            }
            else
            {
                expression = expression.And(t => t.DictType == entity.DictType && t.Id != entity.Id);
            }
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        /// <summary>
        /// 是否存在字典值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ExistDictDetail(string dictType)
        {
            var expression = LinqExtensions.True<DataDictDetailModel>();
            expression = expression.And(t => t.DictType == dictType);
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(DataDictModel entity)
        {
            var db = await this.BaseRepository().BeginTrans();
            try
            {
                if (!entity.Id.IsNullOrZero())
                {
                    var dbEntity = await db.FindEntity<DataDictModel>(entity.Id.Value);
                    if (dbEntity.DictType != entity.DictType)
                    {
                        // 更新子表的DictType，因为2个表用DictType进行关联
                        IEnumerable<DataDictDetailModel> detailList = await db.FindList<DataDictDetailModel>(p => p.DictType == dbEntity.DictType);
                        foreach (DataDictDetailModel detailEntity in detailList)
                        {
                            detailEntity.DictType = entity.DictType;
                            await detailEntity.Modify();
                        }
                    }
                    dbEntity.DictType = entity.DictType;
                    dbEntity.Remark = entity.Remark;
                    dbEntity.DictSort = entity.DictSort;
                    await dbEntity.Modify();
                    await db.Update<DataDictModel>(dbEntity);
                }
                else
                {
                    await entity.Create();
                    await db.Insert<DataDictModel>(entity);
                }
                await db.CommitTrans();
            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextUtil.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<DataDictModel>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<DataDictModel, bool>> ListFilter(DataDictListParam param)
        {
            var expression = LinqExtensions.True<DataDictModel>();
            if (param != null)
            {
                if (!param.DictType.IsEmpty())
                {
                    expression = expression.And(t => t.DictType.Contains(param.DictType));
                }
                if (!param.Remark.IsEmpty())
                {
                    expression = expression.And(t => t.Remark.Contains(param.Remark));
                }
            }
            return expression;
        }
        #endregion
    }
}
