using Penguin.Cms.Modules.Core.ComponentProviders;
using Penguin.Cms.Modules.Core.Navigation;
using Penguin.Navigation.Abstractions;
using Penguin.Security.Abstractions;
using Penguin.Security.Abstractions.Interfaces;
using System.Collections.Generic;
using SecurityRoles = Penguin.Security.Abstractions.Constants.RoleNames;

namespace Penguin.Cms.Modules.Errors.ComponentProviders
{
    public class AdminNavigationMenuProvider : NavigationMenuProvider
    {
        public override INavigationMenu GenerateMenuTree()
        {
            return new NavigationMenu()
            {
                Name = "Admin",
                Text = "Admin",
                Children = new List<INavigationMenu>() {
                         new NavigationMenu()
                         {
                             Text = "Errors",
                             Icon = "list",
                             Href = "/Admin/Error/List",
                             Permissions = new List<ISecurityGroupPermission>()
                             {
                                 CreatePermission(SecurityRoles.SYS_ADMIN, PermissionTypes.Read | PermissionTypes.Write)
                             }
                         }
                    }
            };
        }
    }
}