using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MikeDemoDBEntities.Models
{
    public class AutomobileTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AutomobileTypeId { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public int wheels { get; set; }

    }
}
