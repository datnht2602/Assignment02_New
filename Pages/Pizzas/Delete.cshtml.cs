using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment02_New.Data;
using Assignment02_New.Models;

namespace Assignment02_New.Pages.Pizzas
{
    public class DeleteModel : PageModel
    {
        private readonly Assignment02_New.Data.ApplicationDBContext _context;

        public DeleteModel(Assignment02_New.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Products Products { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products.Include(p => p.Suppliers).Include(p => p.Categories).FirstOrDefaultAsync(m => m.ProductID == id);

            if (products == null)
            {
                return NotFound();
            }
            else 
            {
                Products = products;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var products = await _context.Products.FindAsync(id);

            if (products != null)
            {
                Products = products;
                _context.Products.Remove(Products);
                await _context.SaveChangesAsync();
            }

              return new JsonResult(new { html=""});
        }
    }
}
