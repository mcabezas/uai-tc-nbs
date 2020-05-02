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

        internal User(SessionToken sessionToken, ITime time)
        {
            _storage = new DAL.User();
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
            return 0;
        }

        internal long Update(BE.User user)
        {
            return 0;
        }

        internal long Get(BE.User user)
        {
            return 0;
        }

        internal long Delete(BE.User user)
        {
            return 0;
        }
    }
}