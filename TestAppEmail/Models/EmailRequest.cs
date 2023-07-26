using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppEmail.Models
{
    public class EmailRequest
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Subject { get; set; }
        //public string HtmlContent { get; set; }
        public string name { get; set; }
    }
}
