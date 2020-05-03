using Common;

namespace Security.DAL
{
    internal class Permission
    {
        private IDatabase _database;

        public Permission(IDatabase database)
        {
            _database = database;
        }

        internal long Save(BE.Permission permission)
        {
            return 0;
        }

        internal void Update(BE.Permission permission)
        {
            return;
        }

        internal MaybeEmpty<BE.Permission> Get(long permissionId)
        {
            return MaybeEmpty<BE.Permission>.Empty();
        }

        internal void Delete(BE.Permission permission)
        {
            return ;
        }
    }
}