using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}