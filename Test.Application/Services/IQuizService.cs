using Test.Entity.DTOS;

namespace Test.Application.Services
{
    public interface IQuizService
    {
        Task<QuestionDto?> GetQuestion(int category);
        Task<CheckAnswerDto> CheckAnswer(Guid answerId, int category);
    }
}
