using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment02_New.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Assignment02_New.Pages.Accounts
{
    public class Login : PageModel
    {
        private readonly ILogger<Login> _logger;
        private readonly Data.ApplicationDBContext _context;
        public Login(Data.ApplicationDBContext applicationDBContext,ILogger<Login> logger){
            _context = applicationDBContext;
            _logger = logger;
        }
         [BindProperty]
        public Account Account {set; get;}

         public IActionResult OnGet()
        {
              var user = HttpContext.Session.GetString("CurrentAccount");
            if (user == null)
            {
                return Page();
            }
            return RedirectToPage("/Index");
        }
         public async Task<IActionResult> OnPostAsync()
        {

            var checkAccount = _context.Accounts.FirstOrDefault(a => a.UserName == Account.UserName && a.Password == Account.Password);
            if(checkAccount != null){
                 HttpContext.Session.SetString("CurrentAccount", checkAccount.UserName);
                HttpContext.Session.SetInt32("AccountType", checkAccount.Type);
                return RedirectToPage("/Index");
            }
              ModelState.AddModelError(string.Empty, "Invalid username or password");
            return Page();
        }
    }
}