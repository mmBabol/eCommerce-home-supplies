using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3MA.Controllers
{
    public class HomeController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        public ActionResult Index()
        {
            ViewData["ProductsCount"] = m.getCount();
            
            ViewData["Flooring"] = m.getProduct("Flooring");
            //ViewBag.ProductsCount = m.getCount();

            return View();
        }

        // GET: Product/Details/5
        //[Route("Home/Details")]
        public ActionResult Details(int? id)
        {
            var o = m.ProductGetById(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            return View(o);
        }

        [HttpPost]
        public JsonResult AjaxMethod(string name)
        {
            return null;
        }
    }
}