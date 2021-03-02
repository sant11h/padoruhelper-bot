using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PadoruHelperBotDAL.Models
{
    public class UserSubscriptions
    {
        public ulong UserId { get; set; }
        public ulong GuildId { get; set; }
        public bool Works { get; set; }
        public bool Adventure { get; set; }
        public bool Training { get; set; }
        public User User { get; set; }
    }
}
