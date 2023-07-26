using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppEmail.Models
{
    public class SendEmailModel
    {
        [Key] // Specify the primary key
        public int EmailId { get; set; }

        public int IsSent { get; set; }
    }
}
