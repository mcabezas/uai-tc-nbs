using Common;
using Http.Router;
using Security.BLL;
using Time;

namespace Security.Gateway
{
    public class Gateway
    {
        private readonly BLL.User _user;

        public Gateway(ITime time, IDatabase database)
        {
            var sessionToken = new SessionToken(database);
            _user = new User(sessionToken, time, database);
        }

        public IRouter RoutesUp(IRouter router)
        {
            return router
                .AddRequestHandler("/users/authenticate", "PUT", new AuthenticateUserHandler(_user))
                .AddRequestHandler("/users/authorize", "PUT", new AuthorizeUserHandler(_user));
        }
    }
}