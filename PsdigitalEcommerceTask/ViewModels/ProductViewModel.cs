using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PsdigitalEcommerceTask.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Invalid Image format or size")]
        public HttpPostedFileBase Image { get; set; }

        [Required]
        public double Price { get; set; }

        public string ImageName { get; set; }
    }
}