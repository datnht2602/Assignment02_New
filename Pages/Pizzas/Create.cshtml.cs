using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment02_New.Data;
using Assignment02_New.Models;
using System.ComponentModel.DataAnnotations;


namespace Assignment02_New.Pages.Pizzas
{
    public class CreateModel : PageModel
    {
        private readonly Assignment02_New.Data.ApplicationDBContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public CreateModel(Assignment02_New.Data.ApplicationDBContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
        ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "CompanyName");
            return Page();
        }

        [BindProperty]
        public Products Products { get; set; } = default!;
        [Display(Name ="Image")]
		[DataType(DataType.Upload)]
		[CheckFileExtension(Extensions = "jpg,png,jpeg,gif")]
        [Required]
        public IFormFile UploadFile { get; set; }

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(Products Products)
        {
          if (!ModelState.IsValid || _context.Products == null || Products == null)
            {
                return Page();
            }
            if(UploadFile != null){
            var filepath = Path.Combine(_environment.WebRootPath,"uploads",UploadFile.FileName);
            string relativePath = filepath.Substring(_environment.WebRootPath.Length).Replace("/", "\\");
            Products.ProductImage = relativePath;
           Console.WriteLine(filepath);
            using (var fileStream = new FileStream(filepath,FileMode.Create)){
                 await UploadFile.CopyToAsync(fileStream);

            }
            }else
            Products.ProductImage = null;
           
            _context.Products.Add(Products);
            await _context.SaveChangesAsync();

            return new JsonResult(new { isValid = true, html=""});
        }
    }
}
