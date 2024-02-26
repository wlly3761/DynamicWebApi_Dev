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
        private readonly SqlSugarProvider sugarScope1;
        private readonly SqlSugarProvider sugarScope2;

        public Certificate()
        {
            sugarScope1=DbScoped.SugarScope.GetConnection("1");
            sugarScope2=DbScoped.SugarScope.GetConnection("2");

        }
        public void AddEntity(CertificateModel model)
        {
            sugarScope1.Insertable(model).ExecuteCommand();
        }
        public List<CertificateModel> GetAll()
        {
            return sugarScope1.Queryable<CertificateModel>().ToList();
        }

        public List<UnitModel> GetUnits()
        {
           return sugarScope2.Queryable<UnitModel>().ToList();
        }
    }
}
