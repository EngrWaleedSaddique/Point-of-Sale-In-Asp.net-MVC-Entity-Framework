﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SuperShaheenChemist.Web.Startup))]
namespace SuperShaheenChemist.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
