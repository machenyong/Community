using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.IRepository
{
    //NLogHelper接口
    public interface INLogHelper
    {
        void LogError(Exception ex);
    }
}
