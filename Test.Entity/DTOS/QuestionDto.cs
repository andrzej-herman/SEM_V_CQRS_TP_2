using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Entity.DTOS
{
    public class QuestionDto
    {
        public Guid QuestionId { get; set; }
        public int Category { get; set; }
        public string? Content { get; set; }
        public List<AnswerDto>? Answers { get; set; } = [];
    }
}
