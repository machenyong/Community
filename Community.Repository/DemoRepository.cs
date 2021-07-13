using Community.Common;
using Community.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Repository
{
    public class DemoRepository : IDemoRepository
    {
        DbFactory dbFactory = new DbFactory();
        public int Execute(string sql, object param = null)
        {
            return dbFactory.DbHelper().Execute(sql, param);
        }

        public int ExecuteTransaction(string[] sqlarr, object[] param)
        {
            return dbFactory.DbHelper().ExecuteTransaction(sqlarr,param);
        }

        public int ExecuteTransaction(Dictionary<string, object> dic)
        {
            return dbFactory.DbHelper().ExecuteTransaction(dic);
        }

        public List<T> Query<T>(string sql, object param = null) where T : class, new()
        {
            return dbFactory.DbHelper().Query<T>(sql, param);
        }
    }
}
