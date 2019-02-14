using System;
using AutoMapper;
using GraphQL;
using GraphQL.Client;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyHotel.Entities;
using MyHotel.EntityFrameworkCore;
using MyHotel.GraphQL;
using MyHotel.GraphQL.Client;
using MyHotel.Models;
using MyHotel.Repositories;

namespace MyHotel
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //***< My services >*** 
            services.AddHttpClient<ReservationHttpGraphqlClient>(x => x.BaseAddress = new Uri(Configuration["GraphQlEndpoint"]));
            services.AddSingleton(t => new GraphQLClient(Configuration["GraphQlEndpoint"]));
            services.AddSingleton<ReservationGraphqlClient>();
            //***</ My services >*** 

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<MyHotelDbContext>(options => options.UseSqlServer(MyHotelDbContext.DbConnectionString));

            services.AddTransient<ReservationRepository>();

            //***< GraphQL Services >*** 
            services.AddScoped<IDependencyResolver>(x =>
                new FuncDependencyResolver(x.GetRequiredService));

            services.AddScoped<MyHotelSchema>();

            services.AddGraphQL(x =>
                {
                    x.ExposeExceptions = true; //set true only in dev mode.
                })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddUserContextBuilder(httpContext => httpContext.User)
                .AddDataLoader();

            //***</ GraphQL Services >*** 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MyHotelDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseGraphQL<MyHotelSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()); //to explorer API navigate https://*DOMAIN*/ui/playground

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            InitializeMapper();
        }

        private static void InitializeMapper()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<Guest, GuestModel>();
                x.CreateMap<Room, RoomModel>();
                x.CreateMap<Reservation, ReservationModel>();
            });
        }
    }
}
