using HandyWMS_ClassLibrary.Extensions;
using HandyWMS_ClassLibrary.Params.SystemManage;
using HandyWMS_ClassLibrary.Utils.TextUtils;
using HandyWMS_Server.Data.Repository;
using HandyWMS_Server.Models.SystemModels.MenusModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HandyWMS_Server.Services.SystemServices.MenuServices
{
    public class MenuService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<MenuModel>> GetList(MenuParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList<MenuModel>(expression);
            return list.OrderBy(p => p.MenuSort).ToList();
        }

        public async Task<MenuModel> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<MenuModel>(id);
        }

        public async Task<int> GetMaxSort(long parentId)
        {
            string where = string.Empty;
            if (parentId > 0)
            {
                where += " AND ParentId = " + parentId;
            }
            object result = await this.BaseRepository().FindObject("SELECT MAX(MenuSort) FROM SysMenu where BaseIsDelete = 0 " + where);
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }

        public bool ExistMenuName(MenuModel entity)
        {
            var expression = LinqExtensions.True<MenuModel>();
            expression = expression.And(t => t.IsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.MenuName == entity.MenuName && t.MenuType == entity.MenuType);
            }
            else
            {
                expression = expression.And(t => t.MenuName == entity.MenuName && t.MenuType == entity.MenuType && t.Id != entity.Id);
            }
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(MenuModel entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            var db = await this.BaseRepository().BeginTrans();
            try
            {
                long[] idArr = TextUtil.SplitToArray<long>(ids, ',');
                await db.Delete<MenuModel>(p => idArr.Contains(p.Id.Value) || idArr.Contains(p.ParentId.Value));
                await db.Delete<MenuAuthorizeModel>(p => idArr.Contains(p.MenuId.Value));
                await db.CommitTrans();
            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }
        #endregion

        #region 私有方法
        private Expression<Func<MenuModel, bool>> ListFilter(MenuParam param)
        {
            var expression = LinqExtensions.True<MenuModel>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.MenuName))
                {
                    expression = expression.And(t => t.MenuName.Contains(param.MenuName));
                }
                if (param.MenuStatus > -1)
                {
                    expression = expression.And(t => t.MenuStatus == param.MenuStatus);
                }
            }
            return expression;
        }
        #endregion

    }
}
