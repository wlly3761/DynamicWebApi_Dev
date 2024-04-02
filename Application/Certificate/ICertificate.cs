using Repository.BaseModel;
using Repository.Entity;
using Repository.QueryModel;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Certificate
{
    internal interface ICertificate
    {
        Task<List<CertificateModel>> GetAll();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="model"></param>
        Task<ReturnModel<CertificateModel>> GetPageList(CertificateQueryModel queryModel);
        void AddEntity(CertificateModel model);

        List<UnitModel> GetUnits();
    }
}
