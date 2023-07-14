using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment02_New.Data;
using Assignment02_New.Models;

namespace Assignment02_New.Pages.Member
{
    public class CreateModel : PageModel
    {
        private readonly Assignment02_New.Data.ApplicationDBContext _context;

        public CreateModel(Assignment02_New.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Customers == null || Customer == null)
            {
                return Page();
            }
            var existingCustomer = _context.Customers.FirstOrDefault(c => c.ContactName == Customer.ContactName);
            if(existingCustomer != null){
                ModelState.AddModelError(string.Empty, "User already exists");
                return Page();
            }
            var account = new Account{
                UserName = Customer.ContactName,
                Password = Customer.Password,
                FullName = Customer.ContactName
            };
            _context.Accounts.Add(account);
            _context.Customers.Add(Customer);
            HttpContext.Session.SetString("CurrentAccount", account.UserName);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
