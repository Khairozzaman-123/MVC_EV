using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evidence_01.Models.ViewModels
{

    public class ProductsViewModel
    {
        public int ProductId { get; set; }
        [Required, StringLength(50), Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required, Column(TypeName = "money"), Display(Name = "Price"), DisplayFormat(DataFormatString = "{0:#.##}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public HttpPostedFileBase PictureUpload { get; set; }
        public int CategoryId { get; set; }
        [Required, Display(Name = "Order Date")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        public virtual Category Category { get; set; }
    }
}