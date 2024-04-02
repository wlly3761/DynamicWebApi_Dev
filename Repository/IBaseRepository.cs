using Repository.BaseModel;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// 仓储层接口基类
    /// 提供最公共的增删改查功能
    /// </summary>
    public interface IBaseRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// 根据主键Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> QueryById(object objId);



        /// <summary>
        /// 功能描述:根据ID查询一条数据
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        Task<TEntity> QueryById(object objId, bool blnUseCache = false);




        /// <summary>
        /// 功能描述:根据Id's查询多条数据
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        Task<List<TEntity>> QueryByIDs(object[] lstIds);


        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        Task<int> Add(TEntity entity);



        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="insertColumns">指定只插入列</param>
        /// <returns>返回自增量列</returns>
        Task<int> Add(TEntity entity, Expression<Func<TEntity, object>> insertColumns = null);


        /// <summary>
        /// 批量插入实体(速度快)
        /// </summary>
        /// <param name="listEntity">实体集合</param>
        /// <returns>影响行数</returns>
        Task<int> AddMany(List<TEntity> listEntity);


        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        Task<bool> Update(TEntity entity);


        //批量更新
        Task<int> UpdateMany(List<TEntity> listEntity);
        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        Task<bool> Delete(TEntity entity);



        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        Task<bool> DeleteById(object id);


        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        Task<bool> DeleteByIds(object[] ids);

        Task<bool> DeleteByNotKeyIds(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 功能描述:查询所有数据
        /// </summary>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryAll();



        /// <summary>
        /// 功能描述:查询数据列表
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);



        /// <summary>
        /// 功能描述:查询一个列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);


        /// <summary>
        /// 功能描述:查询一个列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="isAsc">排序规则</param>
        /// <returns></returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);


        /// <summary>
        /// 功能描述:查询一个列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> Query(string strWhere, string strOrderByFileds);


        /// <summary>
        /// 功能描述:查询前N条数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryTopN(
            Expression<Func<TEntity, bool>> whereExpression,
            int intTop,
            string strOrderByFileds);



        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        Task<ReturnModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression = null, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null);



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
        Task<ReturnModel<TResult>> QueryPageWithSelect<TResult>(Expression<Func<TEntity, bool>> whereExpression = null, Expression<Func<TEntity, TResult>> selectExpression = null, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null);

        /// <summary> 
        ///查询-多表查询
        /// </summary> 
        /// <typeparam name="T">实体1</typeparam> 
        /// <typeparam name="T2">实体2</typeparam> 
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param> 
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param> 
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <returns>值</returns>
        Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, TResult>(
              Expression<Func<T, T2, T3, object[]>> joinExpression,
              Expression<Func<T, T2, T3, TResult>> selectExpression,
              Expression<Func<T, T2, T3, bool>> whereLambda = null,
               string strOrderByFileds = null,
              int intPageIndex = 1, int intPageSize = 20) where T : class, new();



        /// <summary> 
        ///查询-多表查询
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
        Task<ReturnModel<TResult>> QueryMuch<T, T2, TResult>(
           Expression<Func<T, T2, object[]>> joinExpression,
           Expression<Func<T, T2, TResult>> selectExpression,
           Expression<Func<T, T2, bool>> whereLambda = null,
           string strOrderByFileds = null,
           int intPageIndex = 1, int intPageSize = 20) where T : class, new();


        /// <summary>
        /// 四表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereLambda"></param>
        /// <param name="strOrderByFileds"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <returns></returns>
        Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, T4, TResult>(
            Expression<Func<T, T2, T3, T4, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, bool>> whereLambda = null,
            string strOrderByFileds = null,
            int intPageIndex = 1, int intPageSize = 10) where T : class, new();


        /// <summary>
        /// 五表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereLambda"></param>
        /// <param name="strOrderByFileds"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <returns></returns>
        Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, T4, T5, TResult>(
            Expression<Func<T, T2, T3, T4, T5, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, T5, bool>> whereLambda = null,
            string strOrderByFileds = null,
            int intPageIndex = 1, int intPageSize = 10) where T : class, new();

        /// <summary>
        /// 六表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereLambda"></param>
        /// <param name="strOrderByFileds"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <returns></returns>
        Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, T4, T5, T6, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, bool>> whereLambda = null,
            string strOrderByFileds = null,
            int intPageIndex = 1, int intPageSize = 10) where T : class, new();



        Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, T4, T5, T6, T7, TResult>(
       Expression<Func<T, T2, T3, T4, T5, T6, T7, object[]>> joinExpression,
       Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> selectExpression,
       Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> whereLambda = null,
       string strOrderByFileds = null,
       int intPageIndex = 1, int intPageSize = 10) where T : class, new();

        Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, T4, T5, T6, T7, T8, TResult>(
    Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object[]>> joinExpression,
    Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> selectExpression,
    Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> whereLambda = null,
    string strOrderByFileds = null,
    int intPageIndex = 1, int intPageSize = 10) where T : class, new();

        Task<ReturnModel<TResult>> QueryMuch<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
  Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object[]>> joinExpression,
  Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> selectExpression,
  Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> whereLambda = null,
  string strOrderByFileds = null,
  int intPageIndex = 1, int intPageSize = 10) where T : class, new();

        /// <summary>
        /// 原生sql查询
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">参数集合</param>
        /// <returns></returns>
        Task<List<TEntity>> QuerySql(string strSql, SugarParameter[] parameters = null);

        /// <summary>
        /// 事务操作
        /// </summary>
        /// <typeparam name="T1">传入参数</typeparam>
        /// <typeparam name="T2">返回值</typeparam>
        /// <param name="fun">执行的方法</param>
        /// <returns></returns>
        T2 Tran<T2>(Func<T2> fun); //where T2 : class;





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
        Task<List<TResult>> QueryMuchGroupBy<T, T2, TResult>(
        Expression<Func<T, T2, object[]>> joinExpression,
        Expression<Func<T, T2, TResult>> selectExpression,
         Expression<Func<T, T2, object>> groupbyExpression,
        Expression<Func<T, T2, bool>> whereLambda = null,
        string strOrderByFileds = null) where T : class, new();



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
        Task<List<TResult>> QueryMuchGroupBy<T, T2, T3, TResult>(
         Expression<Func<T, T2, T3, object[]>> joinExpression,
         Expression<Func<T, T2, T3, TResult>> selectExpression,
          Expression<Func<T, T2, T3, object>> groupbyExpression,
         Expression<Func<T, T2, T3, bool>> whereLambda = null,
         string strOrderByFileds = null) where T : class, new();

        /// <summary>
        /// 分组查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="groupbyExpression"></param>
        /// <param name="whereLambda"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        Task<List<TResult>> QueryMuchGroupBy<T, T2, T3, T4, TResult>(
         Expression<Func<T, T2, T3, T4, object[]>> joinExpression,
         Expression<Func<T, T2, T3, T4, TResult>> selectExpression,
          Expression<Func<T, T2, T3, T4, object>> groupbyExpression,
         Expression<Func<T, T2, T3, T4, bool>> whereLambda = null,
         string strOrderByFileds = null) where T : class, new();

    }

}
