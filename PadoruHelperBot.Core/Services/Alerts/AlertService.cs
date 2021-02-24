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
    public interface IAlertService
    {
        Task AddAlertPetition(AlertPetition petition);
        Task RemoveAlertPetition(AlertPetition petition);
        Task<List<AlertPetition>> GetAlertPetitionsByType(AlertType alertType);
        Task<List<AlertPetition>> GetAlertPetitionsByUser(ulong userId);
        Task<AlertPetition> GetAlertPetition(ulong userId, AlertType alertType);
    }

    public class AlertService : IAlertService
    {
        private readonly PadoruHelperContext _context;

        public AlertService(PadoruHelperContext context)
        {
            _context = context;
        }

        public async Task AddAlertPetition(AlertPetition petition)
        {
            _context.AlertPetition.Add(petition);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAlertPetition(AlertPetition petition)
        {
            _context.AlertPetition.Remove(petition);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AlertPetition>> GetAlertPetitionsByType(AlertType alertType)
        {
            return await _context.AlertPetition.Where(x => x.AlertType == alertType).AsNoTracking().ToListAsync();
        }

        public async Task<List<AlertPetition>> GetAlertPetitionsByUser(ulong userId)
        {
            return await _context.AlertPetition.Where(x => x.UserId == userId).AsNoTracking().ToListAsync();
        }

        public async Task<AlertPetition> GetAlertPetition(ulong userId, AlertType alertType)
        {
            return await _context.AlertPetition.Where(x => x.UserId == userId && x.AlertType == alertType).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
