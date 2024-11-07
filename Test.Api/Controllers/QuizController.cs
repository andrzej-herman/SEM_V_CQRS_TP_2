using Microsoft.AspNetCore.Mvc;
using Test.Api.Services;
using Test.Entity.Entities;

namespace Test.Api.Controllers
{
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;



        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        [Route("getquestion")]
        public async Task<IActionResult> GetQuestion([FromQuery] int category)
        {
            try
            {
                var result = await _quizService.GetQuestionFromCategory(category);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("checkanswer")]
        public async Task<IActionResult> CheckAnswer([FromQuery] Guid answerId)
        {
            try
            {
                var result = await _quizService.CheckAnswer(answerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
