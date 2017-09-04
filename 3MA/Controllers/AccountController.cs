using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using _3MA.Models;
using System.Collections.Generic;

namespace _3MA.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // New "Index" method in the account controller
        // Study the Views/Account/Index.cshtml view code too
        // GET: /Account
        [AllowAnonymous]
        public ActionResult Index()
        {
            // Users, names only

            // Get all users
            var allUsers = UserManager.Users;

            // Fetch the user names
            string userNames = "";
            foreach (var user in allUsers)
            {
                userNames += "<br />" + user.UserName;
            }

            // Add this data to the ViewBag
            ViewBag.UserNames = userNames;

            // Claims, raw view

            // Is authenticated?
            string status = Request.IsAuthenticated
                ? User.Identity.Name + " is authenticated"
                : "Anonymous";

            // Fetch the claims
            if (Request.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                var claims = identity.Claims.Select(c => new { Type = c.Type, Value = c.Value }).AsEnumerable();
                foreach (var claim in claims)
                {
                    status += "<br />" + claim.Type + " = " + claim.Value;
                }
            }

            // Add this data to the ViewBag
            ViewBag.Status = status;

            return View();
        }

        // ############################################################
        // New account details action/method
        // Uses a new AccountDetails view model class
        // GET: /Account/Details
        public ActionResult Details()
        {
            // Create a view model object
            var accountDetails = new AccountDetails();

            // Identity object "name" (i.e. not the claim)
            accountDetails.UserName = User.Identity.Name;

            // Work with the current User in claims-compatible mode
            var identity = User.Identity as ClaimsIdentity;

            // Now, go through the claims...

            // Get the name, if present
            var name = identity.FindFirst(ClaimTypes.Name);
            accountDetails.ClaimsName = name == null ? "(none)" : name.Value;

            // Get the given name, if present
            var givenName = identity.FindFirst(ClaimTypes.GivenName);
            accountDetails.ClaimsGivenName = givenName == null ? "(none)" : givenName.Value;

            // Get the surname, if present
            var surname = identity.FindFirst(ClaimTypes.Surname);
            accountDetails.ClaimsSurname = surname == null ? "(none)" : surname.Value;

            // Get the email, if present
            var email = identity.FindFirst(ClaimTypes.Email);
            accountDetails.ClaimsEmail = email == null ? "(none)" : email.Value; 

           // var stuff = identity.FindFirst(ClaimTypes.)

            // Get the roles, if present
            var roles = identity.FindAll(ClaimTypes.Role).Select(rn => rn.Value).ToArray();
            accountDetails.ClaimsRoles = roles.Count() == 0 ? "(none)" : string.Join(", ", roles);

            return View(accountDetails);
        }

        // ############################################################

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public async Task<ActionResult> Login(string returnUrl)
        {
            // Account/Login code was modified
            // The method's signature was also changed to run asynchronously

            if (UserManager.Users.Count() == 0)
            {
                var user = new ApplicationUser { UserName = "admin@3ma.com", Email = "admin@3ma.com" };
                var result = await UserManager.CreateAsync(user, "Password123!");
                // Add claims
                await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Email, "admin@3ma.com"));
                await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "Admin"));
                await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, "Application"));
                await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Surname, "Administrator"));
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                       .Where(y => y.Count > 0)
                       .ToList();

                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register_Type()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("Register/Type/{type}")]
        public ActionResult Type(string type)
        {
            TempData["Type"] = type;

            return RedirectToAction("Register");
        }

        public ActionResult Roles()
        {
            var roles = new List<string> { "Customer", "Client", "Admin" };
            var form = new myRoles();
            form.RoleList = new MultiSelectList(roles);

            return View(form);
        }

        [Route("Account/Select/{role}")]
        public ActionResult Select(string role)
        {
            if(role == "customer")
            {
                //return RedirectToAction("Register_Cust");
                return RedirectToAction("Register_Type_Admin");
            }
            else if(role == "client")
            {
                return RedirectToAction("Register_Client");
            }
            else
            {
                return RedirectToAction("Register_Admin");
            }
        }



        //
        // GET: /Account/Register
        // Used to create new customer profiles only, new user is logged in after registry
        [AllowAnonymous]
        public ActionResult Register()
        {
            Manager m = new Manager();

            ViewData["Type"] = TempData["Type"] as string;
            
            var stuff = m.ProjectGetList();
            ViewData["ProjectList"] = stuff;
            var form = new RegisterViewModelForm();

            return View(form);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            Manager m = new Manager();

            // if Plan is not entered, user sign in fails because it is null. Change to an empty string instead
            if(model.Plan == null) { model.Plan = ""; }

            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Suite = model.Suite, Plan = model.Plan };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Add claims
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Email, model.Email));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, model.GivenName));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Surname, model.Surname));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "User"));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "Customer"));


                    // Log me in after registration is complete

                    try
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    POrderAdd porder = new POrderAdd();

                    porder.HPhone = model.HPhone;
                    porder.MPhone = model.MPhone;
                    porder.Name = model.GivenName + " " + model.Surname;
                    porder.MoveIn = model.MoveIn;
                    porder.Suite = model.Suite;
                    porder.Plan = model.Plan;
                    porder.Street = model.Street;
                    porder.City = model.City;
                    porder.Prov = model.Prov;
                    porder.Country = model.Country;
                    porder.Postal = model.Postal;

                    if (model.isSameAddress)
                    {
                        porder.BillCity = model.City;
                        porder.BillCountry = model.Country;
                        porder.BillPostal = model.Postal;
                        porder.BillProv = model.Prov;
                        porder.BillStreet = model.Street;
                    }
                    else
                    {
                        porder.BillCity = model.BillCity;
                        porder.BillCountry = model.BillCountry;
                        porder.BillPostal = model.BillPostal;
                        porder.BillProv = model.BillProv;
                        porder.BillStreet = model.BillStreet;
                    }

                    if (model.ProjectName != null)
                    {
                        porder.ProjectName = model.ProjectName;
                        porder.ProjectId = m.ProjectGetId(model.ProjectName);
                    }

                    porder.customerID = user.Id;

                    m.POrderAdd(porder);

                    return RedirectToAction("Details", "Account");

                }
                AddErrors(result);
            }

            // Something failed :( redisplay the form

            var form = m.mapper.Map<RegisterViewModelForm>(model);
            var roles = m.RoleClaimGetAllStrings();

            //form.RoleList = new MultiSelectList(
            //    items: roles,
            //    selectedValues: model.Roles);

            if (model.ProjectName != null)
            {
                ViewData["Type"] = "project";
                var stuff = m.ProjectGetList();
                ViewData["ProjectList"] = stuff;
                ViewData["Selected"] = model.ProjectName;
            }

            return View(form);
        }

        public ActionResult Register_Type_Admin()
        {
            return PartialView();
        }

        [Route("Register_Admin/Type/{type}")]
        public ActionResult Type_Admin(string type)
        {
            TempData["Type"] = type;
            
            return RedirectToAction("Register_Cust");
        }

        // Register_Cust
        // Used to create accounts of different types, does not log in newly created account
        public ActionResult Register_Cust()
        {
            Manager m = new Manager();

            ViewData["Type"] = TempData["Type"] as string;
            ViewData["ProjectList"] = m.ProjectGetList();

            // Define a register form
            var form = new RegisterViewModelForm();

            return PartialView(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register_Cust(RegisterViewModel model)
        {
            Manager m = new Manager();

            // if Plan is not entered, user sign in fails because it is null. Change to an empty string instead
            if (model.Plan == null) { model.Plan = ""; }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Suite = model.Suite, Plan = model.Plan };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Add claims
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Email, model.Email));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, model.GivenName));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Surname, model.Surname));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "User"));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "Customer"));

                    POrderAdd porder = new POrderAdd();

                    porder.HPhone = model.HPhone;
                    porder.MPhone = model.MPhone;
                    porder.Name = model.GivenName + " " + model.Surname;
                    porder.MoveIn = model.MoveIn;
                    porder.Suite = model.Suite;
                    porder.Plan = model.Plan;
                    porder.Street = model.Street;
                    porder.City = model.City;
                    porder.Prov = model.Prov;
                    porder.Country = model.Country;
                    porder.Postal = model.Postal;

                    if (model.isSameAddress)
                    {
                        porder.BillCity = model.City;
                        porder.BillCountry = model.Country;
                        porder.BillPostal = model.Postal;
                        porder.BillProv = model.Prov;
                        porder.BillStreet = model.Street;
                    }
                    else { 
                        porder.BillCity = model.BillCity;
                        porder.BillCountry = model.BillCountry;
                        porder.BillPostal = model.BillPostal;
                        porder.BillProv = model.BillProv;
                        porder.BillStreet = model.BillStreet;
                    }

                    if (model.ProjectName != null)
                    {
                        porder.ProjectName = model.ProjectName;
                        porder.ProjectId = m.ProjectGetId(model.ProjectName);
                    }

                    porder.customerID = user.Id;

                    m.POrderAdd(porder);

                    return RedirectToAction("Index", "AccountManager");
                }
                AddErrors(result);
            }

            // Something failed :( redisplay the form

            var form = m.mapper.Map<RegisterViewModelForm>(model);
            var roles = m.RoleClaimGetAllStrings();

            if (model.ProjectName != null)
            {
                ViewData["Type"] = "project";
                var stuff = m.ProjectGetList();
                ViewData["ProjectList"] = stuff;
                ViewData["Selected"] = model.ProjectName;
            }

            return View(form);
        }

        // Register_Cust
        // Used to create accounts of different types, does not log in newly created account
        public ActionResult Register_Client()
        {
            // Define a register form
            var form = new RegisterClientForm();

            return PartialView(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register_Client(RegisterClient model)
        {
            Manager m = new Manager();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Suite = 0, Plan = "" };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Add claims
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Email, model.Email));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, model.GivenName));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Surname, model.Surname));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "User"));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "Client"));

                    TempData["Name"] = model.GivenName + " " + model.Surname;
                    TempData["Id"] = user.Id;

                    ProjectAdd project = new ProjectAdd();

                    project.Name = model.Name;
                    project.Developer = model.GivenName + " " + model.Surname;
                    project.Street = model.Street;
                    project.City = model.City;
                    project.Province = model.Prov;
                    project.Country = model.Country;
                    project.Postal = model.Postal;

                    project.ClientID = user.Id;

                    m.ProjectAdd(project);
                    
                    return RedirectToAction("Index", "AccountManager");

                    }
                AddErrors(result);
            }

            // Something failed :( redisplay the form

            var form = m.mapper.Map<RegisterViewModelForm>(model);
            var roles = m.RoleClaimGetAllStrings();


            return View(form);
        }


        // Register_Cust
        // Used to create accounts of different types, does not log in newly created account
        public ActionResult Register_Admin()
        {
            // Define a register form
            var form = new RegisterAdminForm();
            //form.RoleList = new MultiSelectList(roles);

            return PartialView(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register_Admin(RegisterAdmin model)
        {
            Manager m = new Manager();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Suite = 0, Plan = "" };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Add claims
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Email, model.Email));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, model.GivenName));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Surname, model.Surname));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "User"));
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "Admin"));

                    //return RedirectToAction("Register_Project", "Home");
                    return RedirectToAction("Index", "AccountManager");

                }
                AddErrors(result);
            }

            // Something failed :( redisplay the form

            var form = m.mapper.Map<RegisterViewModelForm>(model);
            var roles = m.RoleClaimGetAllStrings();
            

            return View(form);
        }
       
        public ActionResult Create_Project(string name, string id)
        {
            Manager m = new Manager();

            ViewData["Name"] = TempData["Name"] as string;
            ViewData["Id"] = TempData["Id"] as string;

            // Define a register form
            var form = new ProjectAdd();

            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Project(ProjectAdd model)
        {
            Manager m = new Manager();

            var addedProject = m.ProjectAdd(model);

            if(model == null)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}