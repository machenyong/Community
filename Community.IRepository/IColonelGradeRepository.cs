using Community.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IRepository
{
    public interface IColonelGradeRepository
    {
        #region//团长等级显示
        /// <summary>
        /// 团长等级显示
        /// </summary>
        /// <returns></returns>
        List<ColonelGrade> GetColonelGrades(int page, int limit, out int total);
        #endregion
        #region/// 修改团长状态
        /// <summary>
        /// 修改团长状态
        /// </summary>
        /// <param name="GradeId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        int UpdateColonelGradeStatusById(int GradeId, int Status);
        #endregion
        #region/// 删除团长状态
        /// <summary>
        /// 删除团长等级
        /// </summary>
        /// <param name="GradeId"></param>
        /// <returns></returns>
        int DeleteColonelGradeById(int GradeId);
        #endregion

        #region/// 修改团长等级信息
        /// <summary>
        /// 修改团长等级信息
        /// </summary>
        /// <param name="GradeId"></param>
        /// <returns></returns>
        int EditColonelGradeById(ColonelGrade colonelGrade);
        #endregion
    }
}
