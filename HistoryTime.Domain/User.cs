using System;
using System.Collections.Generic;

namespace HistoryTime.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string  Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
        
        public int RoleId { get; set; }
        
        public IEnumerable<UserAnswer> UserAnswers { get; set; }
        
    }
}