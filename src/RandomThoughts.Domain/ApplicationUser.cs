using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace RandomThoughts.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string NickName { get; set; }
    }
}
