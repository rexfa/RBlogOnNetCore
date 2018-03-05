using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RBlogOnNetCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Api")]
    public class ApiController : Controller
    {
        [HttpGet]
        public string test()
        {
            return "成功了";
        }
    }
}