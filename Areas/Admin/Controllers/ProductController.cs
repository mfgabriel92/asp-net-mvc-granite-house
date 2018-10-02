using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Models;
using GraniteHouse.Models.ViewModels;
using GraniteHouse.Utility;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HostingEnvironment _hostingEnvironment;

        [BindProperty]
        public ProductViewModel ProductViewModel { get; set; }

        public ProductController(ApplicationDbContext context, HostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            ProductViewModel = new ProductViewModel()
            {
                ProductType = _context.ProductType.ToList(),
                SpecialTag = _context.SpecialTag.ToList(),
                Product = new Product()
            };
        }

        public async Task<IActionResult> Index()
        {
            var products = _context.Product.Include(m => m.ProductType).Include(m => m.SpecialTag);
            return View(await products.ToListAsync());
        }

        public IActionResult Create()
        {
            return View(ProductViewModel);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid)
            {
                return View(ProductViewModel);
            }

            _context.Product.Add(ProductViewModel.Product);
            await _context.SaveChangesAsync();

            string webrootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var product = _context.Product.Find(ProductViewModel.Product.ID);

            if (files.Count != 0)
            {
                var uploads = Path.Combine(webrootPath, Constant.DefaultProdutImagePath);
                var extension = Path.GetExtension(files[0].FileName);
                var newFilename = ProductViewModel.Product.ID + extension;

                using (var filestream = new FileStream(Path.Combine(uploads, newFilename), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }

                product.Image = @"/" + Constant.DefaultProdutImagePath + @"/" + newFilename;
            }
            else
            {
                var uploads = Path.Combine(webrootPath, Constant.DefaultProdutImagePath + @"/" + Constant.DefaultProductImage);
                var destination = webrootPath + @"/" + Constant.DefaultProdutImagePath + @"/" + ProductViewModel.Product.ID + ".jpg";

                System.IO.File.Copy(uploads, destination);
                product.Image = @"/" + destination;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductViewModel.Product = await _context.Product.Include(m => m.ProductType).Include(m => m.SpecialTag).SingleOrDefaultAsync(m => m.ID == id);

            if (ProductViewModel.Product == null)
            {
                return NotFound();
            }

            return View(ProductViewModel);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int id)
        {
            if (!ModelState.IsValid)
            {
                return View(ProductViewModel);
            }

            string webrootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var product = _context.Product.Where(m => m.ID == ProductViewModel.Product.ID).FirstOrDefault();

            if (files.Count != 0)
            {
                var uploads = Path.Combine(webrootPath, Constant.DefaultProdutImagePath);
                var extensionNew = Path.GetExtension(files[0].FileName);
                var extensionOld = Path.GetExtension(product.Image);
                var oldImagePath = Path.Combine(uploads, ProductViewModel.Product.ID + extensionOld);
                var newFilename = ProductViewModel.Product.ID + extensionNew;

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                using (var filestream = new FileStream(Path.Combine(uploads, newFilename), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }

                ProductViewModel.Product.Image = @"/" + Constant.DefaultProdutImagePath + @"/" + newFilename;
            }

            if (ProductViewModel.Product.Image != null)
            {
                product.Image = ProductViewModel.Product.Image;
            }

            product.Name = ProductViewModel.Product.Name;
            product.Price = ProductViewModel.Product.Price;
            product.IsAvailable = ProductViewModel.Product.IsAvailable;
            product.ProductTypeId = ProductViewModel.Product.ProductTypeId;
            product.SpecialTagId = ProductViewModel.Product.SpecialTagId;
            product.ShadeColor = ProductViewModel.Product.ShadeColor;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductViewModel.Product = await _context.Product.Include(m => m.ProductType).Include(m => m.SpecialTag).SingleOrDefaultAsync(m => m.ID == id);

            if (ProductViewModel.Product == null)
            {
                return NotFound();
            }

            return View(ProductViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductViewModel.Product = await _context.Product.Include(m => m.ProductType).Include(m => m.SpecialTag).SingleOrDefaultAsync(m => m.ID == id);

            if (ProductViewModel.Product == null)
            {
                return NotFound();
            }

            return View(ProductViewModel);
        }
    }
}