using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        
        public IEnumerable<AnswerTheQuestion> AnswerTheQuestions { get; set; }

        public IEnumerable<UserAnswer> UserAnswers { get; set; }
    }
}