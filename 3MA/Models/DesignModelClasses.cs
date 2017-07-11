using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace _3MA.Models
{
    // Add your design model classes below

    // Follow these rules or conventions:

    // To ease other coding tasks, the name of the 
    //   integer identifier property should be "Id"
    // Collection properties (including navigation properties) 
    //   must be of type ICollection<T>
    // Valid data annotations are pretty much limited to [Required] and [StringLength(n)]
    // Required to-one navigation properties must include the [Required] attribute
    // Do NOT configure scalar properties (e.g. int, double) with the [Required] attribute
    // Initialize DateTime and collection properties in a default constructor

    public class Package
    {
        public Package()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        public string ImgURL { get; set; }
        public ICollection<Product> Products { get; set; }
    }

    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string MainCategory { get; set; }
        [Required]
        public string SubCategory { get; set; }
        [Required]
        public string PriceCategory { get; set; }
        [Required]
        public string MFG_SKU { get; set; }
        [Required]
        public string Collection { get; set; }
        public string Finish { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required,StringLength(200)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public string Image { get; set; }
        public string Specs { get; set; }

        public string DimW { get; set; }
        public string DimTH { get; set; }
        public string DimL { get; set; }

        public double PricePerSF { get; set; }
        public double SqFt { get; set; }
        public double Lbs { get; set; }
        public string Type { get; set; }
        public string RHC { get; set; }
        public string Core { get; set; }
        public string Strctr { get; set; }
        public string Fnsh { get; set; }
    }

    public class RoomFloor
    {
        public int Id { get; set; }
        public double walls { get; set; }
        public double floor { get; set; }
    }

    public class Order
    {
        public Order()
        {
            Completed = false;
        }
        public int Id { get; set; }
        public int Suite { get; set; }
        public bool LightUpdate { get; set; }
        public Package OrderWithPackage { get; set; }
        public bool Completed { get; set; }
    }

    public class POrder
    {
        public POrder()
        {
            AllProducts = new List<Product>();
            IdProductList = new List<int>();
            Room = new Dictionary<int, string>();
            Qty = new Dictionary<int, int>();
            Completed = false;
            OrderPlaced = DateTime.Now;
            MoveIn = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        public int Suite { get; set; }
        public string Plan { get; set; }
        public bool LightUpgrade { get; set; }
        public ICollection<Product> AllProducts { get; set; }
        public ICollection<int> IdProductList { get; set; }
        public bool Completed { get; set; }
        public DateTime OrderPlaced { get; set; }
        public string Name { get; set; }
        public string HPhone { get; set; }
        public string MPhone { get; set; }
        public DateTime MoveIn { get; set; }
        public string customerID { get; set; }

        public Dictionary<int, string> Room { get; set; }
        public Dictionary<int, int> Qty { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Prov { get; set; }
        public string Country { get; set; }
        public string Postal { get; set; }
    }

    public class RoleClaim
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }

}
