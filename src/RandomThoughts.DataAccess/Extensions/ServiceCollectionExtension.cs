using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.DataAccess.Repositories.Thoughts;

namespace RandomThoughts.DataAccess.Extensions
{
    /// <summary>
    ///     Contians the functionalities to add the services 
    ///     that are implemented in the DataAcces layer.
    /// </summary>
    /// <remarks>
    ///     When a service for the DI will be used, this won't
    ///     be neccessary.
    /// </remarks>
    public static class ServiceCollectionExtension
    {
        public static void AddDataAccessServices(this IServiceCollection service)
        {
            service.AddScoped<IThoughtsRepository, ThoughtsRepository>();
            service.AddScoped<IUnitOfWork, SqlUnitOfWork>();

        }
    }
}
