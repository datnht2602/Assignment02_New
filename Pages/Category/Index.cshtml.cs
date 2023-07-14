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
    public class IndexModel : PageModel
    {
        private readonly Assignment02_New.Data.ApplicationDBContext _context;

        public IndexModel(Assignment02_New.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IList<Categories> Categories { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Categories != null)
            {
                Categories = await _context.Categories.ToListAsync();
            }
        }
    }
}
