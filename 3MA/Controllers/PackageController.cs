using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3MA.Controllers
{
    public class PackageController : Controller
    {
        private Manager m = new Manager();
        // GET: Package
        public ActionResult Index()
        {
            return View(m.PackageWithProductsGetAll());
        }

        public ActionResult Order()
        {
            return View(m.PackageWithProductsGetAll());
        }

        // GET: Package/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.PackageGetById(id.GetValueOrDefault());
            
            if(o == null)
            {
                return HttpNotFound();

            }
            else
            {
                return View(o);
            }
        }
        
        // GET: Package/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var form = new PackageAddForm();
            form.Products = m.ProductGetAll().ToList();
            return View(form);
        }

        // POST: Package/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PackageWithProducts newItem)
        {
            var addedItem = m.PackageAdd(newItem);
            if(addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        // GET: Package/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var o = m.PackageGetById(id.GetValueOrDefault());

            if(o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form
                var editForm = m.mapper.Map<PackageEditForm>(o);

                editForm.PackageId = o.Id;
                editForm.name = o.name;
                editForm.ImgURL = o.ImgURL;
                editForm.Products = o.Products.OrderBy(mc => mc.MainCategory).ThenBy(sc => sc.SubCategory).ThenBy(n => n.Name);


                //editForm.ProductsList = new MultiSelectList(m.ProductGetAll(), "Id", "getName");

                editForm.ProductsList = new MultiSelectList(

                    items: m.ProductGetAll(),
                    dataValueField: "Id",
                    dataTextField: "getName",
                    selectedValues: o.Products.Select(p => p.Id));



                return View(editForm);
            }
        }

        // POST: Package/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PackageEditInfo newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return RedirectToAction("edit", new { id = newItem.Id });
            }

            if(id.GetValueOrDefault() != newItem.Id)
            {
                return RedirectToAction("index");
            }

            //foreach(var i in newItem.ProductIds)
            //{
            //    newItem.Products.Add(m.ProductGetById(i));
            //}

            // Attempt to do the update
            var editedItem = m.PackageEditInfo(newItem);

            if(editedItem == null)
            {
                // There was a problem updating the object
                // Our 'version 1' approach is to display the 'edit form' again
                return RedirectToAction("edit", new { id = editedItem.Id });
            }
            else
            {
                return RedirectToAction("details", new { id = editedItem.Id });
            }
        }

        // GET: Package/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemDelete = m.PackageGetById(id.GetValueOrDefault());
            if(itemDelete == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(itemDelete);
            }
        }

        // POST: Package/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            
            var result = m.PackageDelete(id.GetValueOrDefault());

            return RedirectToAction("Index");

        }
    }
}
