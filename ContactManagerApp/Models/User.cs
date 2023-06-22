namespace ContactManagerApp.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string HashedPassword { get; set; }
        public int RoleID { get; set; }

        //public virtual ICollection<UserEmail> UserEmails { get; set; }
        public virtual Role Role { get; set; }
    }
}
