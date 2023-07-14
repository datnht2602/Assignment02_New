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
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Assignment02_New.Pages.Pizzas
{
    public class EditModel : PageModel
    {
        private readonly Assignment02_New.Data.ApplicationDBContext _context;
        private readonly IWebHostEnvironment  _environment;

        public EditModel(Assignment02_New.Data.ApplicationDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Products Products { get; set; } = default!;
           [Display(Name ="Image")]
		[DataType(DataType.Upload)]
		[CheckFileExtension(Extensions = "jpg,png,jpeg,gif")]
        public IFormFile UploadFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products =  await _context.Products.FirstOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }
            Products = products;
           ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
           ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "CompanyName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id, Products Products)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { isValid = false, html=""});
            }else {
                          _context.Attach(Products).State = EntityState.Modified;

            try
            {
                    if(UploadFile != null){
            var filepath = Path.Combine(_environment.WebRootPath,"uploads",UploadFile.FileName);
            string relativePath = filepath.Substring(_environment.WebRootPath.Length).Replace("/", "\\");
            Products.ProductImage = relativePath;
           Console.WriteLine(filepath);
            using (var fileStream = new FileStream(filepath,FileMode.Create)){
                 await UploadFile.CopyToAsync(fileStream);

            }
            }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(Products.ProductID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            

            return new JsonResult(new { isValid = true, html=""});
            }

  
        }

        private bool ProductsExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductID == id)).GetValueOrDefault();
        }
    }
}
