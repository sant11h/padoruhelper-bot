using Microsoft.EntityFrameworkCore;
using PadoruHelperBotDAL;
using PadoruHelperBotDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadoruHelperBot.Core.Services
{
    public interface IUserSubscriptionsService
    {
        Task<UserSubscriptions> GetOrCreate(ulong userId, ulong guildId);
        Task Update(UserSubscriptions userSubscription);
    }

    public class UserSubscriptionsService : IUserSubscriptionsService
    {
        private readonly PadoruHelperContext _context;
        private readonly IUserService _userService;

        public UserSubscriptionsService(PadoruHelperContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<UserSubscriptions> GetOrCreate(ulong userId, ulong guildId)
        {
            var user = await _userService.GetOrCreate(userId).ConfigureAwait(false);
            var userSubs = user.Subscriptions.FirstOrDefault();

            if (userSubs != null) { return userSubs; }

            user.Subscriptions.Add(new UserSubscriptions
            {
                UserId = userId,
                GuildId = guildId,
                Works = false,
                Training = false,
                Adventure = false,
            });

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return userSubs;
        }

        public async Task Update(UserSubscriptions userSubscription)
        {
            var userSubs = await _context.UserSubscriptions
                .FirstOrDefaultAsync(x => x.UserId == userSubscription.UserId && x.GuildId == userSubscription.GuildId)
                .ConfigureAwait(false);

            userSubs.Works = userSubscription.Works;
            userSubs.Training = userSubscription.Training;
            userSubs.Adventure = userSubscription.Adventure;

            _context.UserSubscriptions.Attach(userSubs);
            _context.Entry(userSubs).State = EntityState.Modified;

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
