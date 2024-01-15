using LocalChatServerWeb.Models;
using LocalChatServerWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocalChatServerWeb.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        private readonly SessionRepository sessionRepository;
        public SessionController(SessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }
        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var session = new Session
            {
                Name = name,
                State = true
            };

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is not null)
            {
                session.Creator = new Guid(userId);
                session.ActiveUsers = new List<Guid> { new(userId) };
            }

            await sessionRepository.CreateAsync(session);

            var sessionId = session.Id;

            return RedirectToAction();
        }

    }
}
