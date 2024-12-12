using Test.Entity.DTOS;


namespace Test.Api.Services
{
    public interface IQuizService
    {
        Task<QuestionDto?> GetQuestionFromCategory(int category);
        Task<CheckAnswerDto> CheckAnswer(Guid answerId, int category);
    }
}
