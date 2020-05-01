using System.Collections.ObjectModel;
using System.Data.Common;

namespace Security.BE
{
    internal class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Collection<Role> Roles { get; set; }
        public Collection<Permission> Permissions { get; set; }
    }
}