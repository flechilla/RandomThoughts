using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.Business.ApplicationServices.ThoughtHole;
using RandomThoughts.Business.ApplicationServices.Thoughts;
using RandomThoughts.Business.ApplicationServices.Comments;
using RandomThoughts.Business.ApplicationServices.ThoughtComment;

namespace RandomThoughts.Business.Extensions
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
        public static void AddBusinessServices(this IServiceCollection service)
        {
            service.AddScoped<IThoughtsApplicationService, ThoughtsApplicationService>();
            service.AddScoped<IThoughtHolesApplicationService, ThoughtHolesApplicationService>();
            service.AddScoped<ICommentsApplicationService, CommentsApplicationService>();
            service.AddScoped<IThoughtCommentApplicationService, ThoughtCommentApplicationService>();
        }
    }
}
