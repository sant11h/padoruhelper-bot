﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PadoruHelperBotDAL.Models
{
    public class AlertPetition
    {
        public int Id { get; set; }
        public ulong UserId { get; set; }
        public ulong GuildId { get; set; }
        public ulong ChannelId { get; set; }
        public AlertType AlertType { get; set; }
        public DateTime SendedAt { get; set; }
    }
}
