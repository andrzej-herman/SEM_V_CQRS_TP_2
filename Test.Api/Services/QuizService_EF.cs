using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Test.Api.Database;
using Test.Apierw;
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


        public async Task<ResultDto> AddQuestion(AddQuestionDto dto)
        {
            try
            {
                var question = new Question
                {
                    Category = dto.Category,
                    Content = dto.Content,
                };

                await _context.Questions.AddAsync(question);
                await _context.SaveChangesAsync();

                var counter = 0;
                foreach (var answerDto in dto.Answers)
                {
                    var answer = new Answer
                    {
                        Content = answerDto.Trim(),
                        IsCorrect = counter == 0 ? true : false,
                        QuestionId = question.Id
                    };

                    await _context.Answers.AddAsync(answer);
                    counter++;
                }

                await _context.SaveChangesAsync();
                return new ResultDto 
                { 
                    Result = true, 
                    Description = $"Pytanie zostało pomyślnie dodane. Id: {question.Id}" 
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    Result = false,
                    Description = $"Wystąpił błąd podczas dodawania pytania. Błąd: {ex.Message}"
                };
            }
        }


    }



    
}
