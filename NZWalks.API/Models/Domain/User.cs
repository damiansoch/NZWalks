using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;

        public string EmailAddress { get; set; } = default!;
        public string  Password { get; set; } = default!;

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        [NotMapped]
        public List<string> Roles { get; set; } = default!;

        //navigation property
        public List<User_Role> User_Roles { get; set; } = default!;
    }
}
