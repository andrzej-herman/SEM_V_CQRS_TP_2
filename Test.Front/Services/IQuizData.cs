using Test.Entity.DTOS;

namespace Test.Front.Services
{
    public interface IQuizData
    {
        Task<QuestionDto?> GetQuestion(GetQuestionRequest request);
        Task<bool> CheckAnswer(CheckAnswerRequest request);
    }
}
