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
    using System.Text;
   
    using BusinessLayer.Interface;
    using BusinessLayer.Service;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
   
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using RepositoryLayer;
    using RepositoryLayer.Interface;
    using RepositoryLayer.Service;

    /// <summary>
    /// Startup Class
    /// </summary>
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
           this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          //  services.AddCors();
            services.AddControllers();
            services.Configure<FundooNotesDatabaseSettings>(
               this.Configuration.GetSection(nameof(FundooNotesDatabaseSettings)));
            services.AddSingleton<IFundooNotesDatabaseSettings>(sp =>
              sp.GetRequiredService<IOptions<FundooNotesDatabaseSettings>>().Value);

            services.AddSingleton<IAccountsBL, AccountsBusinessLayer>();
            services.AddSingleton<IAccountsRL, AccountsRepositoryLayer>();

            services.AddSingleton<INotesBL, NotesBL>();
            services.AddSingleton<INotesRL, NotesRL>();

         
            var key = Encoding.ASCII.GetBytes("SuperSecretKey@345fghhhhhhhhhhhhhhhhhhhhhhhhhhhhhfggggggg");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

           
                services.AddControllers();
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Fundoo Notes",
                    Description = "Swagger Fundoo Api",

                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/ignaciojvig/ChatAPI/blob/master/LICENSE")
                    }

                });

                services.AddCors(options =>
                {
                    options.AddPolicy(name: MyAllowSpecificOrigins,
                                      builder =>
                                      {
                                          builder.WithOrigins("http://example.com",
                                                              "http://www.contoso.com");
                                      });
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            });


        }
      
     
// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
           
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();

            });
           
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "Fundoo Notes");
            });
        }
    }
}
