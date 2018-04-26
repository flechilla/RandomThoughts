using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RandomThoughts.Domain
{
    [Table("ThoughtComments")]
    public class ThoughtComments : Comment<Thought,int>
    {
    }
}
