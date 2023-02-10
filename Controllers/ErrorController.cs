using Microsoft.AspNetCore.Mvc;
using Penguin.Cms.Errors;
using Penguin.Cms.Errors.Extensions;
using Penguin.Persistence.Abstractions.Interfaces;
using Penguin.Security.Abstractions.Constants;
using Penguin.Security.Abstractions.Extensions;
using Penguin.Security.Abstractions.Interfaces;
using Penguin.Web.Mvc.Extensions;
using System;

namespace Penguin.Cms.Modules.Errors.Controllers
{
    public partial class ErrorController : Controller
    {
        private const string MISSING_PERMISSIONS_MESSAGE = "User does not have required permissions for requested URL";
        protected IRepository<AuditableError> ErrorRepository { get; set; }
        protected IUserSession UserSession { get; set; }

        public ErrorController(IRepository<AuditableError> errorRepository, IUserSession userSession)
        {
            ErrorRepository = errorRepository;
            UserSession = userSession;
        }

        public new IActionResult NotFound()
        {
            return View();
        }

        public ActionResult Oops(Guid errorId)
        {
            return !UserSession.LoggedInUser.HasRole(RoleNames.SYS_ADMIN) && !HttpContext.Request.IsLocal()
                ? View(ErrorRepository.Find(errorId))
                : RedirectToRoute(new
                {
                    controller = "error",
                    area = "admin",
                    action = "detail",
                    ErrorId = errorId
                });
        }

        public ActionResult Unauthorized(string requestedUrl = "")
        {
            _ = ErrorRepository.TryAdd(new UnauthorizedAccessException(MISSING_PERMISSIONS_MESSAGE), false, requestedUrl);
            return View();
        }

        public ActionResult Unauthorized(Uri requestedUrl)
        {
            throw new NotImplementedException();
        }
    }
}