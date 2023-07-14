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
    public class DetailsModel : PageModel
    {
        private readonly Assignment02_New.Data.ApplicationDBContext _context;

        public DetailsModel(Assignment02_New.Data.ApplicationDBContext context)
        {
            _context = context;
        }

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
    }
}
