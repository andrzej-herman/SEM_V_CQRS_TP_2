using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Entity.Entities;

namespace Test.Entity.DTOS
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public int Category { get; set; }
        public string? Content { get; set; }
        public List<AnswerDto>? Answers { get; set; }
    }
}
