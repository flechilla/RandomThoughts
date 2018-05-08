using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.DataAccess.Extensions;
using RandomThoughts.Domain;
using RandomThoughts.Models;
using RandomThoughts.Services;
using AutoMapper;
using RandomThoughts.Business.Extensions;
using RandomThoughts.Config;
using RandomThoughts.DataAccess.Seeds;
using SeedEngine;
using SeedEngine.Core;
using Swashbuckle.AspNetCore.Swagger;

namespace RandomThoughts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            var mapper = autoMapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<RandomThoughtsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<RandomThoughtsDbContext>()
                .AddDefaultTokenProviders();

            //services.AddTransient<DbContextOptions<RandomThoughtsDbContext>>(_ =>
            //{
            //    var optionBuilder =  new DbContextOptionsBuilder<RandomThoughtsDbContext>();
            //    optionBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            //    return optionBuilder.Options;
            //});

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            //adds the configuration of the services for the DataLayer
            services.AddDataAccessServices();

            //adds the configuration of the services for the BusinessLayer
            services.AddBusinessServices();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "RandomThought Api", Version = "v1" });
            });

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                .AddInMemoryClients(IdentityConfig.GetClients())
                .AddTestUsers(IdentityConfig.GetUsers())
                .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                .AddAspNetIdentity<ApplicationUser>(); ;

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";
                });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5003")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("default");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();

                //TODO: have to try to resolve this in the library. Maybe is the SeedEnginge is configured
                //in the ConfigureServices, this will be possible ;)
               // var context = app.ApplicationServices.GetRequiredService<RandomThoughtsDbContext>();

                //apply all the seeds
                app.EnsureSeedData<RandomThoughtsDbContext>();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // app.UseAuthentication();

            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RandomThought Api");
            });
        }
    }
}
