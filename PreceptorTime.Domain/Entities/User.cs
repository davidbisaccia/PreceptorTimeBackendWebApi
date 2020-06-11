using System;
using System.Collections.Generic;

namespace PreceptorTime.Domain.Entities
{
    public enum AccountType
    {
        Unknown = 0,
        Student,
        Resident,
        Preceptor,
        Admin,
    }

    public class User
    {
        //id, log-in name, email, display name, account type, password, token, title, suspended
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public AccountType Account { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
        public bool Active { get; set; }

        public List<TimeEntry> TimeEntries { get; set; }
    }
}
