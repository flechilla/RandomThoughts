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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<RandomThoughtsDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<DbContextOptions<RandomThoughtsDbContext>>(_ =>
            {
                var optionBuilder =  new DbContextOptionsBuilder<RandomThoughtsDbContext>();
                optionBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

                return optionBuilder.Options;
            });

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


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();

                //TODO: see why the resolver is not resolving this ( problem with the instance of the options)
                var context = app.ApplicationServices.GetRequiredService<RandomThoughtsDbContext>();
                //var context = new RandomThoughtsDbContext(new DbContextOptions<RandomThoughtsDbContext>());

                //seed the DB TODO: These calls won't be here but in the SeedEngine library
                //are here now to test the seeds
                UserSeeds.AddOrUpdate(context);
                ThoughtHoleSeeds.AddOrUpdate(context);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

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
