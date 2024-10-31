using System.Data.SqlClient;
using Test.Entity.DTOS;
using Test.Entity.Entities;

namespace Test.Api.Services
{
    public class QuizService_SqlConnection : IQuizService
    {
        private readonly SqlConnection _connection;
        private readonly Random _random;

        public QuizService_SqlConnection()
        {
            var connStr = "Server=.\\HERMANLOCAL;Database=CqrsTp2;Integrated Security=True";
            _connection = new SqlConnection(connStr);
            _random = new Random();
        }

        public async Task<bool> CheckAnswer(int answerId)
        {
            bool correct = false;
            await _connection.OpenAsync();
            var query = $"SELECT IsCorrect FROM Answers WHERE AnswerId = {answerId}";
            var cmd = new SqlCommand(query, _connection);
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read()) 
            {
                correct = reader.GetBoolean(0);
            }

            await reader.CloseAsync();
            await _connection.CloseAsync();
            return correct;
        }

        public async Task<QuestionDto> GetQuestionFromCategory(int category)
        {
            var questions = new List<Question>();
            await _connection.OpenAsync();
            var query = $"SELECT * FROM Questions WHERE QuestionCategory = {category}";
            var cmd = new SqlCommand(query, _connection);
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                var questionId = reader.GetInt32(0);
                var questionCategory = reader.GetInt32(1);
                var questionContent = reader.GetString(2);
                var question = new Question
                {
                    Id = questionId,
                    Category = questionCategory,
                    Content = questionContent,
                    Answers = new List<Answer>()
                };
                questions.Add(question);
            }
            await reader.CloseAsync();


            foreach (var question in questions)
            {
                query = $"SELECT * FROM Answers WHERE QuestionId = {question.Id}";
                cmd = new SqlCommand(query, _connection);
                var answerReader = await cmd.ExecuteReaderAsync();
                while (answerReader.Read())
                {
                    var answerId = answerReader.GetInt32(0);
                    var answerContent = answerReader.GetString(1);
                    var isCorrect = answerReader.GetBoolean(2);
                    var answer = new Answer
                    {
                        Id = answerId,
                        Content = answerContent,
                        IsCorrect = isCorrect
                    };
                    question.Answers!.Add(answer);
                }

                await answerReader.CloseAsync();
            }

            await _connection.CloseAsync();
            var index = _random.Next(0, questions.Count);
            var selectedQuestion = questions[index];

            return Mapper.ToDto(selectedQuestion, _random);
        }
    }
}
