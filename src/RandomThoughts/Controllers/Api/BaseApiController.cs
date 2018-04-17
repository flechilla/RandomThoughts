using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RandomThoughts.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    public class BaseApiController : Controller
    {
        /// <summary>
        ///     Gets or sets the Id of the current user.
        /// </summary>
        public string CurrentUserId { get; set; }
         

        public BaseApiController(IHttpContextAccessor httpContextAccessor)
        {
            CurrentUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    
    }
}