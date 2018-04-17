using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RandomThoughts.Controllers
{
    public class ThoughtHolesController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public ThoughtHolesController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }
}