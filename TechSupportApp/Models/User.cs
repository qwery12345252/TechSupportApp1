namespace TechSupportApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public int? ClientId { get; set; }
        public int? EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}