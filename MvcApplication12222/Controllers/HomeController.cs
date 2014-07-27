using MvcApplication12222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcApplication12222.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
           
            return View();
        }

        

        public ActionResult AddLocation()
        {

            return View(new Location());
        }

        [HttpPost]
        public ActionResult SaveLocation(Location model)
        {
            var entity = new DB_myEntities();
            entity.Locations.Add(model);
            entity.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddProducer()
        {
            var entity = new DB_myEntities();
            var locations=entity.Locations.ToList();
            var locationList=locations.Select(x=>new SelectListItem{Text=x.Name,
                                                                    Value=x.Id.ToString()
            }).ToList();                                            
             ViewBag.List=locationList;                             
            return View(new Producer());
        }

        [HttpPost]
        public ActionResult SaveProducer(Producer model)
        {
            var entity = new DB_myEntities();
            model.Location = entity.Locations.ToList().First(x=>x.Id==model.LocationID);
            entity.Producers.Add(model);
            entity.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddProduct()
        {

            var entity = new DB_myEntities();
            var producers = entity.Producers.ToList();
            var producersList = producers.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.List = producersList;
            return View(new Product());
        }

        [HttpPost]
        public ActionResult SaveProduct(Product model)
        {
            var entity = new DB_myEntities();
            model.Producer= entity.Producers.ToList().First(x => x.Id == model.ProducerID);
            entity.Products.Add(model);
            entity.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ActionLocations()
        {
            var entity = new DB_myEntities();            
            var locationsList = entity.Locations.ToList();
            ViewBag.LocationsList = locationsList;
            return View();
        }

        public JsonResult LocationsList()
        {
            var entity = new DB_myEntities();
            var locationsList = entity.Locations.ToList().Select(x => new
            {
                Name = x.Name
            }).ToList();
            return Json(locationsList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActionProducer()
        {
            var entity = new DB_myEntities();
            var locations = entity.Locations.ToList();
            var locationsList = locations.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.List = locationsList;

            var producersList = entity.Producers.ToList();
            ViewBag.ProducersList = producersList;
            return View();
        }

        public JsonResult ProducersList()
        {
            var entity = new DB_myEntities();
            var producersList = entity.Producers.ToList().Select(x => new
            {
                Name = x.Name                
            }).ToList();
            return Json(producersList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActionProducts()
        {
            var entity = new DB_myEntities();
            var producers = entity.Producers.ToList();
            var producersList = producers.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.List = producersList;

            var productsList = entity.Products.ToList();
            ViewBag.ProductsList = productsList;
            return View();
        }

        public JsonResult ProductsList()
        {
            var entity = new DB_myEntities();
            var productsList = entity.Products.ToList().Select(x => new
            {
                Name = x.Name,
                Count = x.Count,
                Price = x.Price
            }).ToList();
            return Json(productsList, JsonRequestBehavior.AllowGet);
        }
    }
}
