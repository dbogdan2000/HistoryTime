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
        public IActionResult GetAnswersTheQuestions()
        {
            var answersTheQuestions = _answersTheQuestionsRepository.Get();
            return Ok(answersTheQuestions);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetAnswerTheQuestion(int questionId, int answerId)
        {
            var question = _answersTheQuestionsRepository.GetQuestion(questionId);
            var answer = _answersTheQuestionsRepository.GetAnswer(answerId);
            if(question == null || answer == null)
                return NotFound();
            var answerTheQuestion = _answersTheQuestionsRepository.Get(questionId, answerId);
            answerTheQuestion.Question = _answersTheQuestionsRepository.GetQuestion(questionId);
            answerTheQuestion.Answer = _answersTheQuestionsRepository.GetAnswer(answerId);
            return Ok(answerTheQuestion);
        }

        [Route("{questionId}")]
        [HttpGet]
        public IActionResult GetAnswersTheQuestion(int questionId)
        {
            var question = _answersTheQuestionsRepository.GetQuestion(questionId);
            if (question == null)
                return NotFound();
            var answersTheQuestion = _questionsRepository.GetAnswersTheQuestion(questionId);
            foreach (var answerTheQuestion in answersTheQuestion)
            {
                answerTheQuestion.Question = _answersTheQuestionsRepository.GetQuestion(answerTheQuestion.QuestionId);
                answerTheQuestion.Answer = _answersTheQuestionsRepository.GetAnswer(answerTheQuestion.AnswerId);
            }

            return Ok(answersTheQuestion);
        }

        [Route("{answerId}")]
        [HttpGet]
        public IActionResult GetQuestionsByAnswer(int answerId)
        {
            var answer = _answersTheQuestionsRepository.GetAnswer(answerId);
            if (answer == null)
                return NotFound();
            var questionsByAnswer = _answersRepository.GetAnswerTheQuestions(answerId);
            foreach (var questionByAnswer in questionsByAnswer)
            {
                questionByAnswer.Question = _answersTheQuestionsRepository.GetQuestion(questionByAnswer.QuestionId);
                questionByAnswer.Answer = _answersTheQuestionsRepository.GetAnswer(questionByAnswer.AnswerId);
            }

            return Ok(questionsByAnswer);
        }

        [HttpPost]
        public IActionResult AddAnswerTheQuestion(AnswerTheQuestion answerTheQuestion)
        {
            var question = _answersTheQuestionsRepository.GetQuestion(answerTheQuestion.QuestionId);
            var answer = _answersTheQuestionsRepository.GetAnswer(answerTheQuestion.AnswerId);
            if (question == null || answer == null)
                return NotFound();
            _answersTheQuestionsRepository.Create(new AnswerTheQuestion
            {
                QuestionId = answerTheQuestion.QuestionId,
                AnswerId = answerTheQuestion.AnswerId,
                IsCorrect = answerTheQuestion.IsCorrect
            });
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAnswerTheQuestion(int questionId, int answerId)
        {
            _answersTheQuestionsRepository.Delete(questionId, answerId);
            return Ok();
        }
    }
}