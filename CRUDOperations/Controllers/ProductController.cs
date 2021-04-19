using CRUDOperations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDOperations.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDBContext _db;

        public IEnumerable<Product> ProductsList { get; set; }
        public Product singleProduct { get; set; }

        public ProductController(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {

            ProductsList = await _db.Product.ToListAsync();

            return View(ProductsList);
        }

        public IActionResult Create(string data)
        {
            ViewBag.Success = data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product productObject)
        {

            if (ModelState.IsValid)
            {
                await _db.AddAsync(productObject);
                await _db.SaveChangesAsync();

                return RedirectToAction("Create", new { data = "Successfully Saved" });
            }
            else
            {
                return View();
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            singleProduct = await _db.Product.FindAsync(id);



            return View(singleProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product productObject)
        {

            if (ModelState.IsValid)
            {
                singleProduct = await _db.Product.FindAsync(productObject.ID);
                singleProduct.ProductName = productObject.ProductName;
                singleProduct.UnitPrice = productObject.UnitPrice;
                singleProduct.TotalQuantity = productObject.TotalQuantity;
                await _db.SaveChangesAsync();

                ProductsList = await _db.Product.ToListAsync();

                return View("Index", ProductsList);
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            singleProduct = await _db.Product.FindAsync(id);
            if (singleProduct == null)
            {
                return NotFound();
            }

            _db.Product.Remove(singleProduct);
            await _db.SaveChangesAsync();

            ProductsList = await _db.Product.ToListAsync();

            return View("Index", ProductsList);
        }

    }
}
