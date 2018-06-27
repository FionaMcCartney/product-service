using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductService.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        //public string Category { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }
        // Navigation property
        public Category Category { get; set; }

    }
}