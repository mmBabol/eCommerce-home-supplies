﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace _3MA.Controllers
{

    // -----------------    Order    ----------------------------------

    public class OrderAdd
    {
        public PackageWithProducts OrderWithPackage { get; set; }
        [Required]
        public int Suite { get; set; }
        public bool LightUpgrade { get; set; }
        public int PackageId { get; set; }
    }

    public class OrderBase
    {
        [Key]
        public int Id { get; set; }
    }

    public class OrderAddForm
    {
        public OrderAddForm()
        {
            OrderWithPackageList = new List<PackageWithProducts>();
        }
        public ICollection<PackageWithProducts> OrderWithPackageList { get; set; }
        public int Suite { get; set; }
        public int MyProperty { get; set; }
        public bool LightUpgrade { get; set; }
    }

    // -----------------    POrder    ----------------------------------

    public class POrderAdd
    {
        public POrderAdd()
        {
            Completed = false;
            IdProductList = new List<int>();
            AllProducts = new List<ProductBase>();
            Room = new Dictionary<int, string>();
            Qty = new Dictionary<int, int>();
            OrderPlaced = DateTime.Now;
            MoveIn = DateTime.Now;
        }

        [Required]
        public int Suite { get; set; }
        [Display(Name = "Floor plan")]
        //, StringLength(5, ErrorMessage = "The {0} must be {2} or fewer characters.")
        [Required]
        public string Plan { get; set; }
        public bool LightUpgrade { get; set; }
        public bool Completed { get; set; }
        public DateTime OrderPlaced { get; set; }

        [Display(Name = "Home Owner's name")]
        public string Name { get; set; }
        [Display(Name = "Home phone")]
        public string HPhone { get; set; }
        [Display(Name = "Mobile phone")]
        public string MPhone { get; set; }
        [Display(Name = "Move in date")]
        public DateTime MoveIn { get; set; }

        public ICollection<int> IdProductList { get; set; }
        public ICollection<ProductBase> AllProducts { get; set; }

        public Dictionary<int, string> Room { get; set; }
        public Dictionary<int, int> Qty { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Prov { get; set; }
        public string Country { get; set; }
        public string Postal { get; set; }

        public string customerID { get; set; }
    }

    public class POrderBase : POrderAdd
    {
        [Key]
        public int Id { get; set; }

        public string getHPhone
        {
            get
            {
                return (HPhone.Length == 10) ? HPhone.Substring(0, 3) + "-" + HPhone.Substring(3, 3) + "-" + HPhone.Substring(6, 4) : "-";
            }
        }

        public string getMPhone
        {
            get
            {
                return (MPhone.Length == 10) ? MPhone.Substring(0, 3) + "-" + MPhone.Substring(3, 3) + "-" + MPhone.Substring(6, 4) : "-";
            }
        }
        public string getAddressName
        {
            get
            {
                return ((Street != null) ? Street + ", " : "" ) + ((City != null) ? City + ", " : "") + ((Name != null) ? Name : "");
            }
        }
        public string getAddress
        {
            get
            {
                return ((Street != null) ? Street + ", " : "") + ((City != null) ? City : "");
            }
        }
        public string getProv
        {
            get
            {
                return ((Prov != null) ? Prov + ", " : "") + ((Country != null) ? Country + ", " : "") + ((Postal != null) ? Postal : "");
            }
        }
    }

    public class POrderAddForm
    {
        public POrderAddForm()
        {
            Completed = false;
            MoveIn = DateTime.Now;
            AllProducts = new List<ProductBase>();
            IdProductList = new List<int>();
        }
        [Required]
        public int Suite { get; set; }
        [Display(Name = "Floor plan")]
        [Required, StringLength(5, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string Plan { get; set; }
        [Display(Name = "Light upgrades?")]
        public bool LightUpgrade { get; set; }
        public bool Completed { get; set; }
        public DateTime OrderPlaced { get; set; }
        [Display(Name = "Home Owner's name")]
        public string Name { get; set; }
        [Phone, Display(Name = "Home phone")]
        public string HPhone { get; set; }
        [Phone, Display(Name = "Mobile phone")]
        public string MPhone { get; set; }
        [Display(Name = "Move in date")]
        [DataType(DataType.Date)]
        public DateTime MoveIn { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Prov { get; set; }
        public string Country { get; set; }
        public string Postal { get; set; }

        public ICollection<ProductBase> AllProducts { get; set; }
        public ICollection<int> IdProductList { get; set; }
        public ProductSearchForm Search { get; set; }
    }

    public class POrderEditForm
    {
        public POrderEditForm()
        {
            AllProducts = new List<ProductBase>();
            MoveIn = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int Suite { get; set; }
        [Display(Name = "Floor plan")]
        [Required]
        public string Plan { get; set; }
        [Display(Name = "Home Owner's name")]
        public string Name { get; set; }
        [Display(Name = "Home phone")]
        public string HPhone { get; set; }
        [Display(Name = "Mobile phone")]
        public string MPhone { get; set; }
        [Display(Name = "Move in date")]
        public DateTime MoveIn { get; set; }
        public ICollection<ProductBase> AllProducts { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Prov { get; set; }
        public string Country { get; set; }
        public string Postal { get; set; }
    }

    public class POrderEditInfo
    {
        public POrderEditInfo()
        {
            AllProducts = new List<ProductBase>();
            MoveIn = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public int Suite { get; set; }
        [Display(Name = "Floor plan")]
        [Required]
        public string Plan { get; set; }
        [Display(Name = "Home Owner's name")]
        public string Name { get; set; }
        [Display(Name = "Home phone")]
        public string HPhone { get; set; }
        [Display(Name = "Mobile phone")]
        public string MPhone { get; set; }
        [Required, Display(Name = "Move in date")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime MoveIn { get; set; }
        public ICollection<ProductBase> AllProducts { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Prov { get; set; }
        public string Country { get; set; }
        public string Postal { get; set; }
    }
}