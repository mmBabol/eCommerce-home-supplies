﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _3MA.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class myRoles
    {
        [Display(Name = "Role(s) - select one or more")]
        public System.Web.Mvc.MultiSelectList RoleList { get; set; }
    }

    // Claims-aware RegisterViewModelForm class
    // Send this object to the HTML Form
    // Includes a multi select list
    // User registration form
    public class RegisterViewModelForm
    {
        public RegisterViewModelForm()
        {
            MoveIn = DateTime.Now;
        }
        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Given (first) name(s)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string GivenName { get; set; }

        [Display(Name = "Surname (family name)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string Surname { get; set; }

        // Attention - suite number definition
        [Display(Name = "Suite number")]
        [Range(0, 3000)]
        public int Suite { get; set; }

        [Display(Name = "Address")]
        public string Street { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Province")]
        public string Prov { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Postal Code")]
        public string Postal { get; set; }

        [Display(Name = "Floor plan")]
        [StringLength(5, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string Plan { get; set; }

        [Phone, Display(Name = "Home phone")]
        public string HPhone { get; set; }

        [Phone, Display(Name = "Mobile phone")]
        public string MPhone { get; set; }

        [Display(Name = "Move in date")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime MoveIn { get; set; }

        // Bill information
        [Display(Name = "Street")]
        public string BillStreet { get; set; }

        [Display(Name = "City")]
        public string BillCity { get; set; }

        [Display(Name = "Province")]
        public string BillProv { get; set; }

        [Display(Name = "Country")]
        public string BillCountry { get; set; }

        [Display(Name = "Postal Code")]
        public string BillPostal { get; set; }

        public string ProjectName { get; set; }
        public int ProjectId { get; set; }

        public bool isSameAddress { get; set; }
    }

    // Claims-aware RegisterViewModel class
    // This view model class includes name and role claim fields
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            MoveIn = DateTime.Now;
        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Given (first) name(s)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string GivenName { get; set; }

        [Display(Name = "Surname (family name)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string Surname { get; set; }

        // Attention - suite number definition
        [Display(Name = "Suite number")]
        [Range(0, 3000)]
        public int Suite { get; set; }

        [Display(Name = "Address")]
        public string Street { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Province")]
        public string Prov { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Postal Code")]
        public string Postal { get; set; }

        [Display(Name = "Floor plan")]
        [StringLength(5, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string Plan { get; set; }

        [Phone, Display(Name = "Home phone")]
        public string HPhone { get; set; }

        [Phone, Display(Name = "Mobile phone")]
        public string MPhone { get; set; }

        [Display(Name = "Move in date")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime MoveIn { get; set; }

        // Bill information
        [Display(Name = "Street")]
        public string BillStreet { get; set; }

        [Display(Name = "City")]
        public string BillCity { get; set; }

        [Display(Name = "Province")]
        public string BillProv { get; set; }

        [Display(Name = "Country")]
        public string BillCountry { get; set; }

        [Display(Name = "Postal Code")]
        public string BillPostal { get; set; }

        public string ProjectName { get; set; }
        public int ProjectId { get; set; }

        public bool isSameAddress { get; set; }
    }

    public class RegisterClientForm
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Given (first) name(s)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string GivenName { get; set; }

        [Display(Name = "Surname (family name)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string Surname { get; set; }

        [Display(Name = "Project name")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        public string Street { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Province")]
        public string Prov { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Postal Code")]
        public string Postal { get; set; }

        public bool isProduct { get; set; }

        public bool isPackage { get; set; }

        public string ClientID { get; set; }
    }
    
    public class RegisterClient
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Given (first) name(s)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string GivenName { get; set; }

        [Display(Name = "Surname (family name)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string Surname { get; set; }

        [Display(Name = "Project name")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        public string Street { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Province")]
        public string Prov { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Postal Code")]
        public string Postal { get; set; }

        public bool isProduct { get; set; }
        
        public bool isPackage { get; set; }

        public string ClientID { get; set; }
        
    }

    public class RegisterAdminForm
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Given (first) name(s)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string GivenName { get; set; }

        [Display(Name = "Surname (family name)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string Surname { get; set; }
        
    }

    public class RegisterAdmin
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Given (first) name(s)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string GivenName { get; set; }

        [Display(Name = "Surname (family name)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string Surname { get; set; }
        
    }

    // ############################################################
    // New account details view model class

    public class AccountDetails
    {
        [Display(Name = "User (login) name")]
        public string UserName { get; set; }

        [Display(Name = "Internal name")]
        public string ClaimsName { get; set; }

        [Display(Name = "Given (first) name(s)")]
        public string ClaimsGivenName { get; set; }

        [Display(Name = "Surname (family name)")]
        public string ClaimsSurname { get; set; }

        [Display(Name = "Email address")]
        public string ClaimsEmail { get; set; }

        [Display(Name ="Suite number")]
        public int ClaimsSuite { get; set; }

        [Display(Name = "Floor plan")]
        public string ClaimsPlan{ get; set; }

        [Display(Name = "Roles")]
        public string ClaimsRoles { get; set; }
    }

    // ############################################################

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
