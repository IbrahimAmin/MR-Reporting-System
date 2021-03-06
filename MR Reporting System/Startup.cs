﻿using System.Reflection;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using MR_Reporting_System;
using MR_Reporting_System.AppConfig;
using MR_Reporting_System_Data_Service.Repository;
using MR_Reporting_System_Interface.IDataService;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace MR_Reporting_System
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var policy = new CorsPolicy
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true,
                SupportsCredentials = true
            };

            policy.ExposedHeaders.Add("Authorization");

            app.UseCors(new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(policy)
                }
            });

            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            app.UseNinjectMiddleware(CreateKernel);
            app.UseNinjectWebApi(config);

        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IAgentAreaRepository>().To<AgentAreaRepository>().InRequestScope();
            kernel.Bind<IAgentDistributerRepository>().To<AgentDistributerRepository>().InRequestScope();
            kernel.Bind<IAgentDrugsRepository>().To<AgentDrugsRepository>().InRequestScope();
            kernel.Bind<IAgentHospitalRepository>().To<AgentHospitalRepository>().InRequestScope();
            kernel.Bind<IAgentPharmaciesRepository>().To<AgentpharmaciesRepository>().InRequestScope();
            kernel.Bind<IAgentsRepository>().To<AgentsRepository>().InRequestScope();
            kernel.Bind<IAreaRepository>().To<AreaRepository>().InRequestScope();
            kernel.Bind<ICompaniesRepository>().To<CompaniesRepository>().InRequestScope();
            kernel.Bind<IDefaultListRepository>().To<DefaultListRepository>().InRequestScope();
            kernel.Bind<IDistributersRepository>().To<DistributersRepository>().InRequestScope();
            kernel.Bind<IDocotorsRepository>().To<DocotorsRepository>().InRequestScope();
            kernel.Bind<IDrugsRepository>().To<DrugsRepository>().InRequestScope();
            kernel.Bind<IGroupPermissionsRepository>().To<GroupPermissionsRepository>().InRequestScope();
            kernel.Bind<IGroupsRepository>().To<GroupsRepository>().InRequestScope();
            kernel.Bind<IHospitalsRepository>().To<HospitalsRepository>().InRequestScope();
            kernel.Bind<ILocationsRepository>().To<LocationsRepository>().InRequestScope();
            kernel.Bind<IPharmaciesRepository>().To<PharmaciesRepository>().InRequestScope();
            kernel.Bind<IVisitsRepository>().To<VisitsRepository>().InRequestScope();
            kernel.Bind<IOrdersRepository>().To<OrdersRepository>().InRequestScope();
            kernel.Bind<IOrderItemsRepository>().To<OrdersItemsRepository>().InRequestScope();

            return kernel;
        }
    }
}
