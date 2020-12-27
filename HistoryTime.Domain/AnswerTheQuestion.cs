namespace HistoryTime.Domain
{
    public class AnswerTheQuestion
    {
        public int QuestionId { get; set; }

        public int AnswerId { get; set; }
        
        public bool IsCorrect { get; set; }

        public Question Question { get; set; }

        public Answer Answer { get; set; }
    }
}