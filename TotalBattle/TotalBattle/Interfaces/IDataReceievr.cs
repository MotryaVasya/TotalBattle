using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalBattle.Interfaces
{
    interface IDataReceievr<T>
    {
        T Data { set; }
    }
}
