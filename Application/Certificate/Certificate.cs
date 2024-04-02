using ApplicationCommon;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.BaseModel;
using Repository.Entity;
using Repository.QueryModel;
using SqlSugar;
using SqlSugar.IOC;
using System.Linq.Expressions;

namespace Application.Certificate
{
    [DynamicApiInterface]
    [ServiceRegistry(ServicelLifeCycle = "Scoped")]
    public class Certificate : ICertificate
    {
        private readonly ISqlSugarClient sugarClient1;
        private readonly BaseRepository<CertificateModel> repository;

        public Certificate(ISqlSugarClient sugarClient)
        {
            sugarClient1=sugarClient;
            repository=new BaseRepository<CertificateModel>(sugarClient);

        }
        public void AddEntity(CertificateModel model)
        {
            sugarClient1.Insertable(model).ExecuteCommand();
        }
        /// <summary>
        /// 获取所有证书信息
        /// </summary>
        /// <returns></returns>
        public  async Task<List<CertificateModel>> GetAll()
        {
            return await repository.QueryAll();

        }

        public async Task<ReturnModel<CertificateModel>> GetPageList(CertificateQueryModel queryModel)
        {
            return await repository.QueryPage(x=>string.IsNullOrWhiteSpace(queryModel.CertificateNumber) || x.CertificateName.Contains(queryModel.CertificateNumber));
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
