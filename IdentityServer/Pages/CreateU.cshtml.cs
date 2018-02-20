using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages
{
    public class CreateUModel : PageModel
    {
        public void OnGet()
        {
            var salt = IdentityServer.Persistence.AppUser.PasswordSaltInBase64();
            var password = IdentityServer.Persistence.AppUser.PasswordToHashBase64("Site@Cnh", salt);
            var b = salt + password;
        }
    }
}