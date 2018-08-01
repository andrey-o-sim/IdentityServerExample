using Microsoft.Owin.Hosting;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRoleIdentity
{
    public class WebRoleEntryPoint: RoleEntryPoint
    {
        public override bool OnStart()
        {
            var app = WebApp.Start("https://cloudserviceandreysimonenko.cloudapp.net:443", appBuilder =>
            {
                Startup.Configuration(appBuilder);
            });

            return base.OnStart();
        }
    }
}