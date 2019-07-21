using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmailId { get; set; }
        [Required, Display(Name = "From Name")]
        public string FromName { get; set; }
        [Required, Display(Name = "From email"), EmailAddress]
        public string ToEmail { get; set; }
        public string FromEmail { get; set; }
        [Required]
        public string Message { get; set; }
        public string Subject { get; set; }
        public string CC { get; set; }
        public string Bcc { get; set; }
        public string ViewMessage { get; set; }
    }
}
