using Common;

namespace Security.DAL
{
    internal class SessionToken
    {
        private IDatabase _database;

        public SessionToken(IDatabase database)
        {
            _database = database;
        }

        public void Save(BE.SessionToken sessionToken)
        {
        }
        
        public MaybeEmpty<BE.SessionToken> Get(string token)
        {
            return MaybeEmpty<BE.SessionToken>.Empty();
        }
    }
}