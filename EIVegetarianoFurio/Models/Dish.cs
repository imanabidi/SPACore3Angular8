using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EIVegetarianoFurio.Models
{
    public class Dish
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0,110)]
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
