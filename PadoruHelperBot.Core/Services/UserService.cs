using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PadoruHelperBotDAL;
using PadoruHelperBotDAL.Models;


namespace PadoruHelperBot.Core.Services
{
    public interface IUserService:IRepository<User>
    {
        Task UpdateTeam(ulong userId, ulong teamId);
        Task<Team> GetTeam(ulong userId);
        Task<User> GetOrCreate(ulong userId);
    }

    public class UserService : Repository<User, PadoruHelperContext>, IUserService
    {
        public UserService(PadoruHelperContext context) : base(context) { }

        public async Task<User> GetOrCreate(ulong userId)
        {
            var user = await _dbContext.Users
                .Include(user => user.Subscriptions)
                .FirstOrDefaultAsync(x => x.UserId == userId)
                .ConfigureAwait(false);

            if (user != null)
            {
                return user;
            }

            user = new User
            {
                UserId = userId,
                Subscriptions = new List<UserSubscriptions> { }
            };

            await _dbContext.Users.AddAsync(user).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return user;
        }

        public async Task UpdateTeam(ulong userId, ulong teamId)
        {
            var user = await  _dbContext.Users
                .FirstOrDefaultAsync(x => x.UserId == userId)
                .ConfigureAwait(false);

            user.TeamId = teamId;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Team> GetTeam(ulong userId)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.UserId == userId)
                .ConfigureAwait(false);

            var team = await _dbContext.Teams
                .FirstOrDefaultAsync(x => x.Id == user.TeamId)
                .ConfigureAwait(false);

            return team;
        }
    }
}
