using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PadoruHelperBotDAL.Models
{
    public class User
    {
        public ulong UserId { get; set; }
        public ulong? TeamId { get; set; }

        //Fluent API
        public List<UserSubscriptions> Subscriptions { get; set; }
        public Team Team { get; set; }
    }
}
