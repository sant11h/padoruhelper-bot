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
    public interface IAlertsService : IRepository<AlertPetition>
    {
        Task<AlertPetition> Get(ulong userId, AlertType alertType);
        Task<List<AlertPetition>> GetByType(AlertType alertType);
        Task<List<AlertPetition>> GetByUser(ulong userId);
    }

    public class AlertsService : Repository<AlertPetition, PadoruHelperContext>, IAlertsService
    {
        public AlertsService(PadoruHelperContext context) : base(context) { }

        public async Task<List<AlertPetition>> GetByType(AlertType alertType)
        {
            return await _dbContext.AlertPetitions.Where(x => x.AlertType == alertType).ToListAsync();
        }

        public async Task<List<AlertPetition>> GetByUser(ulong userId)
        {
            return await _dbContext.AlertPetitions.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<AlertPetition> Get(ulong userId, AlertType alertType)
        {
            return await _dbContext.AlertPetitions.Where(x => x.UserId == userId && x.AlertType == alertType).FirstOrDefaultAsync();
        }
    }
}
