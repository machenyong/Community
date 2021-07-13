using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Common;
using Community.IRepository;
using Community.Model;

namespace Community.Repository
{
    public class CommunityGroupRepository: ICommunityGroupRepository
    {
        DbContextHelper _db = new DbContextHelper();
        #region///团购配置新增
        /// <summary>
        /// 团购配置新增
        /// </summary>
        /// <param name="communityGroup"></param>
        /// <returns></returns>
        public int AddCommunityGroup(CommunityGroup communityGroup)
        {
            _db.CommunityGroup.Add(communityGroup);
            return _db.SaveChanges();
        }
        #endregion
    }
}
