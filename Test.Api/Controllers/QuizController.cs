using Microsoft.AspNetCore.Mvc;
using Test.Api.Services;
using Test.Entity.DTOS;

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
                return result != null ? Ok(result) : BadRequest("Nieprawid³owa kategoria pytania");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("checkanswer")]
        public async Task<IActionResult> CheckAnswer([FromQuery] Guid answerId, int category)
        {
            try
            {
                var result = await _quizService.CheckAnswer(answerId, category);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }  
        }
    }
}
