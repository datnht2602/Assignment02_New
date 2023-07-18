using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment02_New.Data;
using Assignment02_New.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assignment02_New.Pages.Pizzas
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Assignment02_New.Data.ApplicationDBContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(Assignment02_New.Data.ApplicationDBContext context,IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public string NameSort { get; set;}
        public string CategoriesSort { get; set; }
        public string SuppliersSort { get; set; }
        public string PriceSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Products> Products { get; set; }    

       

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CategoriesSort = String.IsNullOrEmpty(sortOrder) ? "cate_desc" : "";
            SuppliersSort = String.IsNullOrEmpty(sortOrder) ? "supp_desc" : "";
            PriceSort = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            if(searchString != null){
                pageIndex = 1;
            }else{
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            // IQueryable<Products> productsIQ = from p in _context.Products select p;
            IQueryable<Products> productsIQ =  _context.Products
              .Include(p =>p.Categories)
            .Include(p =>p.Suppliers).AsQueryable();
            int number;
            if(!String.IsNullOrEmpty(searchString))
            {
                if(int.TryParse(searchString, out number)){
                    productsIQ = productsIQ.Where( s=> s.ProductID == number || s.UnitPrice == (decimal) number );
                }else
                productsIQ = productsIQ.Where( s=> s.ProductName.Contains(searchString) );

            }
            productsIQ = sortOrder switch{
                "name_desc" => productsIQ.OrderByDescending(p => p.ProductName),
                "cate_desc" => productsIQ.OrderByDescending(p => p.Categories.CategoryName),
                "supp_desc" => productsIQ.OrderByDescending(p => p.Suppliers.CompanyName),
                "price_desc" => productsIQ.OrderByDescending(p => p.UnitPrice),
                _ => productsIQ.OrderBy(p =>p.ProductName)
            };
            var pageSize = Configuration.GetValue("PageSize", 4);
            Products = await PaginatedList<Products>.CreateAsync(productsIQ.AsNoTracking(),pageIndex ?? 1, pageSize);
        }
    }
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex {get; private set;}
        public int TotalPages { get; private set; }
        public PaginatedList(List<T> items, int count, int pageIndex, int PageSize){
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count/ (double) PageSize);
            this.AddRange(items);
        }
        public bool HasPreviousPage
        {
            get{
                return (PageIndex > 1);
            }
        }
        public bool HasNextPage
        {
            get{
                return (PageIndex < TotalPages);
            }
        }
        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int pageSize
        )
        {
            var count = await source.CountAsync();
            var items = await source.Skip(
                (pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count,pageIndex,pageSize);
        }
    }
}
