using System;
using System.Collections.Generic;
using System.Text;

namespace PadoruHelperBotDAL.Models
{
    public class Team
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public ulong RoleId { get; set; }

        //FluentAPI
        public List<User> Users { get; set; }
    }
}
