using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MikeDemoDBEntities.Models
{
    public class Automobiles : IAutomobileEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AutomobileId { get; set; }
        public string Colour { get; set; }
        public DateTime? DateAdded { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }

        //Store specific details in Json in case we want to use them
        public string JsonDetails { get; set; }
        public virtual AutomobileTypes AutomobileTypes { get; set; }
    }
}
