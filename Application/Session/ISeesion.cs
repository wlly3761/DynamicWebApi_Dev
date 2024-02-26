using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Session
{
    internal interface ISeesion
    {
        string GetSessionByKey(string key,string val);
    }
}
