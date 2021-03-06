﻿using Http.Router;

namespace CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            var time = new Time.Time();
            var database = new Common.Database();

            var securityGateway = new Security.Gateway.Gateway(time, database);

            var router = Factory.CreateRouter();
            router = securityGateway.RoutesUp(router);
            Http.Server.HttpServer.Start("http://*:8888/", router);
        }
    }
}