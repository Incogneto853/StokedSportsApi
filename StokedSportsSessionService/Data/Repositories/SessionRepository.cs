using Microsoft.EntityFrameworkCore;
using StokedSportsSessionService.Data.DBContext;
using StokedSportsSessionService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokedSportsSessionService.Data.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly StokedSessionContext _stokedSessionContext;
        public SessionRepository(StokedSessionContext stokedSessionContext)
        {
            _stokedSessionContext = stokedSessionContext;
        }
        public async Task<Session> CreateSession(Session session)
        {
            await _stokedSessionContext.Sessions.AddAsync(session);
            await _stokedSessionContext.SaveChangesAsync();
            return session;
        }

        public async Task<Session> GetSession(Guid sessionId)
        {
            var returnValue = await _stokedSessionContext.Sessions.SingleOrDefaultAsync(s => s.SessionId == sessionId);
            return returnValue;
        }
    }
}
