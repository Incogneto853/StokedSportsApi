using StokedSportsSessionService.Data.Models;
using StokedSportsSessionService.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokedSportsSessionService
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
         public SessionService(ISessionRepository sessionRepository)

        {
            _sessionRepository = sessionRepository;
        }
        public async Task<Session> CreateSession(Session session)
        {
            await _sessionRepository.CreateSession(session);
            return session;
        }

        public async Task<Session> GetSession(Guid sessionId)
        {
           var returnValue = await _sessionRepository.GetSession(sessionId);
            return returnValue;
            
        }
    }
}
