using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Options
{
    internal interface Ioptions
    {
        object GetConfigure();

        string GetConfigValue(string key);
    }
}
