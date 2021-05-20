using System.Collections.Generic;
using System.Threading.Tasks;
using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/usersAnswers")]
    public class UsersAnswersController : Controller
    {
        private readonly IUsersAnswersRepository _usersAnswersRepository;
        private readonly IAnswersRepository _answersRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IQuestionsRepository _questionsRepository;

        public UsersAnswersController(IUsersAnswersRepository usersAnswersRepository, IAnswersRepository answersRepository, IUsersRepository usersRepository, IQuestionsRepository questionsRepository)
        {
            _usersAnswersRepository = usersAnswersRepository;
            _answersRepository = answersRepository;
            _usersRepository = usersRepository;
            _questionsRepository = questionsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAnswers()
        {
            var usersAnswers = await _usersAnswersRepository.GetAll();
            return Ok(usersAnswers);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAnswerOfUser(int userId, int answerId, int questionId)
        {
            var user = await _usersAnswersRepository.GetUser(userId);
            var answer = await _usersAnswersRepository.GetAnswer(answerId);
            var question = await _usersAnswersRepository.GetQuestion(questionId);
            if (user == null || answer == null || question == null)
                return NotFound();
            var userAnswer = await _usersAnswersRepository.Get(userId, answerId,questionId);
            userAnswer.User = await _usersAnswersRepository.GetUser(userId);
            userAnswer.Question = await _usersAnswersRepository.GetQuestion(questionId);
            userAnswer.Answer = await _usersAnswersRepository.GetAnswer(answerId);
            return Ok(userAnswer);
        }

        [Route("{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetUserAnswers(int userId)
        {
            var user = await _usersAnswersRepository.GetUser(userId);
            if (user == null)
                return NotFound();
            var userAnswers = await _usersRepository.GetUserAnswers(userId);
            foreach (var userAnswer in userAnswers)
            {
                userAnswer.User = await _usersAnswersRepository.GetUser(userAnswer.UserId);
                userAnswer.Question = await _usersAnswersRepository.GetQuestion(userAnswer.QuestionId);
                userAnswer.Answer = await _usersAnswersRepository.GetAnswer(userAnswer.AnswerId);
            }
            return Ok(userAnswers);
        }

        [Route("{questionId}")]
        [HttpGet]
        public async Task<IActionResult> GetUsersAnswersTheQuestion(int questionId)
        {
            var question = await _usersAnswersRepository.GetQuestion(questionId);
            if (question == null)
                return NotFound();
            var usersAnswersTheQuestion = await _questionsRepository.GetUsersAnswers(questionId);
            foreach (var userAnswerTheQuestion in usersAnswersTheQuestion)
            {
                userAnswerTheQuestion.User = await _usersAnswersRepository.GetUser(userAnswerTheQuestion.UserId);
                userAnswerTheQuestion.Question = await _usersAnswersRepository.GetQuestion(userAnswerTheQuestion.QuestionId);
                userAnswerTheQuestion.Answer = await _usersAnswersRepository.GetAnswer(userAnswerTheQuestion.AnswerId);
            }

            return Ok(usersAnswersTheQuestion);
        }

        [Route("{answerId}")]
        [HttpGet]
        public async Task<ActionResult> GetSelectedAnswerOnQuestions(int answerId)
        {
            var answer = await _usersAnswersRepository.GetAnswer(answerId);
            if (answer == null)
                return NotFound();
            var selectedAnswerOnQuestions = await _answersRepository.GetUsersAnswers(answerId);
            foreach (var selectedAnswerOnQuestion in selectedAnswerOnQuestions)
            {
                selectedAnswerOnQuestion.User = await _usersAnswersRepository.GetUser(selectedAnswerOnQuestion.UserId);
                selectedAnswerOnQuestion.Question = await _usersAnswersRepository.GetQuestion(selectedAnswerOnQuestion.QuestionId);
                selectedAnswerOnQuestion.Answer = await _usersAnswersRepository.GetAnswer(selectedAnswerOnQuestion.AnswerId);
            }

            return Ok(selectedAnswerOnQuestions);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAnswer(UserAnswer userAnswer)
        {
            var user = await _usersAnswersRepository.GetUser(userAnswer.UserId);
            var question = await _usersAnswersRepository.GetQuestion(userAnswer.QuestionId);
            var answer = await _usersAnswersRepository.GetAnswer(userAnswer.AnswerId);
            if (user == null || answer == null || question == null)
                return NotFound();
            await _usersAnswersRepository.Create(new UserAnswer
            {
                UserId = userAnswer.UserId,
                AnswerId = userAnswer.AnswerId,
                QuestionId = userAnswer.QuestionId
            });
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAnswer(int answerId, int userId, int questionId)
        {
            await _usersAnswersRepository.Delete(answerId, userId, questionId);
            return Ok();
        }

    }
}