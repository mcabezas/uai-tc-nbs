using System.Collections.ObjectModel;

namespace Security.BE
{
    internal class Role
    {
        public string Name { get; set; }
        public Collection<Role> Roles { get; set; }
        public Collection<Permission> Permissions { get; set; }
    }
}