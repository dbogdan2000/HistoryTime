using System.Threading.Tasks;
using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/answersTheQuestions")]
    public class AnswersTheQuestionsController : Controller
    {
        private readonly IAnswersTheQuestionsRepository _answersTheQuestionsRepository;
        private readonly IAnswersRepository _answersRepository;
        private readonly IQuestionsRepository _questionsRepository;

        public AnswersTheQuestionsController(IAnswersTheQuestionsRepository answersTheQuestionsRepository,
            IAnswersRepository answersRepository, IQuestionsRepository questionsRepository)
        {
            _answersTheQuestionsRepository = answersTheQuestionsRepository;
            _answersRepository = answersRepository;
            _questionsRepository = questionsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnswersTheQuestions()
        {
            var answersTheQuestions = await _answersTheQuestionsRepository.GetAll();
            return Ok(answersTheQuestions);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAnswerTheQuestion(int questionId, int answerId)
        {
            var question = await _answersTheQuestionsRepository.GetQuestion(questionId);
            var answer = await _answersTheQuestionsRepository.GetAnswer(answerId);
            if(question == null || answer == null)
                return NotFound();
            var answerTheQuestion = await _answersTheQuestionsRepository.Get(questionId, answerId);
            answerTheQuestion.Question = await _answersTheQuestionsRepository.GetQuestion(questionId);
            answerTheQuestion.Answer = await _answersTheQuestionsRepository.GetAnswer(answerId);
            return Ok(answerTheQuestion);
        }

        [Route("{questionId}")]
        [HttpGet]
        public async Task<IActionResult> GetAnswersTheQuestion(int questionId)
        {
            var question = await _answersTheQuestionsRepository.GetQuestion(questionId);
            if (question == null)
                return NotFound();
            var answersTheQuestion = await _questionsRepository.GetAnswersTheQuestion(questionId);
            foreach (var answerTheQuestion in answersTheQuestion)
            {
                answerTheQuestion.Question = await _answersTheQuestionsRepository.GetQuestion(answerTheQuestion.QuestionId);
                answerTheQuestion.Answer = await _answersTheQuestionsRepository.GetAnswer(answerTheQuestion.AnswerId);
            }

            return Ok(answersTheQuestion);
        }

        [Route("{answerId}")]
        [HttpGet]
        public async Task<IActionResult> GetQuestionsByAnswer(int answerId)
        {
            var answer = await _answersTheQuestionsRepository.GetAnswer(answerId);
            if (answer == null)
                return NotFound();
            var questionsByAnswer = await _answersRepository.GetAnswerTheQuestions(answerId);
            foreach (var questionByAnswer in questionsByAnswer)
            {
                questionByAnswer.Question = await _answersTheQuestionsRepository.GetQuestion(questionByAnswer.QuestionId);
                questionByAnswer.Answer = await _answersTheQuestionsRepository.GetAnswer(questionByAnswer.AnswerId);
            }

            return Ok(questionsByAnswer);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnswerTheQuestion(AnswerTheQuestion answerTheQuestion)
        {
            await _answersTheQuestionsRepository.Update(answerTheQuestion);
            return Ok(answerTheQuestion);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswerTheQuestion(AnswerTheQuestion answerTheQuestion)
        {
            var question = await _answersTheQuestionsRepository.GetQuestion(answerTheQuestion.QuestionId);
            var answer = await _answersTheQuestionsRepository.GetAnswer(answerTheQuestion.AnswerId);
            if (question == null || answer == null)
                return NotFound();
            await _answersTheQuestionsRepository.Create(new AnswerTheQuestion
            {
                QuestionId = answerTheQuestion.QuestionId,
                AnswerId = answerTheQuestion.AnswerId,
                IsCorrect = answerTheQuestion.IsCorrect
            });
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAnswerTheQuestion(int questionId, int answerId)
        {
            await _answersTheQuestionsRepository.Delete(questionId, answerId);
            return Ok();
        }
    }
}