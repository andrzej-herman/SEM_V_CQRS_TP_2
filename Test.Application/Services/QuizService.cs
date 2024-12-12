using System.Text.Json;
using Test.Entity.DTOS;

namespace Test.Application.Services
{
    public class QuizService : IQuizService
    {
        private readonly HttpClient _client;
        private JsonSerializerOptions _serializerOptions;

        public QuizService(HttpClient client)
        {
            _client = client;
            _serializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }


        public async Task<CheckAnswerDto> CheckAnswer(Guid answerId, int category)
        {
            throw new NotImplementedException();
        }

        public async Task<QuestionDto?> GetQuestion(int category)
        {
            var url = $"getquestion?category={category}";
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<QuestionDto?>(content, _serializerOptions);   
            }

            return null;
        }
    }
}
