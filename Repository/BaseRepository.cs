
using Repository.BaseModel;
using SqlSugar.Extensions;
using SqlSugar;
using System.Linq.Expressions;

namespace Repository
{
    /// <summary>
    /// 仓储层基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
      where TEntity : class, new()
    {

        protected readonly ISqlSugarClient _sqlSugarClient;
        public BaseRepository(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient;
        }

        /// <summary>
        /// 根据主键Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> QueryById(object objId)
        {

            //_dbContext.Set<TEntity>.
            return await _sqlSugarClient.Queryable<TEntity>().In(objId).SingleAsync();
        }






        /// <summary>
        /// 功能描述:根据ID查询一条数据
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            //return await Task.Run(() => _db.Queryable<TEntity>().WithCacheIF(blnUseCache).InSingle(objId));
            return await _sqlSugarClient.Queryable<TEntity>().WithCacheIF(blnUseCache).In(objId).SingleAsync();
        }


        /// <summary>
        /// 功能描述:根据Id's查询多条数据
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            //return await Task.Run(() => _db.Queryable<TEntity>().In(lstIds).ToList());
            return await _sqlSugarClient.Queryable<TEntity>().In(lstIds).ToListAsync();
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {


            var i = await Task.Run(() => _sqlSugarClient.Insertable(entity).ExecuteCommandAsync());
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            return (int)i;
            //var insert = _sqlSugarClient.Insertable(entity).ExecuteReturnIdentityAsync();
            //return await insert;
        }


        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="insertColumns">指定只插入列</param>
        /// <returns>返回自增量列</returns>
        public async Task<int> Add(TEntity entity, Expression<Func<TEntity, object>> insertColumns = null)
        {
            var insert = _sqlSugarClient.Insertable(entity);
            if (insertColumns == null)
            {
                return await insert.ExecuteReturnIdentityAsync();
            }
            else
            {
                return await insert.InsertColumns(insertColumns).ExecuteReturnIdentityAsync();
            }
        }

        /// <summary>
        /// 批量插入实体(速度快)
        /// </summary>
        /// <param name="listEntity">实体集合</param>
        /// <returns>影响行数</returns>
        public async Task<int> AddMany(List<TEntity> listEntity)
        {
            return await _sqlSugarClient.Insertable(listEntity.ToArray()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            //var i = await Task.Run(() => _db.Updateable(entity).ExecuteCommand());
            //return i > 0;
            //这种方式会以主键为条件
            return await _sqlSugarClient.Updateable(entity).ExecuteCommandHasChangeAsync();
        }

        //批量更新
        public async Task<int> UpdateMany(List<TEntity> listEntity)
        {
            var i = await _sqlSugarClient.Updateable(listEntity).ExecuteCommandAsync();
            return i;
        }

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity entity)
        {

            return await _sqlSugarClient.Deleteable(entity).ExecuteCommandHasChangeAsync();
        }


        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {

            return await _sqlSugarClient.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {

            return await _sqlSugarClient.Deleteable<TEntity>().In(ids).ExecuteCommandHasChangeAsync();
        }


        public async Task<bool> DeleteByNotKeyIds(Expression<Func<TEntity, bool>> expression)
        {
            return await _sqlSugarClient.Deleteable<TEntity>().Where(expression).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 功能描述:查询所有数据
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAll()
        {
            return await _sqlSugarClient.Queryable<TEntity>().ToListAsync();
        }


        /// <summary>
        /// 功能描述:查询数据列表
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _sqlSugarClient.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }


        /// <summary>
        /// 功能描述:查询一个列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await _sqlSugarClient.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(strOrderByFileds != null, strOrderByFileds).ToListAsync();
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="isAsc">排序规则</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {

            return await _sqlSugarClient.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await _sqlSugarClient.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToListAsync();
        }

        /// <summary>
        /// 功能描述:查询前N条数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryTopN(
            Expression<Func<TEntity, bool>> whereExpression,
            int intTop,
            string strOrderByFileds)
        {
            return await _sqlSugarClient.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToListAsync();
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public async Task<ReturnModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression = null, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {

            RefAsync<int> totalCount = 0;
            var list = await _sqlSugarClient.Queryable<TEntity>()
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .ToPageListAsync(intPageIndex, intPageSize, totalCount);
            return new ReturnModel<TEntity>() { TotalCount = totalCount,  PageSize = intPageSize, Data = list };
        }


        /// <summary>
        /// 映射查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="whereExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<ReturnModel<TResult>> QueryPageWithSelect<TResult>(Expression<Func<TEntity, bool>> whereExpression = null, Expression<Func<TEntity, TResult>> selectExpression = null, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {

            RefAsync<int> totalCount = 0;
            var list = await _sqlSugarClient.Queryable<TEntity>()
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .Select(selectExpression)
             .ToPageListAsync(intPageIndex, intPageSize, totalCount);
            return new ReturnModel<TResult>() { TotalCount = totalCount,  PageSize = intPageSize, Data = list };
        }





        /// <summary> 
        ///查询-多表查询(2表)
        /// </summary> 
        /// <typeparam name="T">实体1</typeparam> 
        /// <typeparam name="T2">实体2</typeparam> 
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param> 
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param> 
        /// <param name="intPageIndex">页码索引</param>
        ///  <param name="intPageIndex">每页条数</param>
        /// <returns>值</returns>
        public async Task<ReturnModel<TResult>> QueryMuch<T, T2, TResult>(
            Expression<Func<T, T2, object[]>> joinExpression,
            Expression<Func<T, T2, TResult>> selectExpression,
            Expression<Func<T, T2, bool>> whereLambda = null,
            string strOrderByFileds = null,
            int intPageIndex = 1, int intPageSize = 10) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            List<TResult> list;
            var query = _sqlSugarClient.Queryable(joinExpression);
            if (whereLambda != null)
            {
                query = query.Where(whereLambda);
            }

            list=  await query.OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).Select(selectExpression).ToPageListAsync(intPageIndex, intPageSize, totalCount);


            return new ReturnModel<TResult>() { TotalCount = totalCount, PageSize = intPageSize, Data = list };
        }

        /// <summary> 
        ///查询-多表查询(3表)
        /// </summary> 
        /// <typeparam name="T">实体1</typeparam> 
        /// <typeparam name="T2">实体2</typeparam> 
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param> 
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param> 
        /// <param name="intPageIndex">页码索引</param>
        ///  <param name="intPageIndex">每页条数</param>
        /// <returns>值</returns>
        public async Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, TResult>(
            Expression<Func<T, T2, T3, object[]>> joinExpression,
            Expression<Func<T, T2, T3, TResult>> selectExpression,
            Expression<Func<T, T2, T3, bool>> whereLambda = null,
            string strOrderByFileds = null,
            int intPageIndex = 1, int intPageSize = 10) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            List<TResult> list;
            var query = _sqlSugarClient.Queryable(joinExpression);
            if (whereLambda != null)
            {
                query = query.Where(whereLambda);
            }

            list = await query.OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).Select(selectExpression).ToPageListAsync(intPageIndex, intPageSize, totalCount);


            return new ReturnModel<TResult>() { TotalCount = totalCount, PageSize = intPageSize, Data = list };

        }

        /// <summary> 
        ///查询-多表查询(4表)
        /// </summary> 
        /// <typeparam name="T">实体1</typeparam> 
        /// <typeparam name="T2">实体2</typeparam> 
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param> 
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param> 
        /// <param name="intPageIndex">页码索引</param>
        ///  <param name="intPageIndex">每页条数</param>
        /// <returns>值</returns>
        public async Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, T4, TResult>(
            Expression<Func<T, T2, T3, T4, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, bool>> whereLambda = null,
            string strOrderByFileds = null,
            int intPageIndex = 1, int intPageSize = 10) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            List<TResult> list;
            var query = _sqlSugarClient.Queryable(joinExpression);
            if (whereLambda != null)
            {
                query = query.Where(whereLambda);
            }

            list = await query.OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).Select(selectExpression).ToPageListAsync(intPageIndex, intPageSize, totalCount);


            return new ReturnModel<TResult>() { TotalCount = totalCount, PageSize = intPageSize, Data = list };

        }


        /// <summary> 
        ///查询-多表查询(5表)
        /// </summary> 
        /// <typeparam name="T">实体1</typeparam> 
        /// <typeparam name="T2">实体2</typeparam> 
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param> 
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param> 
        /// <param name="intPageIndex">页码索引</param>
        ///  <param name="intPageIndex">每页条数</param>
        /// <returns>值</returns>
        public async Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, T4, T5, TResult>(
            Expression<Func<T, T2, T3, T4, T5, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, T5, bool>> whereLambda = null,
            string strOrderByFileds = null,
            int intPageIndex = 1, int intPageSize = 10) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            List<TResult> list;
            var query = _sqlSugarClient.Queryable(joinExpression);
            if (whereLambda != null)
            {
                query = query.Where(whereLambda);
            }

            list = await query.OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).Select(selectExpression).ToPageListAsync(intPageIndex, intPageSize, totalCount);


            return new ReturnModel<TResult>() { TotalCount = totalCount, PageSize = intPageSize, Data = list };

        }

        /// <summary> 
        ///查询-多表查询(6表)
        /// </summary> 
        /// <typeparam name="T">实体1</typeparam> 
        /// <typeparam name="T2">实体2</typeparam> 
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param> 
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param> 
        /// <param name="intPageIndex">页码索引</param>
        ///  <param name="intPageIndex">每页条数</param>
        /// <returns>值</returns>
        public async Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, T4, T5, T6, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, bool>> whereLambda = null,
            string strOrderByFileds = null,
            int intPageIndex = 1, int intPageSize = 10) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            List<TResult> list;
            var query = _sqlSugarClient.Queryable(joinExpression);
            if (whereLambda != null)
            {
                query = query.Where(whereLambda);
            }

            list = await query.OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).Select(selectExpression).ToPageListAsync(intPageIndex, intPageSize, totalCount);


            return new ReturnModel<TResult>() { TotalCount = totalCount, PageSize = intPageSize, Data = list };

        }



        /// <summary>
        /// 根据sql语句查询
        /// </summary>
        /// <param name="strSql">完整的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>泛型集合</returns>
        public async Task<List<TEntity>> QuerySql(string strSql, SugarParameter[] parameters = null)
        {
            return await _sqlSugarClient.Ado.SqlQueryAsync<TEntity>(strSql, parameters);
        }

        /// <summary>
        /// 事务操作
        /// </summary>
        /// <typeparam name="T1">传入参数</typeparam>
        /// <typeparam name="T2">返回值</typeparam>
        /// <param name="fun">执行的方法</param>
        /// <returns></returns>
        public T2 Tran<T2>(Func<T2> fun) //where T2 : class
        {
            T2 t2 = default;
            try
            {
                _sqlSugarClient.Ado.BeginTran();
                //操作
                t2 = fun.Invoke();
                _sqlSugarClient.Ado.CommitTran();
                return t2;
            }
            catch (Exception ex)
            {
                _sqlSugarClient.Ado.RollbackTran();
                throw ex;
            }
        }

        public async Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> whereLambda = null, string strOrderByFileds = null, int intPageIndex = 1, int intPageSize = 10) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            List<TResult> list;
            var query = _sqlSugarClient.Queryable(joinExpression);
            if (whereLambda != null)
            {
                query = query.Where(whereLambda);
            }

            list = await query.OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).Select(selectExpression).ToPageListAsync(intPageIndex, intPageSize, totalCount);


            return new ReturnModel<TResult>() { TotalCount = totalCount, PageSize = intPageSize, Data = list };
        }


        public async Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> whereLambda = null, string strOrderByFileds = null, int intPageIndex = 1, int intPageSize = 10) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            List<TResult> list;
            var query = _sqlSugarClient.Queryable(joinExpression);
            if (whereLambda != null)
            {
                query = query.Where(whereLambda);
            }

            list = await query.OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).Select(selectExpression).ToPageListAsync(intPageIndex, intPageSize, totalCount);


            return new ReturnModel<TResult>() { TotalCount = totalCount, PageSize = intPageSize, Data = list };
        }


        public async Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> whereLambda = null, string strOrderByFileds = null, int intPageIndex = 1, int intPageSize = 10) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            List<TResult> list;
            var query = _sqlSugarClient.Queryable(joinExpression);
            if (whereLambda != null)
            {
                query = query.Where(whereLambda);
            }

            list = await query.Select(selectExpression).ToPageListAsync(intPageIndex, intPageSize, totalCount);

            //list = await query.OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).Select(selectExpression).ToPageListAsync(intPageIndex, intPageSize, totalCount);


            return new ReturnModel<TResult>() { TotalCount = totalCount, PageSize = intPageSize, Data = list };
        }


        /// <summary>
        /// 分组查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="groupbyExpression"></param>
        /// <param name="whereLambda"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchGroupBy<T, T2, TResult>(
         Expression<Func<T, T2, object[]>> joinExpression,
         Expression<Func<T, T2, TResult>> selectExpression,
          Expression<Func<T, T2, object>> groupbyExpression,
         Expression<Func<T, T2, bool>> whereLambda = null,
         string strOrderByFileds = null) where T : class, new()
        {
            List<TResult> list;
            var query = _sqlSugarClient.Queryable(joinExpression).GroupBy(groupbyExpression);
            if (whereLambda != null)
            {
                query = query.Where(whereLambda);
            }

            list = await query.OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).Select(selectExpression).ToListAsync();
            return list;
        }


        /// <summary>
        /// 分组查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="groupbyExpression"></param>
        /// <param name="whereLambda"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuchGroupBy<T, T2, T3, TResult>(
         Expression<Func<T, T2, T3, object[]>> joinExpression,
         Expression<Func<T, T2, T3, TResult>> selectExpression,
          Expression<Func<T, T2, T3, object>> groupbyExpression,
         Expression<Func<T, T2, T3, bool>> whereLambda = null,
         string strOrderByFileds = null) where T : class, new()
        {
            List<TResult> list;
            var query = _sqlSugarClient.Queryable(joinExpression).GroupBy(groupbyExpression);
            if (whereLambda != null)
            {
                query = query.Where(whereLambda);
            }

            list = await query.OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).Select(selectExpression).ToListAsync();
            return list;
        }

        public async Task<List<TResult>> QueryMuchGroupBy<T, T2, T3, T4, TResult>(Expression<Func<T, T2, T3, T4, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, object>> groupbyExpression, Expression<Func<T, T2, T3, T4, bool>> whereLambda = null, string strOrderByFileds = null) where T : class, new()
        {
            List<TResult> list;
            var query = _sqlSugarClient.Queryable(joinExpression).GroupBy(groupbyExpression);
            if (whereLambda != null)
            {
                query = query.Where(whereLambda);
            }

            list = await query.OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).Select(selectExpression).ToListAsync();
            return list;
        }
    }

}
