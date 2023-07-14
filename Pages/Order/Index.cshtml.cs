using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment02_New.Data;
using Assignment02_New.Models;

namespace Assignment02_New.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly Assignment02_New.Data.ApplicationDBContext _context;

        public IndexModel(Assignment02_New.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IList<Orders> Orders { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Orders = await _context.Orders
                .Include(o => o.Customer).ToListAsync();
            }
        }
    }
}
