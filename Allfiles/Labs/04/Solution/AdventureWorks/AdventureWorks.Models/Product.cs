using System;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.Models
{
    public class Product
    {
        [Key]
        public Guid id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string Category { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public decimal? Weight { get; set; }

        public decimal ListPrice { get; set; }

        public string Photo { get; set; }
    }
}