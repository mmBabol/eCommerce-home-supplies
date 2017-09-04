using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3MA.Controllers
{
    public class ApplicationUserBase
    {
        public ApplicationUserBase()
        {
            Roles = new List<string>();
        }
        [Key]
        public string Id { get; set; }
        public int Suite { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string allRoles
        {
            get
            {
                string all = "";
                foreach(var i in Roles)
                {
                    all += i + ",";
                }

                return all.TrimEnd(',');
            }
        }
    }

    public class ApplicationUserAdd
    {
        public ApplicationUserAdd()
        {
            Roles = new List<string>();
        }
        public int Suite { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }

    public class ApplicationUserDetail : ApplicationUserBase
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
    }

    public class ApplicationUserDelete
    {
        [Required]
        public string Id { get; set; }
    }

    public class ApplicationUserEditForm
    {
        [Key]
        public string MyProperty { get; set; }
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Display(Name ="Surname")]
        public string Surname { get; set; }
        [Display(Name = "Suite number")]
        public int Suite { get; set; }
        [Display(Name ="Floor plan")]
        public string Plan { get; set; }
        [Display(Name ="All Roles")]
        public MultiSelectList RoleList { get; set; }
    }

    public class ApplicationUserEdit
    {
        public ApplicationUserEdit()
        {
            Roles = new List<string>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string UserName { get; set; }

        [Display(Name = "Given (first) name(s)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string GivenName { get; set; }

        [Display(Name = "Surname (family name)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string Surname { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }

}