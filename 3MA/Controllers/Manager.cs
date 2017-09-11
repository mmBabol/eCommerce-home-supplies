using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using _3MA.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.IO;
using Excel;
using System.Data;
using System.Reflection;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace _3MA.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // Property to hold the user account for the current request


        // AutoMapper components
        MapperConfiguration config;
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }


        public UserAccount UserAccount { get; private set; }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                // Null coalescing operator
                // https://msdn.microsoft.com/en-us/library/ms173224.aspx
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public EntityState En { get; private set; }

        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            config = new MapperConfiguration(cfg =>
            {
                // ATTENTION - mapper definitions
                // cfg.CreateMap<SourceType, DestinationType>();


                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                cfg.CreateMap<Models.Package, Controllers.PackageBase>();
                cfg.CreateMap<Models.Package, Controllers.PackageWithProducts>();
                cfg.CreateMap<Controllers.PackageAdd, Models.Package>();
                cfg.CreateMap<Controllers.PackageWithProducts, Models.Package>();
                cfg.CreateMap<Controllers.PackageAdd, Controllers.PackageBase>();
                cfg.CreateMap<Controllers.PackageBase, Controllers.PackageEditForm>();
                cfg.CreateMap<Controllers.PackageEditInfo, Controllers.PackageEditForm>();
                cfg.CreateMap<Controllers.PackageEditInfo, Models.Package>();

                cfg.CreateMap<Models.Product, Controllers.ProductBase>();
                cfg.CreateMap<Controllers.ProductBase, Models.Product>();
                cfg.CreateMap<Controllers.ProductAdd, Models.Product>();
                cfg.CreateMap<Controllers.ProductAdd, Controllers.ProductBase>();
                cfg.CreateMap<Controllers.ProductBase, Controllers.ProductEditForm>();
                cfg.CreateMap<Controllers.ProductEditInfo, Controllers.ProductEditForm>();

                cfg.CreateMap<Models.Order, Controllers.OrderBase>();
                cfg.CreateMap<Controllers.OrderBase, Models.Order>();
                cfg.CreateMap<Controllers.OrderAddForm, Controllers.OrderBase>();

                cfg.CreateMap<Models.POrder, Controllers.POrderBase>();
                cfg.CreateMap<Controllers.POrderBase, Models.POrder>();
                cfg.CreateMap<Controllers.POrderAddForm, Controllers.POrderBase>();
                cfg.CreateMap<Controllers.POrderAdd, Models.POrder>();
                cfg.CreateMap<Models.POrder, Controllers.POrderAdd>();
                cfg.CreateMap<Controllers.POrderBase, Controllers.POrderEditForm>();
                cfg.CreateMap<Controllers.POrderEditInfo, Controllers.POrderEditForm>();
                cfg.CreateMap<Controllers.POrderEditInfo, Models.POrder>();

                cfg.CreateMap<Models.Project, Controllers.ProjectBase>();
                cfg.CreateMap<Controllers.ProjectAdd, Models.Project>();

                cfg.CreateMap<Models.Accessories, Controllers.AccessoriesBase>();
                cfg.CreateMap<Controllers.AccessoriesAdd, Models.Accessories>();

                cfg.CreateMap<Models.ApplicationUser, Controllers.ApplicationUserBase>();
                cfg.CreateMap<Controllers.RequestUser, Controllers.ApplicationUserBase>();
                cfg.CreateMap<Controllers.UserAccount, Controllers.ApplicationUserDetail>();
                cfg.CreateMap<Controllers.ApplicationUserDelete, Controllers.ApplicationUserEditForm>();
                cfg.CreateMap<Controllers.ApplicationUserEdit, Controllers.ApplicationUserDelete>();

            });

            mapper = config.CreateMapper();

            // Initialize the UserAccount property
            UserAccount = new Controllers.UserAccount(HttpContext.Current.User as ClaimsPrincipal);

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>


        // Attention - Code snippets
        // cw - Console.WriteLine();
        // do - do loop
        // else - else
        // enum - enum val
        // equals - equals function for objects
        // for - for loop
        // foreach - foreach loop
        // if - if statement
        // sim - static int main
        // struct - struct
        // svm - static void main
        // switch - switch command
        // try - try statement
        // tryf - try final statement
        // while - while loop
        // ~ - destructor


        // Get all
        public IEnumerable<AccessoriesBase> AccessoriesGetAll()
        {
            return mapper.Map<IEnumerable<AccessoriesBase>>(ds.Accessories.OrderBy(p => p.Id));
        }

        public IEnumerable<PackageBase> PackadgeGetAll()
        {
            return mapper.Map<IEnumerable<PackageBase>>(ds.Packages.OrderBy(p => p.Id));
        }

        public IEnumerable<PackageWithProducts> PackageWithProductsGetAll()
        {
            return mapper.Map<IEnumerable<PackageWithProducts>>(ds.Packages.OrderBy(p => p.Name));
        }

        public IEnumerable<ProductBase> ProductGetAll()
        {
            return mapper.Map<IEnumerable<ProductBase>>(ds.Products.OrderBy(p => p.MainCategory).ThenBy(m => m.SubCategory).ThenBy(a => a.Name));
        }

        public IEnumerable<OrderBase> OrderGetAll()
        {
            return mapper.Map<IEnumerable<OrderBase>>(ds.Orders.OrderBy(o => o.Id));
        }

        public IEnumerable<POrderBase> POrderGetAll()
        {
            //return mapper.Map<IEnumerable<POrderBase>>(ds.POrders.OrderBy(o => o.Suite).ThenBy(d => d.Id));
            return mapper.Map<IEnumerable<POrderBase>>(ds.POrders.OrderBy(o => o.ProjectName).ThenBy(d => d.Id));
            //return mapper.Map<IEnumerable<POrderBase>>(ds.POrders.SingleOrDefault(z => z.Completed == true));
        }

        public IEnumerable<ProjectBase> ProjectGetAll()
        {
            return mapper.Map<IEnumerable<ProjectBase>>(ds.Projects.OrderBy(o => o.Name).ThenBy(d => d.Developer));
        }

        public List<string> ProjectGetList()
        {
            var all = ds.Projects.OrderBy(d => d.Developer).ThenBy(n => n.Name);

            List<string> list = new List<string>();

            foreach(var pro in all)
            {
                list.Add(pro.Name + " - " + pro.Developer);
            }

            return list;
        }

        public IEnumerable<ApplicationUserBase> UsersGetAll()
        {
            // Fetch all users
            var allUsers = UserManager.Users.OrderBy(z => z.UserName);

            if (allUsers == null)
            {
                return null;
            }

            var userList = new List<ApplicationUserBase>();
            foreach ( var user in allUsers)
            {
                // Map the values all users to view model
                var appUser = mapper.Map<ApplicationUserBase>(user);
                var userClaims = user.Claims.Where(c => c.ClaimType == ClaimTypes.Role).Select(roles => roles.ClaimValue).ToArray();

                if (userClaims == null || userClaims.Count() == 0)
                {
                    string[] str = { "User", "Customer" };
                    userClaims = str;
                }

                // Add role claims
                appUser.Roles = userClaims;
                userList.Add(appUser);
            }
            return userList;
        }

        // getAllSuites() - get a Dictionary of all Suites, includes each suite and whether it is completed or no
        public Dictionary<int, bool> getAllSuites()
        {
            Dictionary<int, bool> dict = new Dictionary<int, bool>();

            var POrders = POrderGetAll();

            // Add suite to each list, mark each complete orders
            foreach (var po in POrders)
            {
                dict.Add(po.Suite, (po.Completed) ? true : false);
            }

            return dict;
        }

        // getAllSuites() - get a Dictionary of all orders, includes the order Id, whether it is completed or not, and the address/owner name
        public Dictionary<string, Tuple<string, bool>> getAllOrders()
        {
            var dict = new Dictionary<string, Tuple<string, bool>>();
            var POrders = POrderGetAll();
            var Key = "";

            foreach (var po in POrders)
            {

                if(po.ProjectName == null)
                {
                    Key = po.getAddressName;
                }
                else
                {
                    //if(po.ProjectName != project)
                    //{
                    //    // Used for headers in the select list
                    //    project = po.ProjectName;
                    //    Tuple<string, bool> tupi = new Tuple<string, bool>(project, false);
                    //    dict.Add("head", tupi);
                    //}
                    Key = po.getSuite;
                }
                Tuple<string, bool> tup = new Tuple<string, bool>(Key, (po.Completed) ? true : false);
                dict.Add(po.customerID, tup);
            }

            return dict;
        }

        public List<string> getAllDimX(string cat)
        {
            var o = ds.Products.Where(m => m.MainCategory == cat).Select(x => x.DimW).Distinct();

            return o.ToList();
        }

        public List<string> getAllDimY(string cat)
        {
            var o = ds.Products.Where(m => m.MainCategory == cat).Select(x => x.DimTH).Distinct();

            return o.ToList();
        }

        public List<string> getAllDimZ(string cat)
        {
            var o = ds.Products.Where(m => m.MainCategory == cat).Select(x => x.DimL).Distinct();

            return o.ToList();
        }

        public List<string> getAllPriceCat(string cat)
        {
            var o = ds.Products.Where(m => m.MainCategory == cat).Select(k => k.PriceCategory).Distinct();

            return o.ToList();
        }

        // getCount
        // List<int>
        // return count of items, used for home page to show number of current items in a category
        public List<int> getCount()
        {
            List<int> count = new List<int>();

            // Number of total products
            count.Add(ds.Products.Count());

            // Number of total flooring products
            count.Add(ds.Products.Where(f => f.MainCategory.ToLower().Equals("flooring")).Count());

            // Number of total tiles products
            count.Add(ds.Products.Where(f => f.MainCategory.ToLower().Equals("tile")).Count());

            // Number of total lighting products
            count.Add(ds.Products.Where(f => f.MainCategory.ToLower().Equals("lighting")).Count());

            // Number of total appliances products
            count.Add(ds.Products.Where(f => f.MainCategory.ToLower().Equals("appliances")).Count());

            // Number of total fixtures products
            count.Add(ds.Products.Where(f => f.MainCategory.ToLower().Equals("plumbing")).Count());

            return count;
        }

        public int ProjectGetId(string name)
        {
            var result = ds.Projects.SingleOrDefault(n => n.Name.ToLower().Equals(name.ToLower()));

            if (result == null)
            {
                return 0;
            }
            else
            {
                var project = mapper.Map<ProjectBase>(result);
                return project.Id;
            }
        }

        public Dictionary<int, Tuple<string, string>> getProduct(string category)
        {
            var flooringList = ds.Products.Where(fl => fl.MainCategory.ToLower().Contains(category.ToLower()));

            Dictionary<int, Tuple<string, string>> dict = new Dictionary<int, Tuple<string, string>>();
            int c = 0;

            foreach(var item in flooringList)
            {
                if(c >= 10) { break; }
                if(item.Image != null)
                {
                    Tuple<string, string> i = new Tuple<string, string>(item.Name, item.Image);
                    dict.Add(item.Id, i);
                }
                c++;
            }

            return dict;
        }


        // Get one
        public AccessoriesBase AccessoriesGetById(int id)
        {
            // Attempt to fetch the object
            var p = ds.Accessories.SingleOrDefault(o => o.Id == id);

            // Return the result, null if not found
            return (p == null) ? null : mapper.Map<AccessoriesBase>(p);
        }

        public Dictionary<int, int> POrderAccessoriesList(string id)
        {
            // Attempt to fetch the object
            //var p = ds.POrders.SingleOrDefault(o => o.Id == id);
            var p = POrderGetByCustId(id);
            if(p == null) { return null; }

            // Return the result, null if not found
            return (p.IdAccList == null) ? null : p.IdAccList;
        }

        public ProjectBase ProjectGetById(int id)
        {
            // Attempt to fetch the object
            var p = ds.Projects.SingleOrDefault(o => o.Id == id);

            // Return the result, null if not found
            return (p == null) ? null : mapper.Map<ProjectBase>(p);
        }

        public POrderBase POrderGetBySuite(int suite)
        {
            // Attempt to fetch the object
            var p = ds.POrders.Include("AllProducts").SingleOrDefault(o => o.Suite == suite);

            // Return the result, null if not found
            return (p == null) ? null : mapper.Map<POrderBase>(p);
        }

        public POrderBase POrderGetByCustId(string ID)
        {
            // Attempt to fetch the object
            var p = ds.POrders.Include("AllProducts").SingleOrDefault(o => o.customerID == ID);

            foreach(var product in p.AllProducts)
            {
                p.IdProductList.Add(product.Id);
            }

            // Return the result, null if not found
            return (p == null) ? null : mapper.Map<POrderBase>(p);
        }

        public int POrderGetOrderID(string ID)
        {
            // Attempt to fetch the object
            var p = ds.POrders.SingleOrDefault(o => o.customerID == ID);

            // Return the result, null if not found
            return (p == null) ? 0 : p.Id;
        }

        public POrderProducts POrderGetProducts(string ID)
        {
            // Attempt to fetch the object
            var p = ds.POrders.Include("AllProducts").SingleOrDefault(o => o.customerID == ID);

            //ISSUE HERE

            POrderProducts temp = new POrderProducts();
            temp.Id = p.Id;
            //temp.AllProducts = p.AllProducts.ToList();

            temp.Qty = p.Qty;

            return temp;
        }

        public POrderBase POrderGetById(int id)
        {
            // Attempt to fetch the object
            var p = ds.POrders.Include("AllProducts").SingleOrDefault(o => o.Id == id);

            // Return the result, null if not found
            return (p == null) ? null : mapper.Map<POrderBase>(p);
        }

        public PackageWithProducts PackageGetById(int id)
        {
            // Attempt to fetch the object
            var p = ds.Packages.Include("Products").SingleOrDefault(o => o.Id == id);

            // Return the result, null if not found
            return (p == null) ? null : mapper.Map<PackageWithProducts>(p);
        }

        public ProductBase ProductGetById(int id)
        {
            var o = ds.Products.Find(id);

            return (o == null)?null:mapper.Map<ProductBase>(o);
        }

        public ApplicationUserDetail GetUserById(string id)
        {
            //Fetch the User by Id
            var user = UserManager.FindById(id);
            if(user == null)
            {
                return null;
            }

            // Initialize UserAccount
            var userIdentity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie) as ClaimsIdentity;
            var claimsPrincipal = new ClaimsPrincipal(userIdentity);

            var userAccount = new UserAccount(claimsPrincipal);

            // Map user details
            var details = mapper.Map<ApplicationUserDetail>(userAccount);
            details.UserName = user.UserName;
            details.email = user.UserName;
            details.Roles = userAccount.RoleClaims;
            details.Suite = userAccount.Suite;

            return details;
        }

        // Search
        public IEnumerable<POrderBase> POrderSearch(List<int> ids)
        {
            if(ids == null)
            {
                var a = ds.POrders.Include("AllProducts").Where(c => c.Completed);
                return mapper.Map<IEnumerable<POrderBase>>(a.OrderBy(d => d.OrderPlaced));
            }
            else
            {
                // TODO fix order selection
                var p = ds.POrders.Where(c => c.Completed == true);
                return mapper.Map<IEnumerable<POrderBase>>(p.OrderBy(s => s.Suite));
            }
        }

        public IEnumerable<ProductBase> ProductSearch(string category, string sub)
        {
            if (string.IsNullOrEmpty(category))
            {
                return null;
            }
            else if(sub == "All")
            {
                var a = ds.Products.Include("AllAccessories")
                    .Where(t => t.MainCategory.ToLower().Contains(category.ToLower()));

                return mapper.Map<IEnumerable<ProductBase>>(a.OrderBy(d => d.SubCategory).
                    ThenBy(ad => ad.Name).ThenBy(p => p.PriceCategory));
            }

            var c = ds.Products.Include("AllAccessories")
                .Where(t => t.MainCategory.ToLower().Contains(category.ToLower()))
                .Where(s => s.SubCategory.ToLower().Contains(sub.ToLower()));

            return mapper.Map<IEnumerable<ProductBase>>(c.OrderBy(a => a.Name).ThenBy(p => p.PriceCategory));
        }

        public IEnumerable<ProductBase> ProductSearchForm(string category, string colour, string name, string dimX, string dimY, string dimZ, string price, List<string> filterCat)
        {
            if (string.IsNullOrEmpty(category))
            {
                return null;
            }

            //var p = ds.Products.Where(a => a.MainCategory.ToLower().Contains(category.ToLower())).Where(a => a.Name.ToLower().Contains(name.ToLower()));

            var p = ds.Products.Include("AllAccessories").Where(a => a.MainCategory.ToLower().Contains(category.ToLower()));

            if (!filterCat.Contains("All"))
            {


                while (filterCat.Count < 4)
                {
                    filterCat.Add("null");
                }

                string a = filterCat[0],
                    b = filterCat[1],
                    c = filterCat[2],
                    d = filterCat[3];







                p = p.Where(n => n.SubCategory.ToLower().Contains(a.ToLower()) ||
                n.SubCategory.ToLower().Contains(b.ToLower()) ||
                n.SubCategory.ToLower().Contains(c.ToLower()) ||
                n.SubCategory.ToLower().Contains(d.ToLower()));
            }

            // TODO: add colour field into database, make it searchable
            //if (colour != "null" && colour != null)
            //{
            //    p = p.Where(c => c.)
            //}

            if(name != "null" && name != null)
            {
                p = p.Where(n => n.Name.ToLower().Contains(name.ToLower()));
            }

            if(dimX != "null" && dimX != null)
            {
                p = p.Where(x => x.DimW.ToLower().Contains(dimX.ToLower()));
            }

            if (dimY != "null" && dimY != null)
            {
                p = p.Where(y => y.DimTH.ToLower().Contains(dimY.ToLower()));
            }

            if (dimZ != "null" && dimZ != null)
            {
                p = p.Where(z => z.DimL.ToLower().Contains(dimZ.ToLower()));
            }

            if (price != "null" && price != null)
            {
                p = p.Where(c => c.PriceCategory.ToLower().Contains(price.ToLower()));
            }


            return mapper.Map<IEnumerable<ProductBase>>(p.OrderBy(a => a.Name).ThenBy(pr => pr.PriceCategory));
        }

        //public IEnumerable<ProductBase> ProductSearchForm(string category, string colour, string name)
        //{
        //    if (string.IsNullOrEmpty(category))
        //    {
        //        return null;
        //    }

        //    var p = ds.Products.Where(a => a.MainCategory.ToLower().Contains(category.ToLower()))
        //        .Where(a => a.Name.ToLower().Contains(name.ToLower()));

        //    return mapper.Map<IEnumerable<ProductBase>>(p.OrderBy(a => a.Name).ThenBy(pr => pr.PriceCategory));
        //}

        // Add
        public PackageBase PackageAdd(PackageAdd newItem)
        {
            var addedItem = ds.Packages.Add(mapper.Map<Package>(newItem));
            ds.SaveChanges();

            return (addedItem == null)?null:mapper.Map<PackageBase>(newItem);
        }

        public ProductBase ProductAdd(ProductAdd newItem)
        {

            var addedItem = ds.Products.Add(mapper.Map<Product>(newItem));
            ds.SaveChanges();

            return ( addedItem == null)?null:mapper.Map<ProductBase>(addedItem);
        }

        public bool POrderAdd(POrderAdd order)
        {
            var addedOrder = ds.POrders.Add(mapper.Map<POrder>(order));

            try
            {
                ds.SaveChanges();
            }
            catch(DbEntityValidationException ex)
            {
                foreach(DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry
                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages
                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occured in {1} at {2}",
                            subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }
                }
            }
            catch(DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
            }



            return (addedOrder == null) ? false : true;
        }

        public ProjectBase ProjectAdd(ProjectAdd newItem)
        {

            var addedItem = ds.Projects.Add(mapper.Map<Project>(newItem));
            ds.SaveChanges();

            return (addedItem == null) ? null : mapper.Map<ProjectBase>(addedItem);
        }

        // Delete
        public void DeleteUser(string id)
        {
            var user = UserManager.FindById(id);

            // Initiaize UserAccount
            var userIdentity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie) as ClaimsIdentity;
            var claimsPrincipal = new ClaimsPrincipal(userIdentity);

            var userAccount = new UserAccount(claimsPrincipal);

            // Get all claims
            var claims = claimsPrincipal.Claims;
            // Set a flag for successful remove
            var check = true;
            // Remove all claims from user
            foreach (var claim in claims)
            {
                var r = UserManager.RemoveClaimAsync(user.Id, new Claim(claim.Type, claim.Value)).Result;
                if (!r.Succeeded) { check = false; }
            }

            // Finally remove the user
            if (check)
            {
                var result = UserManager.DeleteAsync(user).Result;
            }
        }

        public bool ProjectDelete(int id)
        {
            //var proDel = ds.POrders.Find(id);
            var proDel = ds.Projects.SingleOrDefault(o => o.Id == id);

            if (proDel == null)
            {
                return false;
            }
            else
            {
                string projectId = proDel.ClientID;
                proDel.ClientID = null;
                ds.SaveChanges();

                ds.Projects.Remove(proDel);

                try
                {
                    ds.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return true;
            }
        }

        public string ProjectGetID(int id)
        {
            var prob = ds.Projects.Find(id);
            return (prob == null) ? "" : prob.ClientID;
        }

        public bool ProjectDeleteWithClientID(string id)
        {
            var proDel = ds.Projects.SingleOrDefault(o => o.ClientID == id);
            return ProjectDelete(proDel.Id);
        }

        public bool POrderDelete(int id)
        {
            //var proDel = ds.POrders.Find(id);
            var proDel = ds.POrders.Include("AllProducts")
            .SingleOrDefault(o => o.Id == id);

            if (proDel == null)
            {
                return false;
            }
            else
            {
                string orderId = proDel.customerID;
                proDel.AllProducts.Clear();
                proDel.customerID = null;
                ds.SaveChanges();
                proDel = ds.POrders.Include("AllProducts")
                .SingleOrDefault(o => o.Id == id);

                ds.POrders.Remove(proDel);

                try
                {
                    ds.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return true;
            }
        }

        public bool POrderDeleteWithCustomerID(string id)
        {
            var proDel = ds.POrders.SingleOrDefault(o => o.customerID == id);
            return POrderDelete(proDel.Id);
        }

        public string POrderGetID(int id)
        {
            var prob = ds.POrders.Find(id);
            return (prob == null) ? "" : prob.customerID;
        }

        public bool PackageDelete(int id)
        {
            // Attept to fetch the object to be deleted
            var itemToDelete = ds.Packages.Find(id);

            if(itemToDelete == null){
                return false;
            }
            else
            {
                // TODO - remove() fails because products are in list, make list = null before deleting
                ds.Packages.Remove(itemToDelete);
                ds.SaveChanges();

                return true;
            }
        }

        public bool ProductDelete(int id)
        {
            var proDel = ds.Products.Find(id);

            if(proDel == null)
            {
                return false;
            }
            else
            {
                ds.Products.Remove(proDel);
                ds.SaveChanges();
                return true;
            }
        }

        // Edit
        public POrderBase POrderEdit(POrderBase poorder)
        {
            // Attempt to fetch the object
            var p = ds.POrders.Include("AllProducts").SingleOrDefault(o => o.Id == poorder.Id);

            if (p == null)
            {
                return null;
            }
            else
            {
                p.Completed = poorder.Completed;
                p.LightUpgrade = poorder.LightUpgrade;
                p.OrderPlaced = poorder.OrderPlaced;
                p.Qty = poorder.Qty;
                p.Room = poorder.Room;
                p.Suite = poorder.Suite;

                p.AllProducts.Clear();

                foreach(var poo in poorder.IdProductList)
                {
                    var m = ds.Products.Find(poo);
                    p.AllProducts.Add(m);
                }
                //foreach (var pp in poorder.AllProducts)
                //{
                //    p.AllProducts(pp);
                //}

                //ds.Entry(p).CurrentValues.SetValues(poorder);
                //ds.Entry(p).State = EntityState.Modified;
                //ds.Entry(poorder).State = EntityState.Modified;

                //ds.Attach(account);
                //ObjectStateEntry entry = context.ObjectStateManager.GetObjectStateEntry(account);
                //entry.SetModified();
                

                //ds.POrders.SaveChanges();
                ds.SaveChanges();

                //ds.SaveChanges();
                return mapper.Map<POrderBase>(p);
            }
        }

        //public POrderBase POrderUpdateQty(int Id, Dictionary<int, int> qty)
        //{
        //    // Attempt to fetch the object
        //    var p = ds.POrders.SingleOrDefault(o => o.Id == Id);
        //    //if (p == null) { return false; }

        //    var pooping = mapper.Map<POrderBase>(p);

        //    p.Qty.Clear();
        //    p.Qty = qty;

        //    //ds.Entry(p).State = EntityState.Modified;
        //    //ds.POrders.SingleOrDefault(o => o.Id == Id).Qty = qty;
        //    //var poop = mapper.Map<POrderBase>(p);

        //    ds.SaveChanges();
        //    return mapper.Map<POrderBase>(p);
        //}

        public bool POrderUpdateLists(POrderProducts porder)
        {
            // Attempt to fetch the object
            //var p = ds.POrders.Include("AllProducts").SingleOrDefault(o => o.Id == porder.Id);
            var po = ds.POrders.Find(porder.Id);
            var p = ds.POrders
                .Include("AllProducts")
                .SingleOrDefault(o => o.Id == porder.Id);

            if (p == null)
            {
                return false;
            }
            ds.Entry(p).CurrentValues.SetValues(porder);


            // Update the object with the incoming values

            // First, clear out the existing collection
            p.AllProducts.Clear();

            // Then, go through the incoming items
            // For each one, add to the fetched object's collection
            foreach (var item in porder.IdProductList)
            {
                var a = ds.Products.Find(item);
                p.AllProducts.Add(a);
            }



            ds.SaveChanges();
            //return mapper.Map<POrderBase>(p);
            return true;
        }

        public bool POrderUpdateQtys(POrderBase po)
        {
            // Attempt to fetch the object
            var p = ds.POrders.Include("AllProducts").SingleOrDefault(o => o.Id == po.Id);
            if (p == null)
            {
                return false;
            }
            p.Qty = po.Qty;
            // add product


            ds.SaveChanges();
            //return mapper.Map<POrderBase>(p);
            return true;
        }

        public bool POrderUpdateCart(string id, ProductBase product, bool addToCart)
        {
            var o = POrderGetByCustId(id);
            if(o == null)
            {
                return false;
            }
            
            if (addToCart)
            {
                o.AllProducts.Add(product);
            }
            else
            {
                o.AllProducts.Remove(product);
            }

            ds.SaveChanges();

            return true;
        }

        public bool POrderAccList(string Id, Dictionary<int, int> accs)
        {
            // Attempt to fetch the object
            //var p = ds.POrders.SingleOrDefault(o => o.Id == Id);
            var p = POrderGetByCustId(Id);
            if (p == null) { return false; }

            p.IdAccList.Clear();
            p.IdAccList = accs;

            ds.SaveChanges();
            return true;
        }

        public POrderBase POrderEditForm(POrderEditInfo poorder)
        {
            // Attempt to fetch the object
            var p = ds.POrders.Include("AllProducts").SingleOrDefault(o => o.Id == poorder.Id);

            if (p == null)
            {
                return null;
            }
            else
            {
                ds.Entry(p).CurrentValues.SetValues(poorder);
                ds.SaveChanges();
                return mapper.Map<POrderBase>(p);
            }
        }

        // ProductEditAccessory - receives parent SKU number and accessory ID, adds accessory to parent product
        // pSKU - Parent SKU number, user to find parent SKU
        // id - ID of accessory
        public void ProductEditAccessory(string pSKU, int id)
        {

            var p = ds.Products.Include("AllAccessories").SingleOrDefault(o => o.MFG_SKU == pSKU);
            if (p == null) { return; }

            var a = ds.Accessories.Find(id);

            if (!p.AllAccessories.Contains(a))
            {
                p.AllAccessories.Add(a);
            }
            try
            {
                ds.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry
                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages
                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occured in {1} at {2}",
                            subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public PackageBase PackageEditInfo(PackageEditInfo newItem)
        {
            // Attempt to fetch the object
            //var o = ds.Packages.Find(newItem.Id);
            var o = ds.Packages.Include("Products").SingleOrDefault(p => p.Id == newItem.Id);

            if(o == null)
            {
                return null;
            }
            else
            {
                // Update the object with the oncoming values

                // Clear out any existing collection
                o.Products.Clear();

                // Go through the incoming items
                // For each one, add to the fetched object's collection
                foreach (var item in newItem.ProductIds)
                {
                    var p = ds.Products.Find(item);
                    o.Products.Add(p);
                }

                ds.SaveChanges();

                return mapper.Map<PackageWithProducts>(o);
            }
        }

        public ProductBase ProductEditInfo(ProductEditInfo editItem)
        {
            var o = ds.Products.Find(editItem.Id);

            if(o == null)
            {
                return null;
            }
            else
            {
                ds.Entry(o).CurrentValues.SetValues(editItem);
                ds.SaveChanges();

                return mapper.Map<ProductBase>(o);
            }
        }

        public ApplicationUserDetail ApplicationUserEdit(ApplicationUserEdit newItem)
        {
            var result = new IdentityResult();

            // Attempt to fetch the object
            var o = UserManager.FindById(newItem.Id);

            if (o == null)
            {
                return null;
            }

            var userIdentity = UserManager.CreateIdentity(o, DefaultAuthenticationTypes.ApplicationCookie) as ClaimsIdentity;
            var claimsPrincipal = new ClaimsPrincipal(userIdentity);
            var userAccount = new UserAccount(claimsPrincipal);

            // Remove all roles
            foreach (var role in userAccount.RoleClaims)
            {
                result = UserManager.RemoveClaimAsync(o.Id, new Claim(ClaimTypes.Role, role)).Result;
            }

            // If successful removal, Add Roles
            if (result.Succeeded)
            {
                foreach (var newRole in newItem.Roles)
                {
                    result = UserManager.AddClaimAsync(o.Id, new Claim(ClaimTypes.Role, newRole)).Result;
                }
                if (result.Succeeded)
                {
                    return Mapper.Map<ApplicationUserDetail>(newItem);
                }
            }
            return null;
        }


    // Find
    public IEnumerable<ApplicationUserBase> FindUsers(string findString)
        {
            // Fetch all the matching users
            var allUsers = UserManager.Users.Where(e => e.UserName.Contains(findString) || e.Email.Contains(findString));
            if (allUsers == null)
            {
                return null;
            }

            // Map the users to the view model
            var userList = new List<ApplicationUserBase>();
            foreach (var user in allUsers)
            {
                var appUser = mapper.Map<ApplicationUserBase>(user);
                var userClaims = user.Claims.Where(c => c.ClaimType == ClaimTypes.Role).Select(role => role.ClaimValue).ToArray();

                appUser.Roles = userClaims;
                userList.Add(appUser);
            }
            return mapper.Map<IEnumerable<ApplicationUserBase>>(userList);
        }

        public string GetUserRole(string id)
        {
            var user = UserManager.Users.SingleOrDefault(i => i.Id.Equals(id));

            if(user == null)
            {
                return null;
            }

            var appUser = mapper.Map<ApplicationUserBase>(user);

            var userClaims = user.Claims.Where(c => c.ClaimType == ClaimTypes.Role).Select(roles => roles.ClaimValue).ToArray();

            if (userClaims == null || userClaims.Count() == 0)
            {
                string[] str = { "User", "Customer" };
                userClaims = str;
            }

            // Add role claims
            appUser.Roles = userClaims;



            if (appUser.Roles.Contains("Customer"))
            {
                return "Customer";
            }
            else if (appUser.Roles.Contains("Client"))
            {
                return "Client";
            }
            else if(appUser.Roles.Contains("Admin"))
            {
                return "Admin";
            }

            return null;
        }

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                ds.RoleClaims.Add(new RoleClaim { Name = "Customer" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Client" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Admin" });

                ds.SaveChanges();
                done = true;
            }

            if (ds.Products.Count() == 0)
            {

                var path = HttpContext.Current.Server.MapPath("~/App_Data/Catalogue.xlsx");

                // using System.IO for File methods
                var stream = File.Open(path, FileMode.Open, FileAccess.Read);

                // install-package exceldatareader
                // Do NOT update ExcelDataReader, leave it at v2.1.2.3, updating it breaks code, have not found a way to fix it. This version works the way we want it to
                IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                reader.IsFirstRowAsColumnNames = true;
                DataSet sourceData = reader.AsDataSet();
                reader.Close();

                // sourceData holds all the data from xlsx in memory

                // worksheet name
                string worksheetName = "Products";
                var worksheet = sourceData.Tables[worksheetName];

                // Convert it to a collection of the desired type
                List<ProductAdd> items = worksheet.DataTableToList<ProductAdd>();           // DataTableToList -> helper class

                // Go through the collection, and add the items to the data context
                foreach (var item in items)
                {
                    ds.Products.Add(mapper.Map<Product>(item));
                }

                ds.SaveChanges();

                // worksheet name
                worksheetName = "Accessories";
                worksheet = sourceData.Tables[worksheetName];

                List<AccessoriesAdd> accs = worksheet.DataTableToList<AccessoriesAdd>();    // DataTableToList -> helper class

                foreach (var acc in accs)
                {

                    var temp = mapper.Map<Accessories>(acc);
                    ds.Accessories.Add(temp);
                    string[] parent = acc.Parent_SKU.Split(',');

                    foreach (var p in parent)
                    {
                        //ProductEditAccessory(p, acc.SKU);
                        ProductEditAccessory(p, temp.Id);
                    }
                }

                ds.SaveChanges();
                done = true;
            }


            if (ds.Packages.Count() == 0)
            {
                ds.Packages.Add(new Package
                {
                    Name = "Package 1 (default)",
                    ImgURL = "https://www.allegricarlo.com/wp-content/uploads/2015/12/absolute-grey-1-big1.png",
                    Products = new List<Product>
                    {
                        ds.Products.SingleOrDefault(a => a.MFG_SKU == "CBHIC127NAT"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "CBHIC127ALM"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "CBOAK1271201"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "EPO98005"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "PRE8735OA"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "EPO89121"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "GRA04430"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SL196WT01"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SL196FM09"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SUP8156")
                    }
                });

                ds.Packages.Add(new Package
                {
                    Name = "Package 2",
                    ImgURL = "http://birnbachsuccesssolutions.com/wp-content/uploads/an-open-office-layout-involves-distinct-benefits-as-well-as-challeng_16000916_40006742_0_14088367_300.jpg",
                    Products = new List<Product>
                    {
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "CBOAK1271203"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "PRE6908NL"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "EP89001"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SUP8155"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SUP8156"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SUP8158"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SL196FM09"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SUP8157"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SL196WT01"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "CBHIC127COC"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SL196TM10"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SL196TM09"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "CBHIC127NAT"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SL196EH06"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SL196EH07"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SL196EH05"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SL196SC02"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "GRA07529")
                    }

                });

                ds.Packages.Add(new Package
                {
                    Name = "Package 3",
                    ImgURL = "http://homecrack.com/wp-content/uploads/2016/09/beautiful-interior-home-designs-on-1200x675-beautiful-design-image-home-interior-beautiful-design-image-home-1024x576.jpg",
                    Products = new List<Product>
                    {
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "CBOAK1271203"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "PRE6908NL"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "CBHIC127GOT"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "CBOAK1271202"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "CBOAK1271203"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SUP8155"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SUP5535"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "EP89037"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "CBWAL127AM"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "GRA07523"),
                    }
                });

                ds.Packages.Add(new Package
                {
                    Name = "Package 4",
                    ImgURL = "http://www.roomsketcher.com/wp-content/uploads/2015/01/Interior_Design1.jpg",
                    Products = new List<Product>
                    {
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "GRA07523"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "PRE6908NL"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "EP89037"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "SL196FM09"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "PRE8726OA"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "EP89002"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "EP89001"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "CBOAK1271203"),
                        ds.Products.SingleOrDefault(a=>a.MFG_SKU == "CBWAL127AM"),
                    }
                });


                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool NewData()
        {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/Catalogue.xlsx");

            // using System.IO for File methods
            var stream = File.Open(path, FileMode.Open, FileAccess.Read);

            // install-package exceldatareader
            // Do NOT update ExcelDataReader, leave it at v2.1.2.3, updating it breaks code, have not found a way to fix it. This version works the way we want it to
            IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            reader.IsFirstRowAsColumnNames = true;
            DataSet sourceData = reader.AsDataSet();
            reader.Close();

            // sourceData holds all the data from xlsx in memory

            // worksheet name
            string worksheetName = "New";
            var worksheet = sourceData.Tables[worksheetName];

            // Convert it to a collection of the desired type
            List<ProductAdd> items = worksheet.DataTableToList<ProductAdd>();       // DataTableToList -> helper class

            // Go through the collection, and add the items to the data context
            foreach (var item in items)
            {
                ds.Products.Add(mapper.Map<Product>(item));
            }

            try
            {
                ds.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry
                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages
                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occured in {1} at {2}",
                            subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }
                }
            }

            // worksheet name
            worksheetName = "New_Accessories";
            worksheet = sourceData.Tables[worksheetName];

            List<AccessoriesAdd> accs = worksheet.DataTableToList<AccessoriesAdd>();    // DataTableToList -> helper class

            foreach (var acc in accs)
            {

                var temp = mapper.Map<Accessories>(acc);
                ds.Accessories.Add(temp);
                string[] parent = acc.Parent_SKU.Split(',');

                foreach (var p in parent)
                {
                    //ProductEditAccessory(p, acc.SKU);
                    ProductEditAccessory(p, temp.Id);
                }
            }

            ds.SaveChanges();

            return true;
        }

        public bool RemoveData()
        {
            try
            {
                //foreach (var e in ds.RoleClaims)
                //{
                //    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                //}


                foreach (var p in ds.Products)
                {
                    ds.Entry(p).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var p in ds.Packages)
                {
                    ds.Entry(p).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

    public class UserAccount
    {
        // Constructor, pass in the security principal
        public UserAccount(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public int Suite { get; set; }
        public string Plan { get; set; }

        public bool IsAuthenticated { get; private set; }

        // Add other role-checking properties here as needed
        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }

    }

    public static class Helper
    {
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }
    } // public static class Helper

}