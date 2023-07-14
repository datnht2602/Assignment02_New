using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment02_New.Data;
using Assignment02_New.Models;

namespace Assignment02_New.Pages.Supplier
{
    public class EditModel : PageModel
    {
        private readonly Assignment02_New.Data.ApplicationDBContext _context;

        public EditModel(Assignment02_New.Data.ApplicationDBContext context)
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

            var suppliers =  await _context.Suppliers.FirstOrDefaultAsync(m => m.SupplierID == id);
            if (suppliers == null)
            {
                return NotFound();
            }
            Suppliers = suppliers;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Suppliers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuppliersExists(Suppliers.SupplierID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SuppliersExists(int id)
        {
          return (_context.Suppliers?.Any(e => e.SupplierID == id)).GetValueOrDefault();
        }
    }
}
