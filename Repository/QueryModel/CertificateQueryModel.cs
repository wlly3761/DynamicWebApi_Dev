using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.QueryModel
{

    public class CertificateQueryModel : PageModel
    {
        public string CertificateNumber { get; set; }
        public string CertificateType { get; set; }
    }
}
