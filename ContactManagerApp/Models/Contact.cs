using System.ComponentModel.DataAnnotations;

namespace ContactManagerApp.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? Prefecture { get; set; }
        public string? PostalCode { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? DateofBirth { get; set; }
        public string? Mobile { get; set; }

        public virtual ICollection<Email>? Emails { get; set; }
    }
}
