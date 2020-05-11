namespace HistoryTime.Domain
{
    public class UserAnswer
    {
        public int UserId { get; set; }
        public int AnswerId { get; set; }

        public User User { get; set; }
        
        public Answer Answer { get; set; }
        
    }
}