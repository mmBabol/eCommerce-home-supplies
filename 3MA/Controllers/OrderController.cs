using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _3MA.Extensions;
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNet.Identity;

using System.Net;
using System.Net.Mail;

namespace _3MA.Controllers
{
    public class OrderController : Controller
    {
        Manager m = new Manager();

        // GET: Order
        public ActionResult Index()
        {
            TempData["OrdersList"] = new List<int>();
            return View(m.POrderGetAll());
        }

        public ActionResult getEndUser()
        {
            //var suites = m.getAllSuites();
            var order = m.getAllOrders();

            ViewBag.SuiteID = 0;
            TempData["AllOrders"] = order;
            TempData["SuiteID"] = 0;
            //TempData["OrderID"] = 0;

            //return View(suites);
            return View(order);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            var form = new OrderAddForm();
            form.OrderWithPackageList = m.PackageWithProductsGetAll().ToList();

            return View(form);
        }

        // Order Packages - All packages  -------------------------------------
        // POST: Order/Create
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Create(OrderAdd newItem)
        {
            if(newItem==null)
            {
                return View(newItem);
            }

            return RedirectToAction("Order", newItem);
        }

        public ActionResult Order(OrderAdd order)
        {
            // TODO if order == null
            order.OrderWithPackage = m.PackageGetById(order.PackageId);
            return View(order);
        }

        // ATTENTION - Create the Excel file using ClosedXML
        //[ActionName("create_order")]
        public ActionResult Accept(OrderAdd order)
        {
            string date = DateTime.Now.ToString("dd_MM_yyyy");

            string name = order.Suite.ToString() + "_" + date + ".xlsx";

            order.OrderWithPackage = m.PackageGetById(order.PackageId);

            Stream fs = new MemoryStream();
            XLWorkbook workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Purchase Order");

            int row = 5;

            // Format shheet
            ws.Column(1).Width = 5;
            ws.Column(10).Width = 5;

            // Title
            var title = ws.Range("B2:H3").Merge();
            title.Style.Font.FontSize = 18;
            title.Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            title.Value = "Purchase Order";

            // Populate the excel spreadsheet

            // Fill out customer information
            ws.Cell(2, 9).Value = DateTime.Now.ToString("dd/MM/yyyy");

            ws.Cell(row, 2).Value = "Suite";
            ws.Cell(row, 2).Style.Font.SetBold();
            ws.Cell(row, 8).Value = order.Suite;
            ws.Cell(row++, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            ws.Cell(row, 2).Value = "Name";
            ws.Cell(row, 2).Style.Font.SetBold();
            ws.Cell(row, 8).Value = "name goes here";
            ws.Cell(row++, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            ws.Cell(row, 2).Value = "Address";
            ws.Cell(row, 2).Style.Font.SetBold();
            ws.Cell(row, 8).Value = "address goes here";
            ws.Cell(row++, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            ws.Cell(row, 2).Value = "Phone";
            ws.Cell(row, 2).Style.Font.SetBold();
            ws.Cell(row, 8).Value = "phone # goes here";
            ws.Cell(row++, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            row++;row++;

            ws.Cell(row, 2).Value = "Lightin upgrades";
            ws.Cell(row, 2).Style.Font.SetBold();
            ws.Cell(row, 8).SetValue((order.LightUpgrade) ? "Yes" : "No");
            ws.Cell(row++, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            row++; row++;

            var prod_header = ws.Range(row, 2, row, 9);
            prod_header.Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.AirForceBlue).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            ws.Cell(row, 2).Value = "Product";
            ws.Cell(row, 5).Value = "Type";
            ws.Cell(row, 8).Value = "Price";

            row++;

            foreach(var prod in order.OrderWithPackage.Products)
            {
                ws.Cell(row, 2).Value = prod.Name;
                ws.Cell(row, 8).Value = prod.Price;

                row++;
            }


            workbook.SaveAs(fs);
            fs.Position = 0;
            //worksheet.Cell("A1").Value = "Hello World!";
            //workbook.SaveAs("HelloWorld.xlsx");
            return new FileStreamResult(fs, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = name };
        }


        // Order Products - All products------------------------------------
        // GET: Order/PCreate
        //[ActionName("Create")]
        public ActionResult PCreate()
        {
            var form = new POrderAddForm();
            form.AllProducts = m.ProductGetAll().ToList();

            return View(form);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateInput(true)]
        //[ActionName("Create")]
        public ActionResult PCreate(POrderAdd newItem)
        {
            if (newItem == null)
            {
                return View(newItem);
            }

            POrderAddForm newOrder = new POrderAddForm();
            newOrder.LightUpgrade = newItem.LightUpgrade;
            newOrder.Suite = newItem.Suite;
            foreach (var o in newItem.IdProductList)
            {
                newOrder.AllProducts.Add(m.ProductGetById(o));
                newOrder.IdProductList.Add(o);
            }

            TempData["Order"] = newOrder;

            return View("POrder", newOrder);
        }

        //[ActionName("create_order")]
        public ActionResult PAccept(POrderAdd order)
        {

            POrderAddForm NO = TempData["Order"] as POrderAddForm;

            string date = DateTime.Now.ToString("dd_MM_yyyy");

            string name = order.Suite.ToString() + "_" + date + ".xlsx";

            //order.OrderWithPackage = m.PackageGetById(order.PackageId);

            Stream fs = new MemoryStream();
            XLWorkbook workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Purchase Order");

            workbook.SaveAs(fs);
            fs.Position = 0;
            //worksheet.Cell("A1").Value = "Hello World!";
            //workbook.SaveAs("HelloWorld.xlsx");
            return new FileStreamResult(fs, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = name };
        }



        // Order Products - searchable  ----------------------------------
        [Route("User_Order/{id}")]
        public ActionResult User_Order(string id)
        {
            var all = TempData["AllOrders"] as Dictionary<string, Tuple<string, bool>>;
            string custId = "";

            if (all == null)
            {
                return null;
            }
            foreach(var a in all)
            {
                if (a.Value.Item1.Equals(id))
                {
                    custId = a.Key;
                    break;
                }
            }

            if (custId == null)
            {
                TempData["AllOrders"] = all;
                return Content("<h3>Error - cannot open profile. Please contact the programmer for support.</h3>");
            }

            TempData["OrderID"] = custId;
            //TempData["SuiteID"] = id;
            //Products_Order();
            return RedirectToAction("Products_Order", "Order");
        }

        public ActionResult Products_Order()
        {
            string id;
            bool admin = (TempData["OrderID"] == null) ? false : true;

            if (admin)
            {
                id = (string)TempData["OrderID"];

                if (id.Equals(User.Identity.GetUserId()))
                {
                    id = User.Identity.GetUserId();
                    admin = false;
                }
            }
            else
            {
                // get current users suite number - obsolite method of order
                //id = User.Identity.GetCustomerSuite();
                id = User.Identity.GetUserId();
            }

            var form = getPOrder(id);

            //TempData["ProductIdsList"] = new List<int>();
            TempData["ProductIdsList"] = form.IdProductList;
            TempData["ProductsQtyList"] = form.Qty;
            TempData["ProductsLocList"] = form.Room;
            TempData["OrderID"] = id;
            //TempData["Suite"] = id;

            if (admin)
            {
                return PartialView("Products_Order", form);
            }
            else
            {
                return View(form);
            }
        }

        private POrderBase getPOrder(string i)
        {
            var form = m.POrderGetByCustId(i);
            foreach (var item in form.AllProducts)
            {
                form.IdProductList.Add(item.Id);
            }
            form.AllProducts.Clear();

            return form;
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Products_Order(POrderBase order)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Products_Order", order);
            }

            order.IdProductList = TempData["ProductIdsList"] as List<int>;
            order.Qty = TempData["ProductsQtyList"] as Dictionary<int, int>;
            order.Room = TempData["ProductsLocList"] as Dictionary<int, string>;

            var updated_order = m.POrderEdit(order);        //HERE FIX

            if(updated_order == null)
            {
                return RedirectToAction("Products_Order", m.POrderGetBySuite(order.Id));
            }
            else
            {
                //m.POrderEdit(order);
                return View("Order_review", updated_order);
            }
        }

        public ActionResult Order_Accept(int Id)
        {
            // var order = m.POrderGetBySuite(suite);

            var order = m.POrderGetById(Id);

            order.Completed = true;
            order.OrderPlaced = DateTime.Now;

            foreach (var item in order.AllProducts)
            {
                order.IdProductList.Add(item.Id);
            }

            m.POrderEdit(order);


            return View("Complete");
        }

        // ProductListSave - On product image click, select / deselect the product
        // id - Id of product selected / deselected
        [HttpPost]
        public ActionResult ProductListSave(int id)
        {
            List<int> ints = TempData["ProductIdsList"] as List<int>;

            if (ints.Contains(id))
            {
                ints.Remove(id);
            }
            else
            {
                ints.Add(id);
            }

            TempData["ProductIdsList"] = ints;

            return null;
        }

        // AccListSave - On accessory image click, select / deselect the accessory
        // id - Id of accessory selected / deselected
        // item - Id of the product that the accessory belongs to
        [HttpPost]
        public ActionResult AccListSave(int id, int item)
        {
            string po = TempData["OrderID"] as string;
            TempData["OrderID"] = po;
            //Dictionary<int, int> ints = TempData["AccsIdsList"] as Dictionary<int, int>;
            Dictionary<int, int> ints = m.POrderAccessoriesList(po);
            if(ints == null) { return null; }
            bool addMe = true;

            foreach(var a in ints)
            {
                if (a.Key == id && a.Value == item)
                {
                    ints.Remove(id);
                    addMe = false;
                    break;
                }
            }
            if (addMe)
            {
                ints.Add(id, item);
            }

            if (m.POrderAccList(po, ints) == false)
            {
                Console.WriteLine("Could not save accessory list.");
            }

            //TempData["AccsIdsList"] = ints;

            return null;
        }

        // ProductQtySave
        // receive the product id and quantity, add it into TempData
        // id - the ID of the product
        // qty - quantity of this product
        [HttpPost]
        public ActionResult ProductQtySave_old(int id, int qty)
        {
            Dictionary<int, int> qtys= TempData["ProductsQtyList"] as Dictionary<int, int>;
            int dict;
            if (!qtys.TryGetValue(id, out dict))
            {
                qtys.Add(id, qty);
            }
            else
            {
                qtys[id] = qty;
            }

            TempData["ProductsQtyList"] = qtys;

            return null;
        }

        // ProductLocSave
        // receive the product id and room locations, add it into TempData
        // id - the ID of the product
        // loc - room location(s)
        [HttpPost]
        public ActionResult ProductLocSave(int id, string loc)
        {
            Dictionary<int, string> locs = TempData["ProductsLocList"] as Dictionary<int, string>;
            string dict;
            if (!locs.TryGetValue(id, out dict))
            {
                locs.Add(id, loc);
            }
            else
            {
                locs[id] = loc;
            }

            TempData["ProductsLocList"] = locs;

            return null;
        }

        // V1.0

        // Partials views foreach category
        [Route("Paint_order/")]
        public ActionResult Paint()
        {
            return PartialView("_Paint", new ProductSearchForm());
        }

        [Route("Millwork_order/")]
        public ActionResult Millwork()
        {
            return PartialView("_Flooring", new ProductSearchForm());
        }

        [Route("Tiles_order/")]
        public ActionResult Tiles()
        {
            return PartialView("_Flooring", new ProductSearchForm());
        }

        [Route("Flooring_order/")]
        public ActionResult Flooring()
        {
            var search = new ProductSearchForm();
            //search.dimX = new List<string> { "inches", "millimeters", "3-1/2\"", "5\"", "5-7/8\"", "6\"", "6 3/8\"", "7-1/2\"", "8-1/2\"", "123mm", "150mm", "157mm", "165mm", "192mm", "196mm"};
            //search.dimY = new List<string> { "inches", "millimeters", "1/2\"", "3/4\"", "5/8\"", "9/16\"", "10mm", "12mm", "18mm"};
            //search.dimZ = new List<string> { "rl", "1218mm", "1285mm", "1835mm", "25% 73\"", "up to 73\"", "up to 86\""};
            //search.PriceCat = new List<string> { "A", "B", "C", "D", "E" };

            search.dimX = m.getAllDimX("Flooring");
            search.dimY = m.getAllDimY("Flooring");
            search.dimZ = m.getAllDimZ("Flooring");
            search.PriceCat = m.getAllPriceCat("Flooring");

            return PartialView("_Flooring", search);
        }

        [Route("Lighting_order/")]
        public ActionResult Lighting()
        {
            return PartialView("_Flooring", new ProductSearchForm());
        }

        [Route("Door_order/")]
        public ActionResult Door()
        {
            return PartialView("_Flooring", new ProductSearchForm());
        }

        [Route("Plumbing_order/")]
        public ActionResult Plumbing()
        {
            return PartialView("_Flooring", new ProductSearchForm());
        }

        [Route("Appliances_order/")]
        public ActionResult Appliances()
        {
            return PartialView("_Flooring", new ProductSearchForm());
        }

        // ProductSub - receives main and sub category, and returns all products under that sub category
        // cat - main category
        // sub - sub category
        // return - partial view '_ProductList', with an POrder object containing all the products
        [Route("order/sub/{cat}/{sub}")]
        public ActionResult ProductSub(string cat, string sub)
        {
            string id = TempData["OrderID"] as string;

            var form = m.POrderGetByCustId(id);

            form.AllProducts = m.ProductSearch(cat, sub).ToList();

            //TempData["Suite"] = id;
            TempData["OrderID"] = id;

            List<int> ints = TempData["ProductIdsList"] as List<int>;
            form.IdProductList = ints;
            TempData["ProductIdsList"] = ints;

            Dictionary<int, int> qty = TempData["ProductsQtyList"] as Dictionary<int, int>;
            form.Qty = qty;
            TempData["ProductsQtyList"] = qty;

            Dictionary<int, string> room = TempData["ProductsLocList"] as Dictionary<int, string>;
            form.Room = room;
            TempData["ProductsLocList"] = room;

            //foreach(var a in form.AllAccessories)
            //{
            //    if(ints.Contains())
            //    form.IdAccList.Add(a.)
            //}

            if (form.AllProducts.Count == 0 || form == null)
            {
                //return Content("<h3>No results found.</h3>", "text/html");
                return PartialView("NotFound");
            }

            return PartialView("_ProductList", form);
        }

        // ProductSearch - Search for specific products and return list of the results
        // cat - main category
        // sub - sub category
        // nam - name of search
        // x - x dimension of search
        // y - y dimension of search
        // z - z dimension of search
        // price - price of result
        // return - parial view '_ProductList, with an POrder object containing all products
        [Route("order/search/{cat}/{col}/{nam}/{x}/{y}/{z}/{price}")]
        public ActionResult ProductSearch(string cat, string col, string nam, string x, string y, string z, string price)
        {
            //int id = (int)TempData["Suite"];
            string id = TempData["OrderID"] as string;

            var form = m.POrderGetByCustId(id);

            List<string> filterCat = new List<string>();
            form.AllProducts = m.ProductSearchForm(cat, col, nam, x, y, z, price, filterCat).ToList();

            //TempData["Suite"] = id;
            TempData["OrderID"] = id;

            List<int> ints = TempData["ProductIdsList"] as List<int>;
            form.IdProductList = ints;
            TempData["ProductIdsList"] = ints;

            Dictionary<int, int> qty = TempData["ProductsQtyList"] as Dictionary<int, int>;
            form.Qty = qty;
            TempData["ProductsQtyList"] = qty;

            Dictionary<int, string> room = TempData["ProductsLocList"] as Dictionary<int, string>;
            form.Room = room;
            TempData["ProductsLocList"] = room;

            if (form.AllProducts.Count == 0 || form == null)
            {
                //return Content("<h3>No results found.</h3>", "text/html");
                return PartialView("NotFound");
            }

            return PartialView("_ProductList", form);
        }

        [HttpPost]
        public JsonResult AjaxMethod(string name)
        {
            return Json(new POrderBase());
        }

        // Save the excel file on a local machine
        public Stream GetStream(XLWorkbook excelWorkbook)
        {
            Stream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }

        [HttpPost]
        public void OrderListSave(int id)
        {
            List<int> ints = TempData["OrdersList"] as List<int>;

            if (ints.Contains(id))
            {
                ints.Remove(id);
            }
            else
            {
                ints.Add(id);
            }

            TempData["OrdersList"] = ints;
        }


        // V2.0

        public ActionResult Home()
        {
            return View();
        }

        // Flooring_Main - Open flooring search page. This is V2.0+
        [Route("Flooring/")]
        public ActionResult Flooring_Main()
        {
            var search = new ProductSearchForm();

            search.dimX = m.getAllDimX("Flooring");
            search.dimY = m.getAllDimY("Flooring");
            search.dimZ = m.getAllDimZ("Flooring");
            search.PriceCat = m.getAllPriceCat("Flooring");

            search.Filter.Add("All", true);
            search.Filter.Add("Hardwood", false);
            search.Filter.Add("Laminate", false);

            return View("Flooring", search);
        }

        // Search - Search for specific products and return list of the results
        // cat - main category
        // sub - sub category
        // nam - name of search
        // x - x dimension of search
        // y - y dimension of search
        // z - z dimension of search
        // price - price of result
        // a -
        // b -
        // c -
        // d -
        // e -
        // return - parial view '_ProductList, with an POrder object containing all products
        [Route("search/{cat}/{col}/{nam}/{x}/{y}/{z}/{price}/{a}/{b}/{c}/{d}/{e}")]
        public ActionResult Search(string cat, string col, string nam, string x, string y, string z, string price, string a, string b, string c, string d, string e)
        {
            List<string> filterCat = new List<string>();
            if (a != "0") { filterCat.Add(a); }
            if (b != "0") { filterCat.Add(b); }
            if (c != "0") { filterCat.Add(c); }
            if (d != "0") { filterCat.Add(d); }
            if (e != "0") { filterCat.Add(e); }

            if (filterCat.Count() == 0)
            {
                return PartialView("NotFound");
            }

            var products = m.ProductSearchForm(cat, col, nam, x, y, z, price, filterCat).ToList();

            if (products.Count == 0 || products == null)
            {
                //return Content("<h3>No results found.</h3>", "text/html");
                return PartialView("NotFound");
            }

            return PartialView("_Search", products);
        }

        // Select - Product details, user is able to add/remove the product from the shopping card
        // id - The Id of the selected product, used to grab the product from the model
        public ActionResult Select(int? id)
        {
            var o = m.ProductGetById(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }

            var order = m.POrderGetByCustId(User.Identity.GetUserId());

            if (order == null)
            {
                return HttpNotFound();
            }

            bool isChecked = false;
            int quantity = 0;
            foreach(var product in order.AllProducts)
            {
                if(product.Id == id)
                {
                    isChecked = true;
                    break;
                }
            }

            if (isChecked)
            {
                foreach(var qty in order.Qty)
                {
                    if(qty.Key == id)
                    {
                        quantity = qty.Value;
                    }
                }
            }

            ViewData["isChecked"] = isChecked;
            ViewData["Qty"] = quantity;

            return View(o);
        }

        // ProductQtySave
        // receive the product id and quantity, add it into TempData
        // id - the ID of the product
        // qty - quantity of this product
        [HttpPost]
        public ActionResult ProductQtySave(int id, int qty)
        {
            var order = m.POrderGetByCustId(User.Identity.GetUserId());
            int dict;

            // Substitute for ContainsKey, looks for key [id], if not found, it will output [dict]
            if (!order.Qty.TryGetValue(id, out dict))
            {
                order.Qty.Add(id, qty);
            }
            else
            {
                order.Qty[id] = qty;
            }

            var result = m.POrderUpdateQtys(order);

            return null;
        }

        [HttpPost]
        public ActionResult AddToCart(int id, bool isChecked)
        {
            var product = m.ProductGetById(id);

            var result = m.POrderUpdateCart(User.Identity.GetUserId(), product, isChecked); 

            return null;
        }

        public ActionResult CreatePO()
        {
            var all = m.POrderSearch(TempData["OrdersList"] as List<int>);

            string date = DateTime.Now.ToString("dd_MM_yyyy");

            string name = "Purchase_Orders_" + date + ".xlsx";

            Stream fs = new MemoryStream();
            XLWorkbook workbook = new XLWorkbook();

            foreach (var order in all)
            {
                var ws = workbook.Worksheets.Add(order.Suite.ToString());

                // Format shheet
                ws.Column(1).Width = 5;
                ws.Column(2).Width = 10;
                ws.Column(3).Width = 10;
                ws.Column(4).Width = 10;
                ws.Column(5).Width = 10;
                ws.Column(6).Width = 10;
                ws.Column(7).Width = 10;
                ws.Column(8).Width = 10;
                ws.Column(9).Width = 10;
                ws.Column(10).Width = 10;
                ws.Column(11).Width = 14;

                ws.Cell(1, 1).Value = DateTime.Now.ToString("dd/MM/yyyy");

                // Title
                var title = ws.Range("G1:K2").Merge();
                title.Style.Font.FontSize = 20;
                title.Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right).Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                title.Value = "PURCHASE ORDER";

                // TODO: client information in excel, save in database later and print dynamically?
                // Fill out company information
                var co = ws.Range("A3:C3").Merge();
                co.Style.Font.FontSize = 12;
                co.Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                co.Value = "3MA";

                co = ws.Range("A4:C4").Merge();
                co.Style.Font.FontSize = 12;
                co.Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                co.Value = "79 Kincort Street";

                co = ws.Range("A5:C5").Merge();
                co.Style.Font.FontSize = 12;
                co.Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                co.Value = "Toronto, Ontario";

                co = ws.Range("A6:C6").Merge();
                co.Style.Font.FontSize = 12;
                co.Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                co.Value = "M6M 5G7";

                co = ws.Range("A7:C7").Merge();
                co.Style.Font.FontSize = 12;
                co.Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                co.Value = "416-561-7724";

                var po = ws.Range("H4:I4").Merge();
                po.Style.Font.FontSize = 12;
                po.Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                po.Value = "PO number";

                po = ws.Range("J4:K4").Merge();
                po.Style.Font.FontSize = 12;
                po.Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                po.Value = "PO DATE";

                po = ws.Range("H5:I5").Merge();
                po.Style.Font.FontSize = 11;
                po.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                po.Value = "PO goes here";

                po = ws.Range("J5:K5").Merge();
                po.Style.Font.FontSize = 11;
                po.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                po.Value = order.OrderPlaced.ToString("dd/MM/yyyy");


                // Populate the excel spreadsheet
                var ship = ws.Range("A10:E10").Merge();
                ship.Style.Font.FontSize = 12;
                ship.Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Fill.SetBackgroundColor(XLColor.LightGreen);
                ship.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ship.Value = "SHIP TO";

                ws.Range("A11:B11").Merge();
                ws.Cell("A11").Value = "Name";
                ws.Cell("A11").Style.Font.SetBold();
                ws.Range("C11:E11").Merge();
                ws.Cell("C11").Value = order.Name;
                ws.Cell("C11").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                ws.Range("A12:B12").Merge();
                ws.Cell("A12").Value = "Suite";
                ws.Cell("A12").Style.Font.SetBold();
                ws.Range("C12:E12").Merge();
                ws.Cell("C12").Value = order.Suite;
                ws.Cell("C12").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                ws.Range("A13:B13").Merge();
                ws.Cell("A13").Value = "Address";
                ws.Cell("A13").Style.Font.SetBold();
                ws.Range("C13:E13").Merge();
                ws.Cell("C13").Value = "Street address will go here";
                ws.Cell("C13").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                ws.Range("C14:E14").Merge();
                ws.Cell("C14").Value = "City, Province, Postal will go here";
                ws.Cell("C14").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                ws.Range("G11:H11").Merge();
                ws.Cell("G11").Value = "Move in Date";
                ws.Cell("G11").Style.Font.SetBold();
                ws.Range("I11:K11").Merge();
                ws.Cell("I11").Value = order.MoveIn;
                ws.Cell("I11").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                ws.Range("G12:H12").Merge();
                ws.Cell("G12").Value = "Floor plan";
                ws.Cell("G12").Style.Font.SetBold();
                ws.Range("I12:K12").Merge();
                ws.Cell("I12").Value = order.Plan;
                ws.Cell("I12").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                ws.Range("G13:H13").Merge();
                ws.Cell("G13").Value = "Home phone";
                ws.Cell("G13").Style.Font.SetBold();
                ws.Range("I13:K13").Merge();
                ws.Cell("I13").Value = order.getHPhone;
                ws.Cell("I13").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                ws.Range("G14:H14").Merge();
                ws.Cell("G14").Value = "Mobile phone";
                ws.Cell("G14").Style.Font.SetBold();
                ws.Range("I14:K14").Merge();
                ws.Cell("I14").Value = order.getMPhone;
                ws.Cell("I14").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                var ups = ws.Range("A16:B16").Merge();
                ups.Style.Font.FontSize = 12;
                ups.Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Fill.SetBackgroundColor(XLColor.LightGreen);
                ups.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ups.Value = "UPGRADES";

                ws.Cell("D16").Value = "Light";
                ws.Cell("D16").Style.Font.SetBold();
                ws.Cell("E16").Value = (order.LightUpgrade) ? "Yes" : "No";

                // Products
                var head = ws.Range("B18:C18").Merge();
                head.Value = "Product ID";

                head = ws.Range("D18:F18").Merge();
                head.Value = "Name";
                ws.Cell("G18").Value = "Category";
                ws.Cell("H18").Value = "Price";
                ws.Cell("I18").Value = "Weight";
                ws.Cell("J18").Value = "SqFt";
                ws.Cell("K18").Value = "Price Per SqFt";

                head = ws.Range("A18:K18");
                head.Style.Font.FontSize = 11;
                head.Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Fill.SetBackgroundColor(XLColor.LightGreen);
                head.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                int row = 19, counter = 0; ;
                double total = 0;

                foreach (var prod in order.AllProducts)
                {
                    ws.Cell(row, 1).Value = ++counter;
                    ws.Range(row, 2, row, 3).Merge();
                    ws.Cell(row, 2).Value = prod.MFG_SKU;
                    ws.Range(row, 4, row, 6).Merge();
                    ws.Cell(row, 4).Value = prod.Name;
                    ws.Cell(row, 7).Value = prod.MainCategory;
                    ws.Cell(row, 8).Value = Math.Round(prod.Price, 2);
                    ws.Cell(row, 9).Value = prod.Lbs;
                    ws.Cell(row, 10).Value = prod.SqFt;
                    ws.Cell(row, 11).Value = Math.Round(prod.PricePerSF, 2);
                    ws.Range(row, 1, row, 11).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    ws.Range(row, 1, row, 11).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Range(row, 1, row, 11).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    row++;
                    total += prod.Price;
                }
                ws.Cell(row, 10).Value = "Total Cost:";
                ws.Cell(row, 10).Style.Font.Bold = true;

                ws.Cell(row, 11).Value = Math.Round(total, 2);
                ws.Cell(row, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            }


            workbook.SaveAs(fs);
            fs.Position = 0;
            //worksheet.Cell("A1").Value = "Hello World!";
            //workbook.SaveAs("HelloWorld.xlsx");
            return new FileStreamResult(fs, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = name };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add("mmbabol@gmail.com");
                mailMessage.From = new MailAddress("mmbabol@gmail.com");
                mailMessage.Subject = "ASP.NET e-mail test";
                mailMessage.Body = "Hello world,\n\nThis is an ASP.NET test e-mail!";
                SmtpClient smtpClient = new SmtpClient("smtp.your-isp.com");
                smtpClient.Send(mailMessage);
                Response.Write("E-mail sent!");
            }
            catch (Exception ex)
            {
                Response.Write("Could not send the e-mail - error: " + ex.Message);
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit()
        {
            var id = User.Identity.GetUserId();
            //int id = User.Identity.GetCustomerSuite();
            var o = m.POrderGetByCustId(id);

            if(o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var edited = m.mapper.Map<POrderEditForm>(o);
                return View(edited);
            }
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, POrderEditInfo editItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("edit", new { id = editItem.Id });
            }
            if(id.GetValueOrDefault() != editItem.Id)
            {
                return RedirectToAction("Products_Order");
            }

            var edited = m.POrderEditForm(editItem);

            if(edited == null)
            {
                return RedirectToAction("edit", new { id = editItem.Id });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            var orderDel = m.POrderGetById(id.GetValueOrDefault());

            if(orderDel == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(orderDel);
            }
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            string customerID = m.POrderGetID(id.GetValueOrDefault());
            var result = m.POrderDelete(id.GetValueOrDefault());

            try
            {
                m.DeleteUser(customerID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("Index");
        }
    }
}
