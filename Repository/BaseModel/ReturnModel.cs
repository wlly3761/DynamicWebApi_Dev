using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.BaseModel
{
    //封装通用返回数据模型，包含数据、状态码、总数
    public class ReturnModel<T>: PageModel
    {
        public List<T> Data { get; set; }
        public string Code { get; set; }
        public string Msg { get; set; }
        public string ErrorMsg { get; set; }
    }
}
