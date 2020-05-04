using Common;
using Http.Router;
using Security.BLL;
using Time;

namespace Security.Gateway
{
    public class Gateway
    {
        private readonly User _user;
        private readonly SessionToken _sessionToken;

        public Gateway(ITime time, IDatabase database)
        {
            _sessionToken = new SessionToken(database);
            var permission = new Permission(database);
            var role = new Role(database);
            _user = new User(_sessionToken, time, database, permission, role);
        }

        public IRouter RoutesUp(IRouter router)
        {
            return router
                .AddRequestHandler("/users/authenticate", "PUT", new AuthenticateUserHandler(_user))
                .AddRequestHandler("/users/authorize", "PUT", new AuthorizeUserHandler(_user))
                .AddRequestHandler("/users/logout", "PUT", new LogoutHandler(_sessionToken));
        }
    }
}