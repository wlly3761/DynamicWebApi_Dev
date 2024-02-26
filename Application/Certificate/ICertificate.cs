using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Certificate
{
    internal interface ICertificate
    {
        List<CertificateModel> GetAll();
        void AddEntity(CertificateModel model);

        List<UnitModel> GetUnits();
    }
}
