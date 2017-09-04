using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _3MA.Controllers
{

    public class ProjectAdd
    {
        [Display(Name="Project name")]
        public string Name { get; set; }

        [Display(Name="Developer's name")]
        public string Developer { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Postal { get; set; }
        public string Country { get; set; }

        // Let customers choose pre-made packages of products
        public bool isPackage { get; set; }

        // Let customer choose each product
        public bool isProduct { get; set; }

        public string ClientID { get; set; }
    }

    public class ProjectBase : ProjectAdd
    {
        [Key]
        public int Id { get; set; }
    }
}