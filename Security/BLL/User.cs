using Common;
using Security.Gateway;
using Time;

namespace Security.BLL
{
    internal class User
    {
        private readonly DAL.User _storage;
        private readonly SessionToken _sessionToken;
        private readonly ITime _time;

        internal User(SessionToken sessionToken, ITime time, IDatabase database)
        {
            _storage = new DAL.User(database);
            _sessionToken = sessionToken;
            _time = time;
        }

        internal MaybeEmpty<BE.SessionToken> Authenticate(AuthenticateUserCommand credentials)
        {
            var lookUp = _storage.Get(credentials.Email);
            if (!lookUp.Found)
            {
                return MaybeEmpty<BE.SessionToken>.Empty();
            }

            var user = lookUp.Get();
            if (Encrypt(credentials.Password) != user.Password)
            {
                return MaybeEmpty<BE.SessionToken>.Empty();
            }

            return _sessionToken.GenerateToken(user);
        }

        #region Authenticate Helpers

        private string Encrypt(string plain)
        {
            return "Encrypted" + plain;
        }

        #endregion

        internal bool Authorize(AuthorizeUserCommand command)
        {
            var lookUp = _sessionToken.Get();
            if (!lookUp.Found)
            {
                return false;
            }

            var sessionToken = lookUp.Get();
            return sessionToken.ExpireAt.CompareTo(_time.Now()) < 1;
        }
        
        internal long Save(BE.User user)
        {
            return _storage.Save(user);
        }

        internal void Update(BE.User user)
        {
            _storage.Update(user);
        }

        internal MaybeEmpty<BE.User> Get(long userId)
        {
            return _storage.Get(userId);
        }

        internal void Delete(BE.User user)
        {
            _storage.Delete(user);
        }
    }
}