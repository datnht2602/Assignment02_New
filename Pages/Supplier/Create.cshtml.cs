using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment02_New.Data;
using Assignment02_New.Models;

namespace Assignment02_New.Pages.Supplier
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
        public Suppliers Suppliers { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Suppliers == null || Suppliers == null)
            {
                return Page();
            }

            _context.Suppliers.Add(Suppliers);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
