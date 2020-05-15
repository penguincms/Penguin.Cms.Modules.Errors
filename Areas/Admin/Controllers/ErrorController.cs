using Microsoft.AspNetCore.Mvc;
using Penguin.Cms.Errors;
using Penguin.Cms.Modules.Dynamic.Areas.Admin.Controllers;
using Penguin.Persistence.Abstractions.Interfaces;
using Penguin.Security.Abstractions.Constants;
using Penguin.Web.Security.Attributes;
using System;

namespace Penguin.Cms.Modules.Errors.Areas.Admin.Controllers
{
    [RequiresRole(RoleNames.SYS_ADMIN)]
    public partial class ErrorController : ObjectManagementController<AuditableError>
    {
        protected IRepository<AuditableError> ErrorRepository { get; set; }

        public ErrorController(IServiceProvider serviceProvider, IRepository<AuditableError> errorRepository) : base(serviceProvider)
        {
            this.ErrorRepository = errorRepository;
        }

        public ActionResult Detail(Guid errorId)
        {
            return this.View(this.ErrorRepository.Find(errorId));
        }
    }
}