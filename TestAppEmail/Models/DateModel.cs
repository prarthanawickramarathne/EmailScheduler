using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppEmail.Models
{
    public class DateModel
    {
        [Key] // Specify the primary key
        public int Id { get; set; }

        //[Required] // Specify a required field
        //public string Name { get; set; }

        public string DateValue { get; set; }

        //public string DateNo { get; set; }
    }
}
