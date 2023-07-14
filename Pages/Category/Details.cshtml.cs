using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment02_New.Data;
using Assignment02_New.Models;

namespace Assignment02_New.Pages.Category
{
    public class DetailsModel : PageModel
    {
        private readonly Assignment02_New.Data.ApplicationDBContext _context;

        public DetailsModel(Assignment02_New.Data.ApplicationDBContext context)
        {
            _context = context;
        }

      public Categories Categories { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryID == id);
            if (categories == null)
            {
                return NotFound();
            }
            else 
            {
                Categories = categories;
            }
            return Page();
        }
    }
}
