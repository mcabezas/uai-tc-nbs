using Common;

namespace Security.BLL
{
    internal class Permission
    {
        private readonly DAL.Permission _storage;

        public Permission(IDatabase database)
        {
            _storage = new DAL.Permission(database);
        }

        internal long Save(BE.Permission permission)
        {
            return _storage.Save(permission);
        }

        internal void Update(BE.Permission permission)
        {
            _storage.Update(permission);
        }

        internal MaybeEmpty<BE.Permission> Get(long permissionId)
        {
            return _storage.Get(permissionId);
        }

        internal void Delete(BE.Permission permission)
        {
            _storage.Delete(permission);
        }
    }
}