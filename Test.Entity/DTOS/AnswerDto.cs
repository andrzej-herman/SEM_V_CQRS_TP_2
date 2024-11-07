using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Entity.DTOS
{
    public class AnswerDto
    {
        public Guid AnswerId { get; set; }
        public string? Content { get; set; }
        public int DisplayOrder { get; set; }
    }
}
