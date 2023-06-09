using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace RestaurantBooking.Api.Services
{
    public class UriService : IUriService
    {
        private readonly IServer server;

        public UriService(IServer server)
        {
            this.server = server;
        }

        public string GetUri()
        {
            return server.Features.Get<IServerAddressesFeature>().Addresses.FirstOrDefault() + "/";
        }
    }
}
