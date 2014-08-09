using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Logger
{

    public interface ILogManager
    {
        ILogger GetLogger(Type type);
    }
}
