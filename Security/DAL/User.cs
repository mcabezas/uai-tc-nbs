using Common;

namespace Security.DAL
{
    internal class User
    {
        private IDatabase _database;

        public User(IDatabase database)
        {
            _database = database;
        }

        public MaybeEmpty<BE.User> Get(string email)
        {
            var user = new BE.User() {Id = 1, Password = "EncryptedSaraza"};
            return MaybeEmpty<BE.User>.Of(user);
        }

        public MaybeEmpty<BE.User> Get(long userId)
        {
            var user = new BE.User() {Id = 1, Password = "EncryptedSaraza"};
            return MaybeEmpty<BE.User>.Of(user);
        }
        
        internal long Save(BE.User user)
        {
            return 0;
        }

        internal void Update(BE.User user)
        {
            return;
        }

        internal void Delete(BE.User user)
        {
            return ;
        }
    }
}