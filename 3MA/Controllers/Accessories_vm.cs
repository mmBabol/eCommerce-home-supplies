using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _3MA.Controllers
{
    public class AccessoriesAdd
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Specs { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Collection { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
        public string SKU { get; set; }
        public string Parent_SKU { get; set; }
    }

    public class AccessoriesBase : AccessoriesAdd
    {
        [Key]
        public int Id { get; set; }
    }

}