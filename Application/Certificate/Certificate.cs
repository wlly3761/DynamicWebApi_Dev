using ApplicationCommon;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using SqlSugar;
using SqlSugar.IOC;

namespace Application.Certificate
{
    [DynamicApiInterface]
    [ServiceRegistry(ServicelLifeCycle = "Scoped")]
    public class Certificate : ICertificate
    {
        private readonly ISqlSugarClient sugarClient1;
        private readonly ISqlSugarClient sugarClient2;
        public Certificate(ISqlSugarClient sugarClient)
        {
            sugarClient1=sugarClient;

        }
        public void AddEntity(CertificateModel model)
        {
            sugarClient1.Insertable(model).ExecuteCommand();
        }
        /// <summary>
        /// 获取所有证书信息
        /// </summary>
        /// <returns></returns>
        public List<CertificateModel> GetAll()
        {
            return sugarClient1.Queryable<CertificateModel>().ToList();
        }
        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <returns></returns>
        public List<UnitModel> GetUnits()
        {
           return sugarClient1.AsTenant().GetConnection("2").Queryable<UnitModel>().ToList();
        }
    }
}
