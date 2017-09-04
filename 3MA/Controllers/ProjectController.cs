using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3MA.Controllers
{
    public class ProjectController : Controller
    {
        Manager m = new Manager();
        // GET: Project
        public ActionResult Index()
        {
            return View(m.ProjectGetAll());
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)
        {
            var projectDel = m.ProjectGetById(id.GetValueOrDefault());

            if (projectDel == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(projectDel);
            }
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            string clientID = m.ProjectGetID(id.GetValueOrDefault());
            var result = m.ProjectDelete(id.GetValueOrDefault());

            try
            {
                m.DeleteUser(clientID);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("Index");
        }
    }
}
