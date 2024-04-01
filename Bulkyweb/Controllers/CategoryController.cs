using Bulkyweb.Data;
using Bulkyweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulkyweb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) {
            
            _db = db;
        }
        public IActionResult Index()
        {

            List<Category> categories = _db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create() 
        { 
        
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {

            if(obj.Name == obj.DisplayOrder.ToString())
            {

                ModelState.AddModelError("Name", "Category name and Display order cannot be the same");
            }

            if (obj.Name != null && obj.Name.ToLower() == "test")
            {

                ModelState.AddModelError("", "test is an invalid name");
            }

            if (ModelState.IsValid) 
            {

                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
           
            return View();
            
        }

        public IActionResult Edit (int? id)
        { 
            if(id == null || id == 0) 
            {
                TempData["error"] = "operation failed";
                return NotFound();
            }

            Category? category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) 
            {
                TempData["error"] = "operation failed";
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {

                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "operation failed";
                return NotFound();
            }

            Category? category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                TempData["error"] = "operation failed";
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Category? category = _db.Categories.FirstOrDefault(u => u.Id == id);

            if (category == null)
            {
                TempData["error"] = "operation failed";
                return NotFound();
            }
            else
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
                TempData["success"] = "Category deleted successfully";
            }


            return RedirectToAction("Index");

        }
    }
}
