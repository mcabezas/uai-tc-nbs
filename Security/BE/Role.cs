using System.Collections.ObjectModel;
using System.Reflection;

namespace Security.BE
{
    internal class Role
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public Collection<Role> Roles { get; set; }
        public Collection<Permission> Permissions { get; set; }
    }
}