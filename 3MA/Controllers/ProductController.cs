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
        public ActionResult Paint()
        {
            return View();
        }

        public ActionResult Millwork()
        {
            return View();
        }

        [Route("Tiles/")]
        public ActionResult Tiles()
        {
            return PartialView("_Tiles");
        }

        [Route("Flooring/")]
        public ActionResult Flooring()
        {
            return PartialView("_Flooring", new ProductSearchForm());
        }

        public ActionResult Door()
        {
            return View();
        }

        public ActionResult Plumbing()
        {
            return View();
        }

        public ActionResult Appliances()
        {
            return View();
        }

        [Route("product/sub/{cat}/{sub}")]
        //public ActionResult ProductSearch(string main, string search)
        public ActionResult ProductSub(string cat, string sub)
        {
            //var c = m.ProductSearch(main, search);
            var c = m.ProductSearch(cat, sub);

            return PartialView("_ProductList", c);
        }

        //TODO: fix searching for product only
        [Route("product/search/{cat}/{col}/{nam}")]
        //public ActionResult ProductSearch(string main, string search)
        public ActionResult ProductSearch(string cat, string col, string nam)
        {
            //var category = Request.QueryString["category"];
            //var name = Request.QueryString["name"];

            var c = m.ProductSearchForm(cat, col, nam, "g", "h", "h", "A");
            //var c = m.ProductSearch("Flooring", sub);


            return PartialView("_ProductList", c);
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
