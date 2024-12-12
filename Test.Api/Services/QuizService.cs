using Microsoft.Data.SqlClient;
using Test.Entity.DTOS;

namespace Test.Api.Services
{
    public class QuizService : IQuizService
    {
        private readonly SqlConnection _connection;
        private readonly Random _random;

        public QuizService()
        {
            var connStr = "Server=.\\HERMANLOCAL;Database=CqrsTp2;Integrated Security=True;TrustServerCertificate=True";
            _connection = new SqlConnection(connStr);
            _random = new Random();
        }

        public async Task<CheckAnswerDto> CheckAnswer(Guid answerId, int category)
        {
            List<int> categories = [100, 200, 300, 400, 500, 750, 1000];
            bool correct = false;
            await _connection.OpenAsync();
            var query = $"SELECT AnswerIsCorrect FROM Answers WHERE AnswerId = @answerId";
            var cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@answerId", answerId);
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                correct = reader.GetBoolean(0);
            }

            var index = categories.IndexOf(category);
            var nextCategory = index != 6 ? categories[index + 1] : 0; 

            await reader.CloseAsync();
            await _connection.CloseAsync();
            return new CheckAnswerDto { IsCorrect = correct, NextCategory = nextCategory };
        }

        public async Task<QuestionDto?> GetQuestionFromCategory(int category)
        {
            var questions = new List<QuestionDto>();
            await _connection.OpenAsync();
            var query = $"SELECT * FROM Questions WHERE QuestionCategory = @category";
            var cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@category", category);
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                var questionId = reader.GetGuid(0);
                var questionCategory = reader.GetInt32(1);
                var questionContent = reader.GetString(2);
                var question = new QuestionDto
                {
                    QuestionId = questionId,
                    Category = questionCategory,
                    Content = questionContent,
                    Answers = []
                };
                questions.Add(question);
            }
            await reader.CloseAsync();

            if (questions.Count == 0)
                return null;

            var index = _random.Next(0, questions.Count);
            var selectedQuestion = questions[index];

            query = $"SELECT AnswerId, AnswerContent FROM Answers WHERE QuestionId = @questionId";
            cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@questionId", selectedQuestion.QuestionId);
            var answerReader = await cmd.ExecuteReaderAsync();
            while (answerReader.Read())
            {
                var answerId = answerReader.GetGuid(0);
                var answerContent = answerReader.GetString(1);
                var answer = new AnswerDto
                {
                    AnswerId = answerId,
                    Content = answerContent,
                };
                selectedQuestion.Answers!.Add(answer);
            }

            await _connection.CloseAsync();
            return selectedQuestion;
        }
    }
}
