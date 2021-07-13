using Community.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Model;
using Community.IRepository;

namespace Community.Repository
{
    public class SalespersonRepository: ISalespersonRepository
    {
        DbFactory dbFactory = new DbFactory();

        #region ///绑定核销员
        public int AddSalesperson(Salesperson salesperson)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Salesperson  values(@Img,@MemberName,@MemberPhone,@ColonelId)");
            var res = dbFactory.DbHelper().Execute(strSql.ToString(), new { @Img=salesperson.Img, @MemberName=salesperson.MemberName, @MemberPhone=salesperson.MemberPhone, @ColonelId=salesperson.ColonelId });
            return res;
        }
        #endregion

        #region ///查看当前团长下的核销员
        public List<Salesperson> ShowSalespersonByColonelId(int colonelId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Salesperson where ColonelId =@ColonelId");
            var res = dbFactory.DbHelper().Query<Salesperson>(strSql.ToString(),new { @ColonelId =colonelId});
            return res;
        }
        #endregion

        #region ///删除当前团长下的核销员
        public int DeleteSalespersonBySalespersonID(int salespersonID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Salesperson where SalespersonID =@SalespersonID");
            var res = dbFactory.DbHelper().Execute(strSql.ToString(), new { SalespersonID = salespersonID });
            return res;
        }
        #endregion
    }
}
