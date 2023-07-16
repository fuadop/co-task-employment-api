//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using employment_api.Dto;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace employment_api.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : Controller
    {

        // GET: /<controller>/
        [HttpGet]
        [Route("")]
        public ResponseBase<string?> Index() 
		{
            return new ResponseBase<string?>(200, "OK", null);
		}
    }
}

