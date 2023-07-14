using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment02_New.Data;
using Assignment02_New.Models;

namespace Assignment02_New.Pages.Supplier
{
    public class DeleteModel : PageModel
    {
        private readonly Assignment02_New.Data.ApplicationDBContext _context;

        public DeleteModel(Assignment02_New.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Suppliers Suppliers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var suppliers = await _context.Suppliers.FirstOrDefaultAsync(m => m.SupplierID == id);

            if (suppliers == null)
            {
                return NotFound();
            }
            else 
            {
                Suppliers = suppliers;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }
            var suppliers = await _context.Suppliers.FindAsync(id);

            if (suppliers != null)
            {
                Suppliers = suppliers;
                _context.Suppliers.Remove(Suppliers);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
