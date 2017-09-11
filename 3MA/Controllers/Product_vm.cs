using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _3MA.Controllers
{
    // Due to each product having different columns needed to be saved for database, some of the columns do not have a meaningful
    // column/data member name. Below, the meaning of all columns for each product is explained, whether it is the same or different meaning
    public class ProductAdd
    {
        public ProductAdd()
        {
            AllAccessories = new List<AccessoriesBase>();
        }

        /// MainCategory - Main category for all products
        [Required(ErrorMessage = "Category is required!"), Display(Name = "Main category")]
        public string MainCategory { get; set; }

        /// SubCategory - Sub category for all products
        [Required(ErrorMessage = "Sub category is required!"), Display(Name = "Sub category")]
        public string SubCategory { get; set; }

        /// PriceCategory - only flooring uses this column
        [Display(Name = "Price category")]
        [StringLength(1, ErrorMessage = "Price category must be one character (A-E)")]
        public string PriceCategory { get; set; }

        /// MFG_SKU - SKU number for all products
        [Required(ErrorMessage = "Manufacturing Sku is required!"), Display(Name = "Manufacturing Sku")]
        public string MFG_SKU { get; set; }

        /// Collection - Collection type, used by all products
        public string Collection { get; set; }

        /// Finish - Flooring products finish type
        public string Finish { get; set; }

        /// Name - Name of product, used by all
        [Required(ErrorMessage = "Name is required!"), StringLength(50, ErrorMessage = "Name must be no larger than 100 characters")]
        public string Name { get; set; }

        /// Description - Product description, used by all
        [Required(ErrorMessage = "Description required!"), StringLength(200, ErrorMessage = "Description must be no larger than 300 characters long")]
        public string Description { get; set; }

        /// Price - Product price, used by all products
        [Required(ErrorMessage = "Price must be included!")]
        public double Price { get; set; }

        /// Image - Image link of product, used by all products. Some products may not have any images
        public string Image { get; set; }

        /// Specs - Product specs, used by all products. Some products may not have any specs
        public string Specs { get; set; }

        /// DimW - Dimension Width.
        /// Flooring - dimension, width
        /// Lighting - Fixture height
        /// Plumbing - dimension x
        /// Tiles - dimension, x
        /// Appliances-
        public string DimW { get; set; }

        /// DimTH - Dimension Height.
        /// Flooring - dimension, height
        /// Lighting - Depth
        /// Plumbing - dimension y
        /// Tiles - dimension, y
        /// Appliances-
        public string DimTH { get; set; }

        /// DimL - Dimension Thickness.
        /// Flooring - dimension, length
        /// Lighting - Width / Length
        /// Plumbing - dimension z
        /// Tiles - dimension, z
        /// Appliances-
        public string DimL { get; set; }

        /// PricePerSF - Price per square foot
        /// Flooring - price per square foot f flooring product
        /// Lighting - Diameter
        /// Plumbing - n/a
        /// Tiles - SqFt / Skid
        /// Appliances -
        public double PricePerSF { get; set; }

        /// SqFt - Square feet
        /// Flooring - price per square foot f flooring product
        /// Lighting - Diameter
        /// Plumbing - n/a
        /// Tiles - SqFt / Skid
        /// Appliances -
        public double SqFt { get; set; }

        public double Lbs { get; set; }
        public string Type { get; set; }
        public string RHC { get; set; }
        public string Core { get; set; }
        public string Strctr { get; set; }
        public string Fnsh { get; set; }

        //[Required(ErrorMessage = "Category is required!"), Display(Name = "Main category")]
        //public string MainCategory { get; set; }
        //[Required(ErrorMessage = "Sub category is required!"), Display(Name = "Sub category")]
        //public string SubCategory { get; set; }
        //[Required(ErrorMessage = "Price category is required!"), Display(Name = "Price category")]
        //[StringLength(1, ErrorMessage = "Price category must be one character (A-E)")]
        //public string PriceCategory { get; set; }
        //[Required(ErrorMessage = "Manufacturing Sku is required!"), Display(Name = "Manufacturing Sku")]
        //public string MFG_SKU { get; set; }
        //[Required(ErrorMessage = "Collection is required!")]
        //public string Collection { get; set; }
        //public string Finish { get; set; }
        //[Required(ErrorMessage = "Name is required!"), StringLength(100, ErrorMessage = "Name must be no larger than 100 characters")]
        //public string Name { get; set; }
        //[Required(ErrorMessage = "Description required!"), StringLength(300, ErrorMessage = "Description must be no larger than 300 characters long")]
        //public string Description { get; set; }
        //[Required(ErrorMessage = "Price must be included!")]
        //public double Price { get; set; }
        //public string Image { get; set; }
        //public string Specs { get; set; }

        //public string DimW { get; set; }
        //public string DimTH { get; set; }
        //public string DimL { get; set; }

        //[Display(Name = "Price per square foot")]
        //public double PricePerSF { get; set; }
        //[Display(Name = "Carton square feet")]
        //public double SqFt { get; set; }
        //[Display(Name = "Carton pounds")]
        //public double Lbs { get; set; }
        //[Display(Name = "Installation type")]
        //public string Type { get; set; }
        //[Display(Name = "Installation RHC")]
        //public string RHC { get; set; }
        //[Display(Name = "Installation Core")]
        //public string Core { get; set; }
        //[Display(Name = "Warranty structure")]
        //public string Strctr { get; set; }
        //[Display(Name = "Warranty Finish")]
        //public string Fnsh { get; set; }

        public ICollection<AccessoriesBase> AllAccessories { get; set; }
    }

    public class ProductBase : ProductAdd
    {
        [Key]
        public int Id { get; set; }

        public string getName
        {
            get
            {
                return MFG_SKU + " - " + MainCategory + " " + SubCategory + ", " + Name;
            }
        }

        public string getDim
        {
            get
            {
                string temp = "";


                if(DimW != null && DimW.Trim() != "") { temp = DimW; }

                if (DimTH != null && DimTH.Trim() != "")
                {
                    if (temp != "") { temp += " x "; }
                    temp += DimTH;
                }

                if (DimL != null && DimL.Trim() != "")
                {
                    if (temp != "") { temp += " x "; }
                    temp += DimL;
                }


                return temp;
            }
        }
    }

    public class ProductAddForm
    {
        [Required(ErrorMessage = "Category is required!"), Display(Name = "Main category")]
        public string MainCategory { get; set; }
        [Required(ErrorMessage = "Sub category is required!"), Display(Name = "Sub category")]
        public string SubCategory { get; set; }
        [Display(Name = "Price category")]
        [StringLength(1, ErrorMessage = "Price category must be one character (A-E)")]
        public string PriceCategory { get; set; }
        [Required(ErrorMessage = "Manufacturing Sku is required!"), Display(Name = "Manufacturing Sku")]
        public string MFG_SKU { get; set; }
        public string Collection { get; set; }
        public string Finish { get; set; }
        [Required(ErrorMessage = "Name is required!"), StringLength(100, ErrorMessage = "Name must be no larger than 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description required!"), StringLength(300, ErrorMessage = "Description must be no larger than 300 characters long")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price must be included!")]
        public double Price { get; set; }
        public string Image { get; set; }
        public string Specs { get; set; }

        [Display(Name = "Carton square feet")]
        public double SqFt { get; set; }
        [Display(Name = "Carton pounds")]
        public double Lbs { get; set; }
        [Display(Name = "Installation type")]
        public string Type { get; set; }
        [Display(Name = "Installation RHC")]
        public string RHC { get; set; }
        [Display(Name = "Installation Core")]
        public string Core { get; set; }
        [Display(Name = "Warranty structure")]
        public string Strctr { get; set; }
        [Display(Name = "Warranty Finish")]
        public string Fnsh { get; set; }
    }

    public class ProductEditForm
    {
        [Required(ErrorMessage = "Category is required!"), Display(Name = "Main category")]
        public string MainCategory { get; set; }
        [Required(ErrorMessage = "Sub category is required!"), Display(Name = "Sub category")]
        public string SubCategory { get; set; }
        [Display(Name = "Price category")]
        [StringLength(1, ErrorMessage = "Price category must be one character (A-E)")]
        public string PriceCategory { get; set; }
        [Required(ErrorMessage = "Manufacturing Sku is required!"), Display(Name = "Manufacturing Sku")]
        public string MFG_SKU { get; set; }
        public string Collection { get; set; }
        [Required(ErrorMessage = "Name is required!"), StringLength(100, ErrorMessage = "Name must be no larger than 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description required!"), StringLength(300, ErrorMessage = "Description must be no larger than 300 characters long")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price must be included!")]
        public double Price { get; set; }
        public string Image { get; set; }
        public string Specs { get; set; }

        [Display(Name = "Carton square feet")]
        public double SqFt { get; set; }
        [Display(Name = "Carton pounds")]
        public double Lbs { get; set; }
        [Display(Name = "Installation type")]
        public string Type { get; set; }
        [Display(Name = "Installation RHC")]
        public string RHC { get; set; }
        [Display(Name = "Installation Core")]
        public string Core { get; set; }
        [Display(Name = "Warranty structure")]
        public string Strctr { get; set; }
        [Display(Name = "Warranty Finish")]
        public string Fnsh { get; set; }
    }

    public class ProductEditInfo
    {
        public ProductEditInfo()
        {
            AllAccessories = new List<AccessoriesBase>();
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category is required!"), Display(Name = "Main category")]
        public string MainCategory { get; set; }
        [Required(ErrorMessage = "Sub category is required!"), Display(Name = "Sub category")]
        public string SubCategory { get; set; }
        [Display(Name = "Price category")]
        [StringLength(1, ErrorMessage = "Price category must be one character (A-E)")]
        public string PriceCategory { get; set; }
        [Required(ErrorMessage = "Manufacturing Sku is required!"), Display(Name = "Manufacturing Sku")]
        public string MFG_SKU { get; set; }
        public string Collection { get; set; }
        public string Finish { get; set; }
        [Required(ErrorMessage = "Name is required!"), StringLength(100, ErrorMessage = "Name must be no larger than 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description required!"), StringLength(300, ErrorMessage = "Description must be no larger than 300 characters long")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price must be included!")]
        public double Price { get; set; }
        public string Image { get; set; }
        public string Specs { get; set; }

        [Display(Name = "Carton square feet")]
        public double SqFt { get; set; }
        [Display(Name = "Carton pounds")]
        public double Lbs { get; set; }
        [Display(Name = "Installation type")]
        public string Type { get; set; }
        [Display(Name = "Installation RHC")]
        public string RHC { get; set; }
        [Display(Name = "Installation Core")]
        public string Core { get; set; }
        [Display(Name = "Warranty structure")]
        public string Strctr { get; set; }
        [Display(Name = "Warranty Finish")]
        public string Fnsh { get; set; }
        public ICollection<AccessoriesBase> AllAccessories { get; set; }
    }

    public class ProductSearchForm
    {
        public ProductSearchForm()
        {
            dimX = new List<string>();
            dimY = new List<string>();
            dimZ = new List<string>();
            PriceCat = new List<string>();
            Filter = new Dictionary<string, bool>();
        }

        // TODO: searchform
        [Display(Name = "Colour of product")]
        public string colour { get; set; }

        [Display(Name = "Product NAME")]
        public string species { get; set; }

        //[RegularExpression("([0-9]*)", ErrorMessage = "Count must be a natural number")]
        public List<string> dimX { get; set; }

        //[RegularExpression("([0-9]*)", ErrorMessage = "Count must be a natural number")]
        public List<string> dimY { get; set; }

        //[RegularExpression("([0-9]*)", ErrorMessage = "Count must be a natural number")]
        public List<string> dimZ { get; set; }

        public List<string> PriceCat { get; set; }

        public Dictionary<string, bool> Filter { get; set; }

    }
}