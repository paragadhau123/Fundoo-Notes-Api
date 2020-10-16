//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace EmployeeManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using BusinessLayer.Service;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using RepositoryLayer;
    using RepositoryLayer.Interface;
    using RepositoryLayer.Service;

    /// <summary>
    /// Startup Class
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
           this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmployeeDatabaseSettings>(
               this.Configuration.GetSection(nameof(EmployeeDatabaseSettings)));
            services.AddSingleton<IEmployeeDatabaseSettings>(sp =>
              sp.GetRequiredService<IOptions<EmployeeDatabaseSettings>>().Value);

            services.AddSingleton<IEmployeeBL, EmployeeBusinessLayer>();
              services.AddSingleton<IEmployeeRL, EmployeeRepositoryLayer>();

            services.AddSingleton<INotesBL, NotesBL>();
            services.AddSingleton<INotesRL,NotesRL >();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
