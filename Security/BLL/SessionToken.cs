using System;
using System.Text;
using Common;

namespace Security.BLL
{
    internal class SessionToken
    {
        private readonly DAL.SessionToken _storage;
        private const int TokenSize = 64;

        internal SessionToken()
        {
            _storage = new DAL.SessionToken();
        }

        public MaybeEmpty<BE.SessionToken> GenerateToken(BE.User user)
        {
            var token = GenerateToken(TokenSize, true);
            var sessionToken = new BE.SessionToken() {User = user, Token = token};
            _storage.Save(sessionToken);
            return MaybeEmpty<BE.SessionToken>.Of(sessionToken);
        }

        private string GenerateToken(int size, bool lowerCase)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public MaybeEmpty<BE.SessionToken> Get()
        {
            var sessionToken = new BE.SessionToken
            {
                User = null,
                Token = "",
                ExpireAt = DateTime.Now,
            };
            
            return MaybeEmpty<BE.SessionToken>.Of(sessionToken);
        }
    }
}