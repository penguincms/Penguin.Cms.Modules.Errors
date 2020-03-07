using Microsoft.AspNetCore.Mvc;
using Penguin.Cms.Errors;
using Penguin.Cms.Errors.Extensions;
using Penguin.Persistence.Abstractions.Interfaces;
using Penguin.Security.Abstractions.Constants;
using Penguin.Security.Abstractions.Extensions;
using Penguin.Security.Abstractions.Interfaces;
using Penguin.Web.Mvc.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Penguin.Cms.Modules.Errors.Controllers
{
    [SuppressMessage("Design", "CA1054:Uri parameters should not be strings")]
    [SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
    public partial class ErrorController : Controller
    {
        private const string MISSING_PERMISSIONS_MESSAGE = "User does not have required permissions for requested URL";
        protected IRepository<AuditableError> ErrorRepository { get; set; }
        protected IUserSession UserSession { get; set; }

        public ErrorController(IRepository<AuditableError> errorRepository, IUserSession userSession)
        {
            this.ErrorRepository = errorRepository;
            this.UserSession = userSession;
        }

        public new IActionResult NotFound()
        {
            return this.View();
        }

        public ActionResult Oops(Guid errorId)
        {
            if (!this.UserSession.LoggedInUser.HasRole(RoleNames.SysAdmin) && !this.HttpContext.Request.IsLocal())
            {
                return this.View(this.ErrorRepository.Find(errorId));
            }
            else
            {
                return this.RedirectToRoute(new
                {
                    controller = "error",
                    area = "admin",
                    action = "detail",
                    ErrorId = errorId
                });
            }
        }

        public ActionResult Unauthorized(string requestedUrl = "")
        {
            this.ErrorRepository.TryAdd(new UnauthorizedAccessException(MISSING_PERMISSIONS_MESSAGE), false, requestedUrl);
            return this.View();
        }
    }
}