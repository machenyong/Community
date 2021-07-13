//using Community.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community.Common;
using Newtonsoft.Json;


namespace Community.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        private readonly JWTTokenOptions _tokenOptions;
        public JWTController(JWTTokenOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }
        [HttpPost]
        public string Login(string name,string pwd)
        {

            JwtTokenHelper token = new JwtTokenHelper();
            var tokenResult=token.AuthorizeToken(name, _tokenOptions);
            return JsonConvert.SerializeObject(new { tokenResult});
        }

        
    }
}
