using Community.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Common
{
    public class DbContextHelper:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (DbFactory.conDb== "SqlServer")
            {
                optionsBuilder.UseSqlServer(ConfigHelper.ConnecSqlServer);
            }
            else if (DbFactory.conDb == "MySql")
            {
                optionsBuilder.UseMySQL(ConfigHelper.ConnecMySql);
            }
            
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Colonel> Colonel { get; set; }
        public virtual DbSet<CommunityGroup>  CommunityGroup { get; set; }
        public virtual DbSet<WarehouseModel>   WarehouseModel { get; set; }
    }
}
