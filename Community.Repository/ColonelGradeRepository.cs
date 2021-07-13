using Community.Common;
using Community.IRepository;
using Community.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Repository
{
    public class ColonelGradeRepository: IColonelGradeRepository
    {
        DbFactory dbFactory = new DbFactory();
        //团长等级显示方法
        public List<ColonelGrade> GetColonelGrades(int page, int limit, out int total) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GradeID,GradeName,GradeSuffer,Rewardratio,CreateTime,[Status] from ColonelGrade");
            var res = dbFactory.DbHelper().Query<ColonelGrade>(strSql.ToString()).ToList();

            total = res.Count;
            return res.Skip((page - 1) * limit).Take(limit).ToList();
        }

        //团长等级状态更改
        public int UpdateColonelGradeStatusById(int GradeId,int Status)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update ColonelGrade set Status =@status where GradeID=@Id");
            var res = dbFactory.DbHelper().Execute(strSql.ToString(), new { @Id = GradeId , @status =Status});
            return res;
        }

        #region //团长等级删除
        //团长等级删除
        public int DeleteColonelGradeById(int GradeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ColonelGrade where GradeID=@Id");
            return dbFactory.DbHelper().Execute(strSql.ToString(), new { @Id = GradeId });
        }
        #endregion //
        #region //团长等级修改
        //团长等级删除
        public int EditColonelGradeById(ColonelGrade colonelGrade)
        {
            colonelGrade.CreateTime = DateTime.Now; //修改时间为当前的时间
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ColonelGrade set GradeName=@GradeName,GradeSuffer=@GradeSuffer,Rewardratio=@Rewardratio,CreateTime=@CreateTime,Status=@Status where GradeID=@Id");
            return dbFactory.DbHelper().Execute(strSql.ToString(), new { @GradeName=colonelGrade.GradeName, @GradeSuffer=colonelGrade.GradeSuffer, @Rewardratio=colonelGrade.Rewardratio, @CreateTime=colonelGrade.CreateTime, @Status=colonelGrade.Status, @Id = colonelGrade.GradeID });
        }
        #endregion //
    }
}
