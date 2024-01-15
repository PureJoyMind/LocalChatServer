using LocalChatServerWeb.Models;
using LocalChatServerWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace LocalChatServerWeb.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        private readonly SessionRepository sessionRepository;
        private readonly MessageRepository messageRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public SessionController(SessionRepository sessionRepository, UserManager<ApplicationUser> userManager, MessageRepository messageRepository)
        {
            this.sessionRepository = sessionRepository;
            this.userManager = userManager;
            this.messageRepository = messageRepository;
        }

        public async Task<ViewResult> GetSessionsForCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sessions = await sessionRepository.GetByUserAsync(userId);
            return View("GetSessions", sessions.AsEnumerable());
        }


        public async Task<ViewResult> GetSessionsForUser(string userId)
        {
            var guid = new Guid();
            if (!Guid.TryParse(userId, out guid))
            {
                throw new ArgumentException("The given userId is invalid.");
            }
            var sessions = await sessionRepository.GetByUserAsync(userId);
            return View("GetSessions", sessions.AsEnumerable());
        }


        public async Task<ViewResult> GetSessions()
        {
            var sessions = await sessionRepository.GetAsync();
            return View(sessions.AsEnumerable());
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

            var sessionId = session.Id.ToString("D");

            return RedirectToAction("Chat", new {sessionId});
        }
        [HttpGet]
        public async Task<IActionResult> Chat(string sessionId)
        {
            var session = await sessionRepository.GetAsync(sessionId);
            if (session is null) throw new KeyNotFoundException($"A session with Id: {sessionId} was not found");
            
            var viewModel = new ChatViewModel
            {
                Session = session,
            };
            var user = await userManager.FindByIdAsync(session.Creator.ToString());
            viewModel.UserId = user.Id.ToString();
            viewModel.UserName = user.UserName!;

            var messages = await messageRepository.GetBySessionAsync(sessionId);

            viewModel.Messages = messages.OrderBy(m => m.AddedAtUtc).ToList(); ;

            return View(viewModel);
        }


    }
}
