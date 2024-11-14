using Test.Entity.DTOS;
using Test.Entity.Entities;

namespace Test.Apierw{
    public static class Mapper
    {
        public static QuestionDto ToDto(Question question, Random rnd)
        {
            var answers = new List<AnswerDto>();
            foreach (var answer in question.Answers!)
            {
                var dto = new AnswerDto
                {
                    AnswerId = answer.Id,
                    Content = answer.Content, 
                };
                answers.Add(dto);
            }

            answers = answers.OrderBy(x => rnd.Next()).ToList();
            var count = 1;
            foreach (var answerDto in answers)
            {
                answerDto.DisplayOrder = count;
                count++;
            }

            return new QuestionDto
            {
                Category = question.Category,
                Content = question.Content,
                Answers = answers,
            };
        }
    }
}
