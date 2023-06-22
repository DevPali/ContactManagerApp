using System.ComponentModel.DataAnnotations;

namespace ContactManagerApp.Models
{
    public class Email
    {
        public int ID { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email.")]
        public string? EmailAddress { get; set; }
        public int ContactID { get; set; }

        public virtual Contact? Contact { get; set; }
    }
}
