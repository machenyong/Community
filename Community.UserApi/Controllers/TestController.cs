using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community.Common;
using Community.IRepository;

namespace Community.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IDemoRepository _demoRepository;
        public TestController(IDemoRepository demoRepository)
        {
            _demoRepository = demoRepository;
        }
        DbFactory dbFactory = new DbFactory();
        
        //测试获取数据
        [Route("GetData")]
        [HttpGet]
        public IActionResult GetData()
        {
            DbContextHelper dbContext = new DbContextHelper();
            //var res = dbContext.Studentinfo.ToList();
            var data = dbFactory.DbHelper().Query<studentinfo>("select * from studentinfo").ToList();
            var res = _demoRepository.Query<Test>("select * from Test").ToList();

            //var data = dbFactory.DbHelper().Query<studentinfo>("select * from studentinfo").ToList();
            //var res = _demoRepository.Query<studentinfo>("select * from studentinfo").ToList();

            //var res = dbFactory.DbHelper().Query<Test>("select * from test").ToList();
            //var res = _demoRepository.Query<Test>("select * from Test").ToList();

            return Ok(res);
        }
        [Route("PostData")]
        [HttpPost]
        public void PostData()
        {
            //string[] sql = new string[2];
            //object[] param = new object[2];
            //sql[0] = "insert into Test values(@name)";
            //sql[1] = "insert into Test2 values(@name2)";
            //param[0] = new { @name="囜好"};
            //param[1] = new { @name2="囜好噢"};
            //int res = dbFactory.GetCRUD().ExecuteTransaction(sql,param);

            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("insert into Test values(@name)",new { @name="我是字典方法"});
            pairs.Add("insert into Test2 values(@name2)",new { @name2="我是字典方法2"});
            int res = dbFactory.DbHelper().ExecuteTransaction(pairs);
        }
        
    }
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Test2
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class studentinfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int CoeId { get; set; }
    }

}
