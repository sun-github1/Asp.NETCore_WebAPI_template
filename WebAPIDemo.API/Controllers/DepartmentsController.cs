using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDemo.API.Controllers
{
    public class DepartmentsController : ControllerBase
    {
        public ActionResult Index()
        {
            return Ok("hello");
        }
    }
}
