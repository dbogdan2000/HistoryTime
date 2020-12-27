using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<UserAnswer> UsersAnswers { get; set; }

        public IEnumerable<AnswerTheQuestion> AnswerTheQuestions { get; set; }
    }
}