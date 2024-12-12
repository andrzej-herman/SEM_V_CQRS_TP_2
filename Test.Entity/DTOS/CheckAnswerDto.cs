using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Entity.DTOS
{
    public class CheckAnswerDto
    {
        public bool IsCorrect { get; set; }
        public int NextCategory { get; set; }
    }
}
