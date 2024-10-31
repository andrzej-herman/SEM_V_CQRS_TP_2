using Test.Entity.DTOS;
using Test.Entity.Entities;

namespace Test.Api.Services
{
    public interface IQuizService
    {
        Task<QuestionDto> GetQuestionFromCategory(int category);
        Task<bool> CheckAnswer(int answerId);
    }
}
