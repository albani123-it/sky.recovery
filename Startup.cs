using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using sky.recovery.Helper.Config;
using sky.recovery.Helper.Query;
using sky.recovery.Insfrastructures;
using sky.recovery.Interfaces;
using sky.recovery.Interfaces.Ext;
using sky.recovery.Services;
using sky.recovery.Services.DBConfig;
using sky.recovery.Services.Ext;
using sky.recovery.Services.Repository;
//using static Org.BouncyCastle.Math.EC.ECCurve;

namespace sky.recovery
{
    
    public partial class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public SymmetricSecurityKey signingKey;
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("request.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IConfiguration config = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json")
     .Build();
            services.Configure<DbContextSettings>(config.GetSection("DbContextSettings"));
            services.AddScoped<IRestrukturServices, RestrukturServices>();
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IWorkflowServices, WorkflowServices>();
            services.AddScoped<IDocServices, DocumentServices>();
            services.AddScoped<IAydaServices, AydaServices>();
            services.AddScoped<IAuctionService, AuctionServices>();
            services.AddScoped<IAsuransiServices, AsuransiServices>();
            services.AddScoped<IExtRestruktureServices, ExtRestruktureServices>();
            services.AddScoped<IQuery, Query>();
            services.AddScoped<IRepositoryServices, RepositoryServices>();
            services.AddScoped<IWriteOffServices, WriteOffServices>();

            services.AddSingleton<IGeneralParam, RestruktureRepoConfig>();
            services.AddSingleton<IRepository, UserRepository>();
            services.AddSingleton<IRecoveryRepository, InsuranceRepositorycs>();

            services.AddSingleton<IRestruktureRepository, RestruktureRepository>();
            services.AddSingleton<IHelperRepository, HelperRepository>();

            services.AddControllers().AddNewtonsoftJson();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            //services.AddHttpContextAccessor();
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
            });
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
           
         

            signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("TokenAuthentication:SecretKey").Value));
            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                // Validate the token expiry
                ValidateLifetime = true,
                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HEIs V1 API",
                    Version = "v1",
                    Description = "HEIs API",
                    Contact = new OpenApiContact
                    {
                        Name = "Muhamad Tossa Syahlevi",
                        Email = "tossasyahlevi01@gmail.com"
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);

            });

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = true,
                    ValidAudience = tokenValidationParameters.ValidAudience,
                    ValidIssuer = tokenValidationParameters.ValidIssuer,

                    // When receiving a token, check that it is still valid.
                    ValidateLifetime = true,

                    // This defines the maximum allowable clock skew - i.e. provides a tolerance on the token expiry time
                    // when validating the lifetime. As we're creating the tokens locally and validating them on the same
                    // machines which should have synchronised time, this can be set to zero. Where external tokens are
                    // used, some leeway here could be useful.
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("MyPolicy");
            app.UseRouting();

            ConfigureAuth(app);
            app.UseMvc();
          

        }
    }
}
