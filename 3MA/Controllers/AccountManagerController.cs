using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3MA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountManagerController : Controller
    {

        private Manager m = new Manager();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountManagerController()
        {

        }

        public AccountManagerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return View(m.UsersGetAll());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            return View(m.GetUserById(id));
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var o = m.GetUserById(id);

            if(o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Createa form
                var form = new ApplicationUserEditForm();
                //form = AutoMapper.Mapper<ApplicationUserEditForm>(o);


            }



            return View();
        }

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

        [HttpGet]
        public ActionResult UserFinder()
        {
            return View();
        }

        // Use partial view to Find users
        // This is the Ajax listener method
        public ActionResult FindUser(string findString)
        {
            //Attempt to find matching objects
            var fetchedObjects = m.FindUsers(findString);
            return PartialView("_FindUser", fetchedObjects);
        }

        public ActionResult Delete(string id)
        {
            return View(m.GetUserById(id));
        }

        [HttpPost]
        public ActionResult Delete(string id, ApplicationUserBase user)
        {
            if (id == null)
            {
                return null;
            }
            try {
                try
                {
                    string role = m.GetUserRole(id);

                    if (role == "Customer")
                    {
                        m.POrderDeleteWithCustomerID(id);
                    }
                    else if(role == "Client")
                    {
                        m.ProjectDeleteWithClientID(id);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                // User stays logged in, need a way to kill the session
                // If the user being deleted is user himself
                if (id == System.Web.HttpContext.Current.User.Identity.GetUserId())
                {
                    m.DeleteUser(id);
                }

                m.DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
