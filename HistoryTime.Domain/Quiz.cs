using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Theme { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}