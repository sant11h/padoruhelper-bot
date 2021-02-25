using Microsoft.EntityFrameworkCore;
using PadoruHelperBotDAL;
using PadoruHelperBotDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadoruHelperBot.Core.Services.Alerts
{
    public interface IAlertsService
    {
        Task Add(AlertPetition petition);
        Task Remove(AlertPetition petition);
        Task<List<AlertPetition>> GetAll();
        Task<AlertPetition> Get(ulong userId, AlertType alertType);
        Task<List<AlertPetition>> GetByType(AlertType alertType);
        Task<List<AlertPetition>> GetByUser(ulong userId);
    }

    public class AlertsService : IAlertsService
    {
        private readonly PadoruHelperContext _context;

        public AlertsService(PadoruHelperContext context)
        {
            _context = context;
        }

        public async Task Add(AlertPetition petition)
        {
            await _context.AlertPetition.AddAsync(petition).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task Remove(AlertPetition petition)
        {
            _context.AlertPetition.Remove(petition);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AlertPetition>> GetByType(AlertType alertType)
        {
            return await _context.AlertPetition.Where(x => x.AlertType == alertType).AsNoTracking().ToListAsync();
        }

        public async Task<List<AlertPetition>> GetByUser(ulong userId)
        {
            return await _context.AlertPetition.Where(x => x.UserId == userId).AsNoTracking().ToListAsync();
        }

        public async Task<AlertPetition> Get(ulong userId, AlertType alertType)
        {
            return await _context.AlertPetition.Where(x => x.UserId == userId && x.AlertType == alertType).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<AlertPetition>> GetAll()
        {
            return await _context.AlertPetition.ToListAsync();
        }
    }
}
