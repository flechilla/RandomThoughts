using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace RandomThoughts.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string NickName { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="Thoughts"/>
        ///     related to the User
        /// </summary>
        public ICollection<Thought> Thoughts { get; set; }
    }
}
