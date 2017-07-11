using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _3MA.Controllers
{
    public class ProductAdd
    {
        [Required(ErrorMessage = "Category is required!"), Display(Name = "Main category")]
        public string MainCategory { get; set; }
        [Required(ErrorMessage = "Sub category is required!"), Display(Name = "Sub category")]
        public string SubCategory { get; set; }
        [Required(ErrorMessage = "Price category is required!"), Display(Name = "Price category")]
        [StringLength(1, ErrorMessage = "Price category must be one character (A-E)")]
        public string PriceCategory { get; set; }
        [Required(ErrorMessage = "Manufacturing Sku is required!"), Display(Name = "Manufacturing Sku")]
        public string MFG_SKU { get; set; }
        [Required(ErrorMessage = "Collection is required!")]
        public string Collection { get; set; }
        public string Finish { get; set; }
        [Required(ErrorMessage = "Name is required!"), StringLength(50, ErrorMessage = "Name must be no larger than 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description required!"), StringLength(200, ErrorMessage = "Description must be no larger than 200 characters long")]
        public String Description { get; set; }
        [Required(ErrorMessage = "Price must be included!")]
        public double Price { get; set; }
        public string Image { get; set; }
        public string Specs { get; set; }

        public string DimW { get; set; }
        public string DimTH { get; set; }
        public string DimL { get; set; }

        [Display(Name = "Price per square foot")]
        public double PricePerSF { get; set; }
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
                return DimW + " x " + DimTH + " x " + DimL;
            }
        }
    }

    public class ProductAddForm
    {
        [Required(ErrorMessage = "Category is required!"), Display(Name = "Main category")]
        public string MainCategory { get; set; }
        [Required(ErrorMessage = "Sub category is required!"), Display(Name = "Sub category")]
        public string SubCategory { get; set; }
        [Required(ErrorMessage = "Price category is required!"), Display(Name = "Price category")]
        [StringLength(1, ErrorMessage = "Price category must be one character (A-E)")]
        public string PriceCategory { get; set; }
        [Required(ErrorMessage = "Manufacturing Sku is required!"), Display(Name = "Manufacturing Sku")]
        public string MFG_SKU { get; set; }
        [Required(ErrorMessage = "Collection is required!")]
        public string Collection { get; set; }
        public string Finish { get; set; }
        [Required(ErrorMessage = "Name is required!"), StringLength(50, ErrorMessage = "Name must be no larger than 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description required!"), StringLength(200, ErrorMessage = "Description must be no larger than 200 characters long")]
        public String Description { get; set; }
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
        [Required(ErrorMessage = "Price category is required!"), Display(Name = "Price category")]
        [StringLength(1, ErrorMessage = "Price category must be one character (A-E)")]
        public string PriceCategory { get; set; }
        [Required(ErrorMessage = "Manufacturing Sku is required!"), Display(Name = "Manufacturing Sku")]
        public string MFG_SKU { get; set; }
        [Required(ErrorMessage = "Collection is required!")]
        public string Collection { get; set; }
        public string Finish { get; set; }
        [Required(ErrorMessage = "Name is required!"), StringLength(50, ErrorMessage = "Name must be no larger than 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description required!"), StringLength(200, ErrorMessage = "Description must be no larger than 200 characters long")]
        public String Description { get; set; }
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
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category is required!"), Display(Name = "Main category")]
        public string MainCategory { get; set; }
        [Required(ErrorMessage = "Sub category is required!"), Display(Name = "Sub category")]
        public string SubCategory { get; set; }
        [Required(ErrorMessage = "Price category is required!"), Display(Name = "Price category")]
        [StringLength(1, ErrorMessage = "Price category must be one character (A-E)")]
        public string PriceCategory { get; set; }
        [Required(ErrorMessage = "Manufacturing Sku is required!"), Display(Name = "Manufacturing Sku")]
        public string MFG_SKU { get; set; }
        [Required(ErrorMessage = "Collection is required!")]
        public string Collection { get; set; }
        public string Finish { get; set; }
        [Required(ErrorMessage = "Name is required!"), StringLength(50, ErrorMessage = "Name must be no larger than 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description required!"), StringLength(200, ErrorMessage = "Description must be no larger than 200 characters long")]
        public String Description { get; set; }
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

    public class ProductSearchForm
    {
        public ProductSearchForm()
        {
            dimX = new List<string>();
            dimY = new List<string>();
            dimZ = new List<string>();
            PriceCat = new List<string>();
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

    }
}