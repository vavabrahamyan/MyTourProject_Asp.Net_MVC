using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tour_WebApp.Models;

namespace Tour_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private TourContext db = new TourContext();
        private static List<int> FTours = new List<int>();
        public ActionResult Index()
        {
            List<Category> categories = db.Categories.ToList();
            List<Tour> tours = db.Tours.ToList();
            ViewBag.ListCategories = categories;
            // ViewBag.ListTours = tours;
            List<Tour> f_tours = new List<Tour>();
            foreach (var item in db.Tours.ToList())
            {
                if (FTours.Contains(item.Id))
                {
                    f_tours.Add(item);
                }
            }
            ViewBag.ListTours = f_tours;
            Filter();
            return View();
        }
        public ActionResult TourDetails(int id = 0)
        {
            Tour tour = db.Tours.Find(id);
            return View(tour);
        }
        [HttpGet]
        public ActionResult CreateTour()
        {
            ViewBag.CategoryList = db.Categories.ToList();
            return View(new Tour());
        }
        [HttpPost]
        public ActionResult CreateTour(Tour tour, int[] selectedList)
        {
            Tour newTour = tour;
            newTour.Categories.Clear();
            if (selectedList != null)
            {
                foreach (var c in db.Categories.Where(co => selectedList.Contains(co.Id)))
                {
                    newTour.Categories.Add(c);
                }
            }
            db.Tours.Add(newTour);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditTour(int id = 0)
        {
            Tour tour = db.Tours.Find(id);
            ViewBag.CategoryList = db.Categories.ToList();
            return View(tour);
        }
        [HttpPost]
        public ActionResult EditTour(Tour tour, int[] selectedList)
        {
            Tour newTour = db.Tours.Find(tour.Id);
            newTour.Name = tour.Name;
            newTour.StartAt = tour.StartAt;
            newTour.EndAt = tour.EndAt;

            newTour.Categories.Clear();
            if (selectedList != null)
            {
                foreach (var c in db.Categories.Where(co => selectedList.Contains(co.Id)))
                {
                    newTour.Categories.Add(c);
                }
            }

            db.Entry(newTour).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DeleteTour(int id = 0)
        {

            Tour tour = db.Tours.Find(id);
            tour.Categories.Clear();
            db.Tours.Remove(tour);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View(new Category());
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditCategory(int id = 0)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            Category newCategory = db.Categories.Find(category.Id);
            newCategory.Name = category.Name;
            db.Entry(newCategory).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DeletCategory(int id = 0)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Filter()
        {
            ViewBag.MyCategories = db.Categories.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Filter(int[] selectedList)
        {
            foreach (var item in db.Tours.ToList())
            {
                foreach (var c in item.Categories)
                {
                    if (selectedList.Contains(c.Id))
                    {
                        FTours.Add(item.Id);
                        break;
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}