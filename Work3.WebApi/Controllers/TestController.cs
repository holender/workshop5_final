using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Work3.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

		/// <summary>
		/// Some Hello Controller
		/// </summary>
		/// <returns>returns string hello</returns>
		[HttpGet]
	    public ActionResult<string> Hello()
	    {
		    return "Hello world";
	    }
    }
}