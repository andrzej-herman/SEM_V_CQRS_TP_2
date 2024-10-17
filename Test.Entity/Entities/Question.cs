using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Entity.Entities
{
	public class Question : BaseEntity
	{
        public int Category { get; set; }
        public string? Content { get; set; }
        public List<Answer>? Answers { get; set; }
    }
}
