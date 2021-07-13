using Community.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IRepository
{
    public interface ICommunityGroupRepository
    {
        #region///团购配置新增
        /// <summary>
        /// 团购配置新增
        /// </summary>
        /// <param name="communityGroup"></param>
        /// <returns></returns>
        int AddCommunityGroup(CommunityGroup communityGroup);
        #endregion
    }
}
