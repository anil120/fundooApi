//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace FundooAPI
{
    using System;
    using System.Text;
    using AutoMapper;
    using FundooAPI.DataContext;
    using FundooAPI.PhotoHandler;
    using FundooAPI.PhotoWriter;
    using FundooAPI.Models;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using FundooAPI.Services;
    using FundooAPI.Repository;
    using Swashbuckle.AspNetCore.Swagger;
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// startup class is auto generated class which register all the service before using inside the project
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of  <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The interface of configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services This method gets called by the runtime. Use this method to add services to the container..
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPushNotification,PushNotification>();
            services.AddTransient<INotesRepository,NotesRepository>();
            services.AddScoped<INotesService, NotesService>();
            services.AddTransient<IImageHandler, ImageHandler>();
            services.AddTransient<IImageWriter, ImageWriter>();
            services.AddAutoMapper();
            //services.AddTransient<UserManager<ApplicationUser>>();
            ////Inject AppSettings
            services.Configure<ApplicationSettings>(this.Configuration.GetSection("ApplicationSettings"));
            ////adding connection string 
            services.AddDbContext<AuthenticationContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("IdentityConnections")));
            services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<AuthenticationContext>();
            ////adding identity to hash password
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 4;
            });

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            ////token 
            var key = Encoding.UTF8.GetBytes(this.Configuration["ApplicationSettings:JWT_Secret"].ToString());
            ////authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.Authority = "https://localhost:44359";
                x.Audience = "FundooApi";
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            })
            ////social logins
            .AddFacebook(options =>
            {
                options.AppId = this.Configuration["Authentication:Facebook:AppId"];

                options.AppSecret = this.Configuration["Authentication:Facebook:AppSecret"];
            })
            .AddCookie(options => {
                options.LoginPath = "/auth/signin";
            });

            ////registering redis cache
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1";
                option.InstanceName = "master";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build();
            });
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings")); // <--- added
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">application builder interface</param>
        /// <param name="env">hosting environment interface</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("MyPolicy");
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI V1");
                });
            }
            app.UseAuthentication();
            app.UseCors(builder =>
            builder.WithOrigins(this.Configuration["ApplicationSettings:Client_URL"].ToString()).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseMvc();
        }
    }
}
