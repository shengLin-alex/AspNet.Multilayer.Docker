using System;
using System.Linq;
using System.Reflection;
using AspectCore.DependencyInjection;
using AspectCore.Extensions.Autofac;
using AspNet.Multilayer.Docker.Helper;
using AspNet.Multilayer.Docker.Repository;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNet.Multilayer.Docker.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        /// <summary>
        /// DI Service Resolver
        /// </summary>
        private IServiceResolver ApplicationServiceResolver { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            // DbContext in application scope
            services.AddDbContext<PostgresDbContext>(options => options.UseNpgsql(this.Configuration.GetConnectionString("DockerPostgres")));
            services.AddDbContext<SqlServerDbContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("DockerSqlServer")));
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            IOrderedEnumerable<ITypeRegistrar> registrars
                = Assembly.GetExecutingAssembly()
                    .GetReferencedAssemblies()
                    .Select(Assembly.Load)
                    .Concat(new Assembly[]
                    {
                        // Assembly.Load("AspNet.Multilayer.Docker.Repository"),
                    })
                    .SelectMany(p => p.ExportedTypes.Where(s => s.IsAssignableTo<ITypeRegistrar>() && !s.IsInterface))
                    .Select(p => (ITypeRegistrar)Activator.CreateInstance(p))
                    .Distinct()
                    .OrderBy(p => p.Order);

            foreach (var registrar in registrars)
            {
                registrar.Register(builder);
            }
            
            // register Proxy for Interceptors(AOP)
            builder.RegisterDynamicProxy(configure =>
            {
                // configure.Interceptors.AddTyped<MethodInterceptorAttribute>();
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Defines a class that provides the mechanisms to configure an application's request pipeline.</param>
        /// <param name="env">Provides information about the web hosting environment an application is running in.</param>
        /// <param name="appLifetime">Allows consumers to perform cleanup during a graceful shutdown.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
        {
            this.ApplicationServiceResolver = app.ApplicationServices as IServiceResolver;
            
            // Set solution packages' dependency resolver
            DependencyResolver.Current.SetResolver(this.ApplicationServiceResolver);

            // build database and sync table if db not created.
            // PostgresDbContext postgresDbContext = this.ApplicationServiceResolver.Resolve<PostgresDbContext>();
            // postgresDbContext.Database.EnsureCreated();
            // SqlServerDbContext sqlServerDbContext = this.ApplicationServiceResolver.Resolve<SqlServerDbContext>();
            // sqlServerDbContext.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
            // If you want to dispose of resources that have been resolved in the
            // application container, register for the "ApplicationStopped" event.
            // You can only do this if you have a direct reference to the container,
            // so it won't work with the above ConfigureContainer mechanism.
            appLifetime.ApplicationStopped.Register(() => this.ApplicationServiceResolver.Dispose());
        }
    }
}
