using Microsoft.EntityFrameworkCore;
using PadoruHelperBotDAL;
using PadoruHelperBotDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadoruHelperBot.Core.Services.User
{
    public interface IUserSubscriptionsService
    {
        Task<UserSubscriptions> GetOrCreate(ulong userId, ulong guildId);
        Task Update(UserSubscriptions userSubscription);
    }

    public class UserSubscriptionsService : IUserSubscriptionsService
    {
        private readonly PadoruHelperContext _context;

        public UserSubscriptionsService(PadoruHelperContext context)
        {
            _context = context;
        }

        public async Task<UserSubscriptions> GetOrCreate(ulong userId, ulong guildId)
        {
            var userSubs = await _context.UserSubscriptions
                .Where(x => x.GuildId == guildId)
                .FirstOrDefaultAsync(x => x.UserId == userId).ConfigureAwait(false);

            if (userSubs != null) { return userSubs; }

            userSubs = new UserSubscriptions
            {
                UserId = userId,
                GuildId = guildId,
                Works = false,
                Training = false,
                Adventure = false
            };

            _context.UserSubscriptions.Add(userSubs);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return userSubs;
        }

        public async Task Update(UserSubscriptions userSubscription)
        {
            var userSubs = await _context.UserSubscriptions
                .FirstOrDefaultAsync(x => x.UserId == userSubscription.UserId && x.GuildId == userSubscription.GuildId);

            userSubs.Works = userSubscription.Works;
            userSubs.Training = userSubscription.Training;
            userSubs.Adventure = userSubscription.Adventure;

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
