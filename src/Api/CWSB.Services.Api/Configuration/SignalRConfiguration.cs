using CWSB.Services.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Configuration
{
    public static class SignalRConfiguration
    {
        public static HubEndpointConventionBuilder MapSignalR(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapHub<ChatHub>("/chathub");
        }
    }
}
