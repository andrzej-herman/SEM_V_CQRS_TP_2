using Microsoft.EntityFrameworkCore;
using Test.Api.Database;
using Test.Entity.DTOS;
using Test.Entity.Entities;

namespace Test.Api.Services
{
    public class QuizService_EF : IQuizService
    {
        private readonly Random _random;
        private readonly QuizContext _context;

        public QuizService_EF(QuizContext context)
        {
            _context = context;
            _random = new Random();     
        }

        public async Task<bool> CheckAnswer(Guid answerId)
        {
            var answer = await _context.Answers.FirstOrDefaultAsync(a => a.Id == answerId);
            if (answer != null)
                return answer.IsCorrect;
            else
                return false;
        }

        public async Task<QuestionDto> GetQuestionFromCategory(int category)
        {
            var questionsFromCategory = await _context.Questions
                .Include(q => q.Answers)
                .Where(q => q.Category == category)
                .ToListAsync();


            var number = _random.Next(0, questionsFromCategory.Count);
            var selectedQuestion = questionsFromCategory[number];
            return Mapper.ToDto(selectedQuestion, _random);
        }



        

        
    }
}
