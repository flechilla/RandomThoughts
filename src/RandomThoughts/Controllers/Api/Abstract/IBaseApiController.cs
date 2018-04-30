using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomThoughts.Controllers.Api
{
    public interface IBaseApiController
    {
        /// <summary>
        ///     Gets or sets the Id of the current user.
        /// </summary>
         string CurrentUserId { get; set; }
         string CurrentUserNickName { get; set; }
    }
}
