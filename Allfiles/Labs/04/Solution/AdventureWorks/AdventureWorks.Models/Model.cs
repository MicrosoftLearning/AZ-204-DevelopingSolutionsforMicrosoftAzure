using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.Models
{
    public class Model
    {
        [Key]
        public Guid id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public List<Product> Products { get; set; }

        public string Photo { get; set; }
    }
}