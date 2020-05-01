using Common;

namespace Security.DAL
{
    internal class User
    {
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
    }
}