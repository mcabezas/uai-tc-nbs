using Common;
using Security.Gateway;

namespace Security.BLL
{
    internal class User
    {
        private readonly DAL.User _storage;
        private readonly SessionToken _sessionToken;

        internal User(SessionToken sessionToken)
        {
            _storage = new DAL.User();
            _sessionToken = sessionToken;
        }

        internal MaybeEmpty<BE.SessionToken> Authenticate(AuthenticateUserCommand credentials)
        {
            var found = _storage.Get(credentials.Email);
            if (!found.IsPresent)
            {
                return MaybeEmpty<BE.SessionToken>.Empty();
            }

            var user = found.Get();
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

        internal bool Authorize(string token, string action)
        {
            return true;
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