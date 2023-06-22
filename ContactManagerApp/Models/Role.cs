namespace ContactManagerApp.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
