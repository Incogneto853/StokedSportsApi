using StokedSportsSessionService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokedSportsSessionService.Data.Repositories
{
    public interface ISessionRepository
    {
        Task<Session> GetSession(Guid sessionId);
        Task<Session> CreateSession(Session session);


    }
}
