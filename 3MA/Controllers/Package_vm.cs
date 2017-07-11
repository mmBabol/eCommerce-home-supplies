using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace _3MA.Controllers
{
    public class PackageAdd
    {
        [Required, StringLength(100)]
        public string name { get; set; }
        public string ImgURL { get; set; }
    }

    public class PackageBase: PackageAdd
    {
        [Key]
        public int Id { get; set; }
    }

    public class PackageAddForm
    {
        public PackageAddForm()
        {
            Products = new List<ProductBase>();
        }
        public string name { get; set; }
        public ICollection<ProductBase> Products { get; set; }
        public string ImgURL { get; set; }
    }

    public class PackageWithProducts : PackageBase
    {
        public PackageWithProducts()
        {
            Products = new List<ProductBase>();
        }
        public ICollection<ProductBase> Products { get; set; }
    }

    public class PackageEditForm
    {
        public PackageEditForm()
        {
            Products = new List<ProductBase>();
        }

        [Key]
        public int PackageId { get; set; }

        [Required, StringLength(100)]
        public string name { get; set; }

        public string ImgURL { get; set; }

        public IEnumerable<ProductBase> Products { get; set; }

        [Display(Name ="All products")]
        public MultiSelectList ProductsList { get; set; }
    }

    public class PackageEditInfo
    {
        public PackageEditInfo()
        {
            //Products = new List<ProductBase>();
            ProductIds = new List<int>();
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string name { get; set; }

        public string ImgURL { get; set; }

        //public ICollection<ProductBase> Products { get; set; }
        public ICollection<int> ProductIds { get;  set; }
    }

}