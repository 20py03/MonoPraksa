using Autofac;
using Autofac.Integration.WebApi;
using Repository.Common;
using requests.Repository;
using requests.Service;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace requests.WebApi.App_Start
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ProteinService>().As<IProteinService>();
            builder.RegisterType<ProteinRepository>().As<IProteinRepository>();
            
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}