using Common;

namespace Security.DAL
{
    internal class Role
    {
        private IDatabase _database;

        public Role(IDatabase database)
        {
            _database = database;
        }
        
        internal long Save(BE.Role role)
        {
            return 0;
        }

        internal void Update(BE.Role role)
        {
            return;
        }

        internal MaybeEmpty<BE.Role> Get(long roleId)
        {
            return MaybeEmpty<BE.Role>.Empty();
        }

        internal void Delete(BE.Role role)
        {
            return ;
        }

    }
}