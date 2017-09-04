using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3MA.Controllers
{
    public class ProductController : Controller
    {
        private Manager m = new Manager();
        // GET: Product
        public ActionResult Index()
        {
            //return View(m.ProductGetAll());
            return View();
        }


        // TODO - ProductsController partial views
        [Route("Paint_product/")]
        public ActionResult Paint()
        {
            return View();
        }

        [Route("Millwork_product/")]
        public ActionResult Millwork()
        {
            return View();
        }

        [Route("Tiles_product/")]
        public ActionResult Tiles()
        {
            return PartialView("_Tiles");
        }

        [Route("Flooring_product/")]
        public ActionResult Flooring()
        {
            var search = new ProductSearchForm();

            search.dimX = m.getAllDimX("Flooring");
            search.dimY = m.getAllDimY("Flooring");
            search.dimZ = m.getAllDimZ("Flooring");
            search.PriceCat = m.getAllPriceCat("Flooring");

            return PartialView("_Flooring", search);
        }

        [Route("Door_product/")]
        public ActionResult Door()
        {
            return View();
        }

        [Route("Plumbing_product/")]
        public ActionResult Plumbing()
        {
            return View();
        }

        [Route("Appliances_product/")]
        public ActionResult Appliances()
        {
            return View();
        }

        [Route("Flooring_Category/")]
        public ActionResult Flooring_Category()
        {
            var search = new ProductSearchForm();

            search.dimX = m.getAllDimX("Flooring");
            search.dimY = m.getAllDimY("Flooring");
            search.dimZ = m.getAllDimZ("Flooring");
            search.PriceCat = m.getAllPriceCat("Flooring");

            ViewData["next"] = "all";

            return View("_Flooring", search);
        }

        [Route("product/sub/{cat}/{sub}")]
        public ActionResult ProductSub(string cat, string sub)
        {

            var products = m.ProductSearch(cat, sub).ToList();

            return PartialView("_ProductList", products);
        }


        // ProductSearch - Search for specific products.
        [Route("product/search/{cat}/{col}/{nam}/{x}/{y}/{z}/{price}")]
        public ActionResult ProductSearch(string cat, string col, string nam, string x, string y, string z, string price)
        {
            List<string> filterCat = new List<string>();
            var products = m.ProductSearchForm(cat, col, nam, x, y, z, price, filterCat).ToList();

            if (products.Count == 0 || products == null)
            {
                //return Content("<h3>No results found.</h3>", "text/html");
                return PartialView("NotFound");
            }

            return PartialView("_ProductList", products);
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.ProductGetById(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            return View(o);
        }

        // GET: Product/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var form = new ProductAddForm();
            return View(form);
        }

        // POST: Product/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductAdd newItem)
        {
            var addedItem = m.ProductAdd(newItem);
            if(addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.Id });
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var o = m.ProductGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = m.mapper.Map<ProductEditForm>(o);
                return View(editForm);
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, ProductEditInfo editItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return RedirectToAction("edit", new { id = editItem.Id });
            }
            if (id.GetValueOrDefault() != editItem.Id)
            {
                return RedirectToAction("index");
            }

            // Attempt to do the update
            var editedItem = m.ProductEditInfo(editItem);

            if (editedItem == null)
            {
                return RedirectToAction("edit", new { id = editItem.Id });
            }
            else
            {
                return RedirectToAction("details", new { id = editItem.Id });
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            var proDel = m.ProductGetById(id.GetValueOrDefault());
            if(proDel == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(proDel);
            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.ProductDelete(id.GetValueOrDefault());

            return RedirectToAction("Index");
        }
    }
}
