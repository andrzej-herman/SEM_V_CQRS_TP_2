using Newtonsoft.Json;
using Test.Entity.DTOS;

namespace Test.Front.Services
{
    public class QuizData : IQuizData
    {
        private readonly HttpClient _client;

        public QuizData(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> CheckAnswer(CheckAnswerRequest request)
        {
            var url = $"checkanswer?answerId={request.AnswerId}";
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(data);
            }
            else
                return false;
        }

        public async Task<QuestionDto?> GetQuestion(GetQuestionRequest request)
        {
            var url = $"getquestion?category={request.Category}";
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<QuestionDto>(data);
            }
            else
                return null;

        }
    }
}
