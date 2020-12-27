using System.Collections.Generic;
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
        public IActionResult GetUsersAnswers()
        {
            var usersAnswers = _usersAnswersRepository.Get();
            return Ok(usersAnswers);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetAnswerOfUser(int userId, int answerId, int questionId)
        {
            var user = _usersAnswersRepository.GetUser(userId);
            var answer = _usersAnswersRepository.GetAnswer(answerId);
            var question = _usersAnswersRepository.GetQuestion(questionId);
            if (user == null || answer == null || question == null)
                return NotFound();
            var userAnswer = _usersAnswersRepository.Get(userId, answerId,questionId);
            userAnswer.User = _usersAnswersRepository.GetUser(userId);
            userAnswer.Question = _usersAnswersRepository.GetQuestion(questionId);
            userAnswer.Answer = _usersAnswersRepository.GetAnswer(answerId);
            return Ok(userAnswer);
        }

        [Route("{userId}")]
        [HttpGet]
        public IActionResult GetUserAnswers(int userId)
        {
            var user = _usersAnswersRepository.GetUser(userId);
            if (user == null)
                return NotFound();
            var userAnswers = _usersRepository.GetUserAnswers(userId);
            foreach (var userAnswer in userAnswers)
            {
                userAnswer.User = _usersAnswersRepository.GetUser(userAnswer.UserId);
                userAnswer.Question = _usersAnswersRepository.GetQuestion(userAnswer.QuestionId);
                userAnswer.Answer = _usersAnswersRepository.GetAnswer(userAnswer.AnswerId);
            }
            return Ok(userAnswers);
        }

        [Route("{questionId}")]
        [HttpGet]
        public IActionResult GetUsersAnswersTheQuestion(int questionId)
        {
            var question = _usersAnswersRepository.GetQuestion(questionId);
            if (question == null)
                return NotFound();
            var usersAnswersTheQuestion = _questionsRepository.GetUsersAnswers(questionId);
            foreach (var userAnswerTheQuestion in usersAnswersTheQuestion)
            {
                userAnswerTheQuestion.User = _usersAnswersRepository.GetUser(userAnswerTheQuestion.UserId);
                userAnswerTheQuestion.Question = _usersAnswersRepository.GetQuestion(userAnswerTheQuestion.QuestionId);
                userAnswerTheQuestion.Answer = _usersAnswersRepository.GetAnswer(userAnswerTheQuestion.AnswerId);
            }

            return Ok(usersAnswersTheQuestion);
        }

        [Route("{answerId}")]
        [HttpGet]
        public IActionResult GetSelectedAnswerOnQuestions(int answerId)
        {
            var answer = _usersAnswersRepository.GetAnswer(answerId);
            if (answer == null)
                return NotFound();
            var selectedAnswerOnQuestions = _answersRepository.GetUsersAnswers(answerId);
            foreach (var selectedAnswerOnQuestion in selectedAnswerOnQuestions)
            {
                selectedAnswerOnQuestion.User = _usersAnswersRepository.GetUser(selectedAnswerOnQuestion.UserId);
                selectedAnswerOnQuestion.Question = _usersAnswersRepository.GetQuestion(selectedAnswerOnQuestion.QuestionId);
                selectedAnswerOnQuestion.Answer = _usersAnswersRepository.GetAnswer(selectedAnswerOnQuestion.AnswerId);
            }

            return Ok(selectedAnswerOnQuestions);
        }

        [HttpPost]
        public IActionResult AddUserAnswer(UserAnswer userAnswer)
        {
            var user = _usersAnswersRepository.GetUser(userAnswer.UserId);
            var question = _usersAnswersRepository.GetQuestion(userAnswer.QuestionId);
            var answer = _usersAnswersRepository.GetAnswer(userAnswer.AnswerId);
            if (user == null || answer == null || question == null)
                return NotFound();
            _usersAnswersRepository.Create(new UserAnswer
            {
                UserId = userAnswer.UserId,
                AnswerId = userAnswer.AnswerId,
                QuestionId = userAnswer.QuestionId
            });
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteUserAnswer(int answerId, int userId, int questionId)
        {
            _usersAnswersRepository.Delete(answerId, userId, questionId);
            return Ok();
        }

    }
}