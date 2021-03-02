using Microsoft.EntityFrameworkCore;
using PadoruHelperBotDAL;
using PadoruHelperBotDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PadoruHelperBot.Core.Services
{
    public interface ITeamService:IRepository<Team>
    {
        Task<Team> GetByName(string name);
    }

    public class TeamService : Repository<Team, PadoruHelperContext>, ITeamService
    {
        public TeamService(PadoruHelperContext context) : base(context) { }

        public async Task<Team> GetByName(string name)
        {
            var team = await _dbContext.Teams
                .FirstOrDefaultAsync(x => x.Name == name)
                .ConfigureAwait(false);

            return team;
        }
    }
}
