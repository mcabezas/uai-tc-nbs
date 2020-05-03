using Common;

namespace Security.BLL
{
    internal class Role
    {
        private readonly DAL.Role _storage;

        public Role(IDatabase database)
        {
            _storage = new DAL.Role(database);
        }

        internal long Save(BE.Role user)
        {
            return _storage.Save(user);
        }

        internal void Update(BE.Role user)
        {
            _storage.Update(user);
        }

        internal MaybeEmpty<BE.Role> Get(long userId)
        {
            return _storage.Get(userId);
        }

        internal void Delete(BE.Role user)
        {
            _storage.Delete(user);
        }
    }
}