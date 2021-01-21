using Microsoft.AspNetCore.Identity;

namespace ESourcing.Core.Entities
{
    public class Employee :IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSupplier { get; set; }
    }
}
