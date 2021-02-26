using System;
using System.Collections.Generic;
using System.Text;

namespace PadoruHelperBotDAL.Models
{
    public class UserSubscriptions : Entity
    {
        public ulong UserId { get; set; }
        public ulong GuildId { get; set; }
        public bool Works { get; set; }
        public bool Adventure { get; set; }
        public bool Training { get; set; }
    }
}
