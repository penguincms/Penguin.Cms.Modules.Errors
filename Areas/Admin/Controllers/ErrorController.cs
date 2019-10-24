using Microsoft.AspNetCore.Mvc;
using Penguin.Cms.Errors;
using Penguin.Cms.Modules.Dynamic.Areas.Admin.Controllers;
using Penguin.Persistence.Abstractions.Interfaces;
using Penguin.Security.Abstractions.Attributes;
using Penguin.Security.Abstractions.Constants;
using Penguin.Web.Security.Attributes;
using System;

namespace Penguin.Cms.Modules.Errors.Areas.Admin.Controllers
{
    [RequiresRole(RoleNames.SysAdmin)]
    public partial class ErrorController : ObjectManagementController<AuditableError>
    {
        protected IRepository<AuditableError> ErrorRepository { get; set; }

        public ErrorController(IServiceProvider serviceProvider, IRepository<AuditableError> errorRepository) : base(serviceProvider)
        {
            ErrorRepository = errorRepository;
        }

        public ActionResult Detail(Guid errorId) => this.View(this.ErrorRepository.Find(errorId));
    }
}